using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPServer
{
    public class UDPnet
    {
        UdpClient udpClient = null;
        bool stopReceive = false;

        public EventHandler eventReceivedMessage = (x, y) => { };
        public EventHandler eventReceivedError = (x, y) => { };
        
        public void StartReceiveAsync(int port) // Запуск приема сообщений
        {
            using(udpClient = new UdpClient(port))
            {
                Console.WriteLine(port);
                stopReceive = false;
                IPEndPoint remoteEP = null;

                try
                {
                    while (!stopReceive)  // В цикле слушаем сообщения
                    {
                        byte[] message = udpClient.Receive(ref remoteEP);

                        Task.Run(() => 
                        {
                            eventReceivedMessage(this, new UDPMessage() { Message = message });
                        });

                        //eventReceivedMessage.BeginInvoke(this, new UDPMessage() { Message = message }, iar => 
                        //{
                        //    var ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
                        //    var invokedMethod = (EventHandler)ar.AsyncDelegate;

                        //    try
                        //    {
                        //        invokedMethod.EndInvoke(iar);
                        //    }
                        //    catch
                        //    {

                        //    }
                        //}, null);
                    }
                }
                catch (Exception ex)
                {
                    // Генерируем событие об ошибке приема сообщений!
                    eventReceivedError(this, new ErrorMessage { Message = ex.Message });
                }
            }
        }
     
        public async Task<byte[]> ReceiveSingleMessageAsync(int port)
        {
            byte[] message;
            using(udpClient = new UdpClient(port))
            {
                message = (await udpClient.ReceiveAsync()).Buffer;
                eventReceivedMessage(this, new UDPMessage() { Message = message });

                udpClient.Close();
            }
            return message;
        }

        public void StopReceive()          // Метод остановки дополнительного потока
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
        public async Task<string> SendMessageAsync(byte[] message, IPAddress ipAddress, int port) // Отправка сообщения
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
                //finally
                //{
                //    udpClient.Close();  // После окончания попытки отправки закрываем UDP соединение
                //    backMessage += "Closed!";
                //}

                return backMessage;
            }            
        }
    }
}
