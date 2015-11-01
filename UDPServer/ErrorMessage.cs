using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    public class ErrorMessage : EventArgs
    {
        public string Message { get; set; }
    }
}
