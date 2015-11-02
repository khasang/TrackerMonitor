using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    public class UDPMessage : EventArgs
    {
        public byte[] Message { get; set; }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(Message);
        }
    }
}
