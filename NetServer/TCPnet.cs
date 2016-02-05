using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public class TCPnet : NetAbstract
    {
        TcpListener tcpListener = null;

        public override void StartRecieveAsync(int port)
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

                    eventReceivedMessage(this, new NetMessage { Message = message } );

                    clientNS.Close();
                }
            }
            catch (Exception ex)
            {
                eventReceivedError(this, new ErrorNetMessage { Message = ex.Message });
            }
        }

        public override void StopRecieveAsync()
        {
            stopReceive = true;            // Останавливаем цикл приема сообщений           
            if (tcpListener != null) tcpListener.Stop();
        }

        public override string SendMessageAsync(byte[] message, IPAddress ipAddress, int port)
        {
            string backMessage;

            try
            {
                TcpClient tcpClient = new TcpClient(new IPEndPoint(ipAddress, port));
                NetworkStream clientStream = tcpClient.GetStream();

                clientStream.Write(message, 0, message.Length);

                backMessage = message.Length.ToString();
            }
            catch(Exception ex)
            {
                backMessage = string.Format("Error send: {0}", ex.Message);
            }

            return backMessage;
        }
    }
}
