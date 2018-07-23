using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrickEncCS
{
    public class Client
    {
        public SessionInfo ClientSession { get; set; }
        public SessionInfo ServerSession { get; set; }
        public ushort Sequence { get; set; } = 0;
    }
}
