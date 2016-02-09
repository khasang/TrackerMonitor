using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public class ErrorNetMessage : EventArgs
    {
        public string Message { get; set; }
    }
}
