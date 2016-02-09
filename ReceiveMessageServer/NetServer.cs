using DAL;
using DAL.Entities;
using DAL.Logic;
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
    class NetServer : IDisposable
    {
        NetProtocol netProtocol;

        ApplicationDbContext dbContext;

        HubConnection hubConnection;
        IHubProxy hubProxy;

        public NetServer(NetProtocol netProtocol)
        {
            this.netProtocol = netProtocol;
            this.netProtocol.eventReceivedMessage += OnShowReceivedMessage;  // Подписываемся на событие получения сообщения

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
            netProtocol.StartReceiveAsync(Settings.Default.Port);
        }

        public void Stop()
        {
            netProtocol.StopReceive();
        }

        //private void OnShowReceivedMessage(object sender, EventArgs e)
        //{
        //    NetMessage message = e as NetMessage;
                        
        //    if (message == null)
        //        return;   // Здесь можно ввести обработку ошибки

        //    foreach (byte b in message.Message)
        //    {
        //        Console.Write("{0}", (int)b);
        //    }
        //    Console.WriteLine();

        //    GPSTrackerMessage gpsMessage = null;

        //    try
        //    {
        //        gpsMessage = GPSTrackerMessageConverter.BytesToMessage(message.Message);
        //    }
        //    catch
        //    {
        //        return;
        //    }

        //    Console.WriteLine("{0} : {1}", gpsMessage.Latitude, gpsMessage.Longitude);

        //    try
        //    {
        //        gpsMessage.GPSTracker = dbContext.GPSTrackers.Find(gpsMessage.GPSTrackerId);

        //        if (gpsMessage.GPSTracker == null)
        //        {
        //            // Записываем в лог, что пришло сообщение с неизвестного трекера
        //            Console.WriteLine("Не найден трекер с id = {0}", gpsMessage.GPSTrackerId);
        //        }

        //        dbContext.GPSTrackerMessages.Add(gpsMessage);
        //        dbContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Ошибка сохранения в базе данных! : {0}", ex.Message);
        //    }

        //    try
        //    {
        //        GPSTrackerMessage signalRMessage = new GPSTrackerMessage();

        //        signalRMessage.Id = gpsMessage.Id;
        //        signalRMessage.Latitude = gpsMessage.Latitude;
        //        signalRMessage.Longitude = gpsMessage.Longitude;
        //        signalRMessage.Time = gpsMessage.Time;
        //        signalRMessage.GPSTrackerId = gpsMessage.GPSTrackerId;
        //        signalRMessage.GPSTracker = new GPSTracker();
        //        signalRMessage.GPSTracker.OwnerId = gpsMessage.GPSTracker.OwnerId;

        //        hubProxy.Invoke("SendNewMessage", signalRMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Ошибка отправки сообщения на узел SignalR! : {0}", ex.Message);
        //    }
        //}

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        private void OnShowReceivedMessage(object sender, EventArgs e)
        {
            NetMessage message = e as NetMessage;

            if (message == null)
                return;   // Здесь можно ввести обработку ошибки

            Console.WriteLine(Encoding.UTF8.GetString(message.Message));
        }
    }
}
