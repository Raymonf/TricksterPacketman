using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrickEncCS
{
    public class SessionInfo
    {
        public bool IsStrict { get; set; } = false;
        public bool IsFirst { get; set; } = true;
        public byte Key { get; set; } = 0x01;
        public byte LastKey { get; set; } = 0x01;
        public ushort Sequence { get; set; } = 0x00;
        public byte[] LastPartialPacket { get; set; } = null;

        public Client Client { get; set; } = new Client();
    };

    public class Header
    {
        public ushort Len { get; set; }
        public ushort Cmd { get; set; }
        public ushort Seq { get; set; }
        public byte RandKey { get; set; }
        public byte Packing { get; set; }
        public byte CheckFlag { get; set; }
    };

    public class Common
    {
        public static byte MakeChecksum(byte[] packet, byte key)
        {
            int ecx = packet[0];
            int edx = packet[2];
            ecx <<= 8;
            ecx += edx;
            ecx = KeyTable.Table[(ushort)ecx];

            edx = (ushort)(key << 8);
            edx += ecx;
            byte bl = KeyTable.Table[(ushort)edx];

            ecx = packet[3];
            edx = packet[1];
            ecx <<= 8;
            ecx += edx;
            ecx = KeyTable.Table[(ushort)ecx];

            edx = packet[6];
            edx <<= 8;
            edx += ecx;
            ecx = edx;
            byte al = KeyTable.Table[(ushort)ecx];

            ecx = bl;
            ecx <<= 8;
            ecx += al;

            al = KeyTable.Table[(ushort)ecx];

            return al;

            /*
            var x = KeyTable.Table[(packet[0] << 8) + packet[2]];
            var bl = KeyTable.Table[(key << 8) + x];

            var y = KeyTable.Table[(packet[3] << 8) + packet[1]];
            var al = KeyTable.Table[(packet[6] << 8) + y];

            return KeyTable.Table[(bl << 8) + al];*/
        }

        public static void UpdateKey(SessionInfo sessionInfo, int tail)
        {
            //byte key = sessionInfo.Key;
            //sessionInfo.Key = (byte)(KeyTable.Table[(ushort)((tail & 0xff) << 8) + key] + key);

            byte key = sessionInfo.Key;
            int edx = tail & 0xff;
            edx <<= 8;
            edx += key;
            byte al = KeyTable.Table[(ushort)edx];
            al += key;
            sessionInfo.Key = al;
        }
    }
}