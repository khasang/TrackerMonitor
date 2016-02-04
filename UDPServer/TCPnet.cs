using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    public class TCPnet
    {
        TcpListener tcpListener = null;
        bool stopReceive = false;

        public EventHandler eventReceivedMessage = (x, y) => { };
        public EventHandler eventReceivedError = (x, y) => { };

        public void StartReceiveAsync(int port)
        {
            stopReceive = false;
            tcpListener = new TcpListener(IPAddress.Any, port);
            
            tcpListener.Start();

            try
            {
                while(!stopReceive)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    NetworkStream clientNS = tcpClient.GetStream();

                    byte[] message = new byte[256];
                    clientNS.Read(message, 0, 256);

                    eventReceivedMessage(this, new UDPMessage { Message = message } );

                    clientNS.Close();
                }
            }
            catch (Exception ex)
            {
                eventReceivedError(this, new ErrorMessage { Message = ex.Message });
            }
        }
    }
}
