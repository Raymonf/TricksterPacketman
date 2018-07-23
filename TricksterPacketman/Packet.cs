using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TricksterPacketman
{
    public class Packet
    {
        public DateTime Time { get; set; }
        public IPAddress Source { get; set; }
        public IPAddress Destination { get; set; }
        public int Opcode { get; set; }
        public int Length { get; set; }
        public byte[] Data { get; set; }
    }
}
