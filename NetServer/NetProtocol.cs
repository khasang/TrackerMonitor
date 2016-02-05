using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public abstract class NetProtocol
    {
        protected bool stopReceive = false;

        public EventHandler eventReceivedMessage = (x, y) => { };
        public EventHandler eventReceivedError = (x, y) => { };

        public abstract void StartReceiveAsync(int port);
        public abstract void StopReceive();
        public abstract Task<string> SendMessageAsync(byte[] message, IPAddress ipAddress, int port);
    }
}
