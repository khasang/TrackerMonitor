using DAL;
using Microsoft.AspNet.SignalR.Client;
using NetServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPServer;

namespace ReceiveMessageServer
{
    public class TCPServer
    {
        TCPnet tcpServer;

        ApplicationDbContext dbContext;

        HubConnection hubConnection;
        IHubProxy hubProxy;

        public TCPServer()
        {
            this.tcpServer = new TCPnet();
            tcpServer.eventReceivedMessage += OnShowReceivedMessage;  // Подписываемся на событие получения сообщения

            try
            {
                dbContext = new ApplicationDbContext("DefaultConnection");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось подключиться к базе данных! : {0}", ex.Message);
            }

            try
            {
                this.hubConnection = new HubConnection(Settings.Default.Host);
                this.hubProxy = hubConnection.CreateHubProxy("PushNotify");
                hubConnection.Start().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось подключиться к узлу SignalR! : {0}", ex.Message);
            }
        }

        private void OnShowReceivedMessage(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
