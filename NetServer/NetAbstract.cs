using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public abstract class NetAbstract
    {
        protected bool stopReceive = false;

        public EventHandler eventReceivedMessage = (x, y) => { };
        public EventHandler eventReceivedError = (x, y) => { };

        public abstract void StartRecieveAsync(int port);
        public abstract void StopRecieveAsync();
        public abstract string SendMessageAsync(byte[] message, IPAddress ipAddress, int port);
    }
}
