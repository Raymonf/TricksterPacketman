using PacketDotNet;
using SharpPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrickEncCS;

namespace TricksterPacketman
{
    public partial class ListForm : Form
    {
        static bool Paused = false;
        public static List<Packet> Packets = new List<Packet>();
        public static Dictionary<string, SessionInfo> Sessions = new Dictionary<string, SessionInfo>();

        public ListForm()
        {
            InitializeComponent();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Program.Device.OnPacketArrival += new PacketArrivalEventHandler(OnPacketArrival);
                Program.Device.Open(DeviceMode.Promiscuous, 1000);
                Program.Device.Filter = Program.Filter;
                Program.Device.Capture();
            }).Start();
        }

        private void OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var time = e.Packet.Timeval.Date;
            var len = e.Packet.Data.Length;
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            var tcpPacket = (TcpPacket)packet.Extract(typeof(TcpPacket));

            if (tcpPacket != null && tcpPacket.PayloadData != null && tcpPacket.PayloadData.Length >= 9)
            {
                var ipPacket = (IpPacket)tcpPacket.ParentPacket;
                IPAddress srcIp = ipPacket.SourceAddress;
                IPAddress dstIp = ipPacket.DestinationAddress;
                int srcPort = tcpPacket.SourcePort;
                int dstPort = tcpPacket.DestinationPort;

                var sKey = $"{srcPort}-{dstPort}";
                if (!Sessions.ContainsKey(sKey))
                {
                    Sessions.Add(sKey, new SessionInfo());
                }

                var isOutbound = ((WinPcapDevice)(e.Device)).Addresses.Where(x => x.Addr.ipAddress != null && x.Addr.ipAddress.ToString() == srcIp.ToString()).FirstOrDefault() != null;

                Console.WriteLine("{0}:{1}:{2},{3} Len={4} {5}:{6} -> {7}:{8} | {9}",
                    time.Hour, time.Minute, time.Second, time.Millisecond, len,
                    srcIp, srcPort, dstIp, dstPort, isOutbound);

                //try
                {

                    var data = tcpPacket.PayloadData;

                    if (Sessions[sKey].LastPartialPacket != null)
                    {
                        var last = Sessions[sKey].LastPartialPacket;
                        var newData = new byte[last.Length + data.Length];
                        Array.Copy(last, 0, newData, 0, last.Length);
                        Array.Copy(data, 0, newData, last.Length, data.Length);
                        Sessions[sKey].LastPartialPacket = null;
                        data = newData;

                        // Restore the key so we can unpack this packet
                        Sessions[sKey].Key = Sessions[sKey].LastKey;
                    }

                    var i = 0;
                    var packetNum = 0;
                    while (i < data.Length)
                    {
                        var bytes = data.Skip(i).ToArray();

                        // Sometimes, we'll get a packet that's less than 9 bytes.
                        // We should check to see if the packet is < 9 bytes and store it for the next time.
                        // After all, we can't unpack/decode a header that's less than 9 bytes long.
                        if (bytes.Length < 9)
                        {
                            // Store it!
                            Sessions[sKey].LastPartialPacket = bytes;
                            return;
                        }

                        var subLen = Unpacker.Unpack(Sessions[sKey], bytes);

                        // If the packet that we're supposed to unpack is larger than the buffer's end...
                        // Well, we should store it for the next time.
                        if (i + subLen > data.Length)
                        {
                            // Store it!
                            Sessions[sKey].LastPartialPacket = bytes;
                            return;
                        }

                        i += subLen;

                        var pkt = bytes.Take(subLen).ToArray();

                        ushort lenNoDum = BitConverter.ToUInt16(pkt, 0);
                        ushort opcode = BitConverter.ToUInt16(pkt, 2);

                        Packets.Add(new Packet()
                        {
                            Time = time,
                            Source = srcIp,
                            Destination = dstIp,
                            Data = pkt.Skip(9).Take(lenNoDum - 9 - 2).ToArray(),
                            Length = lenNoDum,
                            Opcode = opcode
                        });

                        if (!Paused)
                        {
                            PacketList.Invoke(() =>
                            {
                                PacketList.Items.Add(new ListViewItem(new string[]
                                {
                                    Packets.Count.ToString(),
                                    time.ToShortTimeString() + "." + time.Millisecond,
                                    $"{srcPort} > {dstPort}",
                                    isOutbound ? "Out" : "In",
                                    $"0x{Util.ByteToHex(BitConverter.GetBytes(opcode).Reverse().ToArray()).Replace(" ", "")}",
                                    $"{Util.ByteToHex(lenNoDum)} ({lenNoDum})"
                                }));
                            });
                        }

                        packetNum++;

                        //if (packetNum > 50) break;
                    }
                }
                //catch (Exception exc)
                {
                    //Console.WriteLine("Unable to unpack packet");
                    //throw exc;
                }
            }
        }

        private void ListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Device.Close();
            Environment.Exit(0);
        }

        private void PacketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PacketList.SelectedIndices.Count < 1 || PacketList.SelectedIndices[0] < 0)
            {
                return;
            }

            var idx = PacketList.SelectedIndices[0];

            if (PacketList.Items.Count < idx)
            {
                return;
            }

            var packet = Packets[int.Parse(PacketList.Items[idx].SubItems[0].Text) - 1];

            InfoTxt.Text = $"Time: {packet.Time.ToShortTimeString()}\r\nOpcode: 0x{Util.ByteToHex(BitConverter.GetBytes((ushort)packet.Opcode).Reverse().ToArray()).Replace(" ", "")}";

            HexTxt.Text = Util.ByteToHex(packet.Data);
        }

        private void CopyHexBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(HexTxt.Text);
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            Paused = !Paused;

            PauseBtn.Text = Paused ? "Resume Output" : "Pause Output";
        }
    }
}
