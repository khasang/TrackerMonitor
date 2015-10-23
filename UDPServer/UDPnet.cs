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
        Thread thrd;
        bool stopReceive;

        int inPort;
        int outPort;

        IPAddress ipHost;
        UdpClient udp = null;

        public int InPort { get; set; }
        public int OutPort { get; set; }
        public IPAddress IpHost { get; set; }

        public UDPnet(string ipNet, int outPort, int inPort)
        {
            this.inPort = inPort;
            this.outPort = outPort;
            this.ipHost = IPAddress.Parse(ipNet);
        }

        public void StartReceive() // Запуск отдельного потока для приема сообщений
        {
            thrd = new Thread(Receive);
            thrd.Start();
        }

        void Receive() // Функция извлекающая пришедшие сообщения работающая в отдельном потоке.
        {
            try
            {
                if (udp != null) udp.Close();  // Перед созданием нового объекта закрываем старый
                udp = new UdpClient(inPort);
                IPEndPoint ipendpoint;

                int count = 0;
                while (true)
                {
                    ipendpoint = null;
                    byte[] message = udp.Receive(ref ipendpoint);

                    //System.Windows.MessageBox.Show(Encoding.Default.GetString(message));

                    if (stopReceive == true) break;  // Если дана команда остановить поток, останавливаем бесконечный цикл.
                }
                udp.Close();
                udp = null;
            }
            catch
            {
                //System.Windows.MessageBox.Show("Ошибка приема сообщений!");
            }
        }

        public void StopReceive()  // Функция безопасной остановки дополнительного потока
        {
            stopReceive = true;            // Останавливаем цикл в дополнительном потоке            
            if (udp != null) udp.Close();  // Принудительно закрываем объект класса UdpClient
            if (thrd != null) thrd.Join(); // Для корректного завершения дополнительного потока подключаем его к основному потоку.
        }

        public void SendMessage(string name, IPAddress ipaddress) // Отправка сообщения
        {
            UdpClient udp = new UdpClient();

            //IPAddress ipaddress = IPAddress.Parse("192.168.1.255");
            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, outPort);

            byte[] message = Encoding.Default.GetBytes(name);
            int sended = udp.Send(message, message.Length, ipendpoint);

            // Если количество переданных байтов и предназначенных для 
            // отправки совпадают, то 99,9% вероятности, что они доберутся 
            // до адресата.
            if (sended == message.Length)
            {
                // все в порядке 
                //System.Windows.MessageBox.Show("ok");
            }
            udp.Close();// После окончания попытки отправки закрываем UDP соединение,
        }
    }
}
