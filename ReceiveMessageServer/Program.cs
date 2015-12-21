using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveMessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.Start();
            Console.WriteLine("Сервер запущен!");

            Console.WriteLine("Чтоб остановить сервер, введите 'stop'");
            while(true)
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
