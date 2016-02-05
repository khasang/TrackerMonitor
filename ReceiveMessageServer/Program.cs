using NetServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiveMessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetServer server;
            
            using (server = new NetServer(new TCPnet()))
            {
                server.Start();
                Console.WriteLine("Сервер запущен! Идет прослушка сообщений...");

                Console.WriteLine("Чтоб остановить сервер, введите 'stop'");
                while (true)
                {
                    if (Console.ReadLine() == "stop")
                    {
                        server.Stop();
                        Console.WriteLine("Сервер остановлен!");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }
    }
}
