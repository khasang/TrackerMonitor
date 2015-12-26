using DAL;
using DAL.Entities;
using DAL.Logic;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPServer;

namespace ReceiveMessageServer
{
    class Server : IDisposable
    {
        UDPnet udpServer;

        ApplicationDbContext dbContext;

        HubConnection hubConnection;
        IHubProxy hubProxy;

        public Server()
        {
            this.udpServer = new UDPnet();
            udpServer.eventReceivedMessage += OnShowReceivedMessage;  // Подписываемся на событие получения сообщения

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

        public void Start()
        {
            udpServer.StartReceiveAsync(Settings.Default.Port);
        }

        public void Stop()
        {
            udpServer.StopReceive();
        }

        private void OnShowReceivedMessage(object sender, EventArgs e)
        {
            UDPMessage message = e as UDPMessage;
            if (message == null)
                return;   // Здесь можно ввести обработку ошибки

            GPSTrackerMessage gpsMessage = GPSTrackerMessageConverter.BytesToMessage(message.Message);

            Console.WriteLine("{0} : {1}", gpsMessage.Latitude, gpsMessage.Longitude);

            try
            {
                gpsMessage.GPSTracker = dbContext.GPSTrackers.Find(gpsMessage.GPSTrackerId);

                if (gpsMessage.GPSTracker == null)
                {
                    // Записываем в лог, что пришло сообщение с неизвестного трекера
                    Console.WriteLine("Не найден трекер с id = {0}", gpsMessage.GPSTrackerId);
                }
                else
                {
                    dbContext.GPSTrackerMessages.Add(gpsMessage);
                    dbContext.SaveChanges();
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка работы с базой данных! : {0}", ex.Message);
            }

            try
            {
                hubProxy.Invoke("SendNewMessage", gpsMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка отправки сообщения на узел SignalR! : {0}", ex.Message);
            }
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
