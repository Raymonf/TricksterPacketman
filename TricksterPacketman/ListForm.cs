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

                try
                {
                    var packetNum = 0;
                    var i = 0;
                    while (i < tcpPacket.PayloadData.Length)
                    {
                        var bytes = tcpPacket.PayloadData.Skip(i).ToArray();

                        var subLen = Unpacker.Unpack(Sessions[sKey], bytes);
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
                                time.ToShortTimeString(),
                                $"{srcPort} > {dstPort}",
                                isOutbound ? "Out" : "In",
                                $"0x{Util.ByteToHex(BitConverter.GetBytes(opcode).Reverse().ToArray()).Replace(" ", "")}",
                                $"{Util.ByteToHex(lenNoDum)} ({lenNoDum})"
                                }));
                            });
                        }

                        packetNum++;

                        if (packetNum > 50) break;
                    }
                }
                catch
                {
                    Console.WriteLine("Unable to unpack packet");
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

            var packet = Packets[int.Parse(PacketList.Items[idx].SubItems[0].Text)];

            InfoTxt.Text = $"Time: {packet.Time.ToShortTimeString()}\r\nOpcode: 0x{Util.ByteToHex(BitConverter.GetBytes((ushort)packet.Opcode).Reverse().ToArray()).Replace(" ", "")}";

            HexTxt.Text = Util.ByteToHex(packet.Data);
        }
    }
}
