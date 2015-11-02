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

        public EventHandler eventReceivedMessage = (x, y) => { };
        public EventHandler eventReceivedError = (x, y) => { };
        
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
                        eventReceivedMessage(this, new UDPMessage() { Message = message }); // Генерируем событие о получении сообщения.

                        if (stopReceive == true) break;  // Если дана команда остановить поток, останавливаем бесконечный цикл.
                    }
                }
                catch (Exception e)
                {
                    // Генерируем событие об ошибке приема сообщений!
                    eventReceivedError(this, new ErrorMessage { Message = e.Message });
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
                    int sended = await udpClient.SendAsync(message, message.Length, ipEndPoint);

                    // Если количество переданных байтов и предназначенных для 
                    // отправки совпадают, то 99,9% вероятности, что они доберутся 
                    // до адресата.
                    if (sended == message.Length)
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
