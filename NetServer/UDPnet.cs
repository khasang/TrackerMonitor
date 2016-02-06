using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetServer
{
    public class UDPnet : NetProtocol
    {
        UdpClient udpClient = null;
        
        public override async void StartReceiveAsync(int port) // Запуск приема сообщений
        {
            using(udpClient = new UdpClient(port))
            {
                Console.WriteLine(port);
                stopReceive = false;

                try
                {
                    while(!stopReceive)
                    {
                        await udpClient.ReceiveAsync().ContinueWith((udpReceiveTask) =>
                        {
                            byte[] message = udpReceiveTask.Result.Buffer;

                            Task.Run(() =>
                            {
                                eventReceivedMessage(this, new NetMessage() { Message = message });
                            });
                        });
                    }
                }
                catch (Exception ex)
                {
                    eventReceivedError(this, new ErrorNetMessage { Message = ex.Message });
                }
            }
        }
     
        public async Task<byte[]> ReceiveSingleMessageAsync(int port)
        {
            byte[] message;
            using(udpClient = new UdpClient(port))
            {
                message = (await udpClient.ReceiveAsync()).Buffer;
                eventReceivedMessage(this, new NetMessage() { Message = message });

                udpClient.Close();
            }
            return message;
        }

        public override void StopReceive()          // Метод остановки дополнительного потока
        {
            stopReceive = true;            // Останавливаем цикл приема сообщений           
            if (udpClient != null) udpClient.Close();  // Принудительно закрываем объект класса UdpClient
        }

        /// <summary>
        /// Асинхронно отправляет сообщение по UDP протоколу
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ipAddress">IP адрес</param>
        /// <param name="port">Порт</param>
        /// <returns>Результат отправки</returns>
        public override async Task<string> SendMessageAsync(byte[] message, IPAddress ipAddress, int port) // Отправка сообщения
        {
            using(udpClient = new UdpClient())
            {
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
                string backMessage = string.Empty;

                try
                {
                    // Отправляем ассинхронно сообщение
                    int sent = await udpClient.SendAsync(message, message.Length, ipEndPoint);

                    // Если количество переданных байтов и предназначенных для 
                    // отправки совпадают, то 99,9% вероятности, что они доберутся 
                    // до адресата.
                    if (sent == message.Length)
                    {
                        backMessage = string.Format("Sent {0} bytes. ", sent);
                    }
                }
                catch
                {
                    backMessage = "Error sending!";
                }

                return backMessage;
            }            
        }
    }
}
