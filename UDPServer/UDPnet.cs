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
        bool stopReceive;

        public EventHandler eventReceived = (x, y) => { };
        public EventHandler eventErrorReceived = (x, y) => { };
        
        public async void StartReceiveAsync(int port) // Запуск отдельного потока для приема сообщений
        {
            using(udpClient = new UdpClient(port))
            {
                byte[] message;

                try
                {
                    while (true)  // В цикле слушаем сообщения
                    {
                        message = (await udpClient.ReceiveAsync()).Buffer; // Ассинхронно ждем получение сообщения
                        eventReceived(this, new MessageUDP() { Message = message }); // Генерируем событие о получении сообщения.

                        if (stopReceive == true) break;  // Если дана команда остановить поток, останавливаем бесконечный цикл.
                    }
                }
                catch (Exception e)
                {
                    // Генерируем событие об ошибке приема сообщений!
                    eventErrorReceived(this, new ErrorMessage { Message = e.Message });
                }
            }
        }
     
        public async Task<byte[]> ReceiveSingleMessageAsync(int port)
        {
            byte[] message;
            using(udpClient = new UdpClient(port))
            {
                message = (await udpClient.ReceiveAsync()).Buffer;
                eventReceived(this, new MessageUDP() { Message = message });

                udpClient.Close();
            }
            return message;
        }

        public void StopReceive()          // Функция безопасной остановки дополнительного потока
        {
            stopReceive = true;            // Останавливаем цикл приема сообщений           
            if (udpClient != null) udpClient.Close();  // Принудительно закрываем объект класса UdpClient
        }

        public async Task<string> SendMessageAsync(string message, IPAddress ipAddress, int port) // Отправка сообщения
        {
            using(udpClient = new UdpClient())
            {
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
                byte[] messageByte = Encoding.Default.GetBytes(message);

                string backMessage = string.Empty;

                try
                {
                    // Отправляем ассинхронно сообщение
                    int sended = await udpClient.SendAsync(messageByte, messageByte.Length, ipEndPoint);

                    // Если количество переданных байтов и предназначенных для 
                    // отправки совпадают, то 99,9% вероятности, что они доберутся 
                    // до адресата.
                    if (sended == messageByte.Length)
                    {
                        backMessage = string.Format("Отправленно {0} байт", sended);
                    }
                }
                catch
                {
                    backMessage = "Ошибка при отправке!";
                }
                finally
                {
                    udpClient.Close();  // После окончания попытки отправки закрываем UDP соединение
                    backMessage += " : Соединение закрыто.";
                }

                return backMessage;
            }            
        }
    }
}
