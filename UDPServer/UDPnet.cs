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
        Thread thrd = null;
        bool stopReceive;
        UdpClient udpClient = null;

        int keyMessage = 0;
        Dictionary<int, byte[]> messages = new Dictionary<int, byte[]>();

        public int InPort { get; set; }
        public int OutPort { get; set; }
        public IPAddress IpAddress { get; set; }
        public Dictionary<int, byte[]> Messages { get { return messages; } }

        public UDPnet()
        {
            this.InPort = 9050;
            this.OutPort = 9051;
            this.IpAddress = new IPAddress(new byte[] { 192, 168, 1, 255 });
        }

        public UDPnet(IPAddress ipAddress, int outPort, int inPort)
        {
            this.InPort = inPort;
            this.OutPort = outPort;
            this.IpAddress = ipAddress;
        }

        public void StartReceive(int port) // Запуск отдельного потока для приема сообщений
        {
            this.InPort = port;
            thrd = new Thread(Receive);
            thrd.Start();
        }

        void Receive() // Функция извлекающая пришедшие сообщения работающая в отдельном потоке.
        {
            try
            {
                if (udpClient != null) udpClient.Close();  // Перед созданием нового объекта закрываем старый

                udpClient = new UdpClient(InPort);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, InPort);

                while (true)
                {
                    byte[] message = udpClient.Receive(ref ipEndPoint);

                    Messages.Add(++keyMessage, message);   // Имитация записи в БД
                    
                    if (stopReceive == true) break;  // Если дана команда остановить поток, останавливаем бесконечный цикл.
                }
                udpClient.Close();
                udpClient = null;
            }
            catch
            {
               //  Ошибка приема сообщений!
                Messages.Add(++keyMessage, Encoding.Default.GetBytes("Ошибка приема сообщений!"));
            }
        }

        public void StopReceive()  // Функция безопасной остановки дополнительного потока
        {
            stopReceive = true;            // Останавливаем цикл в дополнительном потоке            
            if (udpClient != null) udpClient.Close();  // Принудительно закрываем объект класса UdpClient
            if (thrd != null) thrd.Join(); // Для корректного завершения дополнительного потока подключаем его к основному потоку.
        }

        public string SendMessage(string message, IPAddress ipAddress, int port) // Отправка сообщения
        {
            UdpClient udp = new UdpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            byte[] messageByte = Encoding.Default.GetBytes(message);

            string backMessage = string.Empty;

            try
            {
                int sended = udp.Send(messageByte, messageByte.Length, ipEndPoint);

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
                udp.Close();  // После окончания попытки отправки закрываем UDP соединение
                backMessage += " : Соединение закрыто.";
            }

            return backMessage;
        }
    }
}
