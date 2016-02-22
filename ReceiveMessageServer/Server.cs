using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Logic;
using Microsoft.AspNet.SignalR.Client;
using NetServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveMessageServer
{
    class Server : IDisposable
    {
        UDPnet udpServer;
        TCPnet tcpServer;
        IDataManager dataManager;

        HubConnection hubConnection;
        IHubProxy hubProxy;

        public Server()
        {
            this.udpServer = new UDPnet();
            this.tcpServer = new TCPnet();
            udpServer.eventReceivedMessage += OnUdpReceivedMessage;  // Подписываемся на событие получения сообщения
            tcpServer.eventReceivedMessage += OnTcpReceivedMessage;

            try
            {
                dataManager = new DataManager();
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
            tcpServer.StartReceiveAsync(Settings.Default.Port);
        }

        public void Stop()
        {
            udpServer.StopReceive();
            tcpServer.StopReceive();
        }

        private void OnUdpReceivedMessage(object sender, EventArgs e)
        {
            NetMessage message = e as NetMessage;
            foreach (var b in message.Message)
            {
                Console.Write("{0}", (int)b);
            }
            Console.WriteLine();
            if (message == null)
                return;   // Здесь можно ввести обработку ошибки

            GPSTrackerMessage gpsMessage = null;

            try
            {
                gpsMessage = GPSTrackerMessageConverter.BytesToMessage(message.Message);
            }
            catch (Exception ex)
            {
                return;
            }

            Console.WriteLine("{0} : {1}", gpsMessage.Latitude, gpsMessage.Longitude);

            SaveGpsTrackerMessage(gpsMessage);
            NotifyHub(gpsMessage);
        }

        private void OnTcpReceivedMessage(object sender, EventArgs e)
        {
            NetMessage message = e as NetMessage;
            foreach (var b in message.Message)
            {
                Console.Write("{0}", (int)b);
            }
            Console.WriteLine();
            if (message == null)
                return;   // Здесь можно ввести обработку ошибки

            GPSTrackerMessage gpsMessage = null;

            try
            {
                gpsMessage = GPSTrackerMessageConverter.Tk102BytesToMessage(message.Message);

                if (gpsMessage == null) return;
            }
            catch (Exception ex)
            {
                return;
            }

            Console.WriteLine("{0} : {1}", gpsMessage.Latitude, gpsMessage.Longitude);

            SaveGpsTrackerMessage(gpsMessage);
            NotifyHub(gpsMessage);
        }

        private void SaveGpsTrackerMessage(GPSTrackerMessage gpsMessage)
        {
            try
            {
                lock (dataManager)
                {
                    gpsMessage.GPSTracker = dataManager.GPSTrackers.GetAll().FirstOrDefault(g => g.Id.Contains(gpsMessage.GPSTrackerId));

                    if (gpsMessage.GPSTracker == null)
                    {
                        // Записываем в лог, что пришло сообщение с неизвестного трекера
                        Console.WriteLine("Не найден трекер с id = {0}", gpsMessage.GPSTrackerId);
                    }

                    dataManager.GPSTrackerMessages.Add(gpsMessage);
                    dataManager.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения в базе данных! : {0}", ex.Message);
            }
        }

        private void NotifyHub(GPSTrackerMessage gpsMessage)
        {
            try
            {
                GPSTrackerMessage signalRMessage = new GPSTrackerMessage();

                signalRMessage.Id = gpsMessage.Id;
                signalRMessage.Latitude = gpsMessage.Latitude;
                signalRMessage.Longitude = gpsMessage.Longitude;
                signalRMessage.Time = gpsMessage.Time;
                signalRMessage.GPSTrackerId = gpsMessage.GPSTrackerId;
                signalRMessage.GPSTracker = new GPSTracker();
                signalRMessage.GPSTracker.OwnerId = gpsMessage.GPSTracker.OwnerId;

                lock (hubProxy) { hubProxy.Invoke("SendNewMessage", signalRMessage); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка отправки сообщения на узел SignalR! : {0}", ex.Message);
            }
        }

        public void Dispose()
        {
            if (dataManager != null)
                dataManager.Dispose();
        }
    }
}
