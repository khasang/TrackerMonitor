using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public class NetMessage : EventArgs
    {
        public byte[] Message { get; set; }
    }
}
