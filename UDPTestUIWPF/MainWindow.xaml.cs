using DAL;
using DAL.Entities;
using DAL.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UDPServer;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Messaging;

namespace UDPTestUIWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPnet udpServer;

        UDPDataModel udpModel;
        SettingModel settingModel;

        ApplicationDbContext dbContext;

        HubConnection hubConnection;
        IHubProxy hubProxy;

        bool stopSend = true;
        Random rnd = new Random();

        public MainWindow()
        {
            this.udpServer = new UDPnet();
            udpServer.eventReceivedMessage += OnShowReceivedMessage;  // Подписываемся на событие получения сообщения

            this.dbContext = new ApplicationDbContext("UDPTestConnection");  // Для возможности записи сообщений в базу   

            InitializeComponent();

            udpModel = (UDPDataModel)this.FindResource("UdpModel");          // Достаем модели из xaml
            settingModel = (SettingModel)this.FindResource("SettingModel");
        }

        /// <summary>
        /// Обработчик нажатия на копку "Start/Stop"
        /// </summary>
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Нажата кнопка "Стоп"
            if (settingModel.StateButton == "Stop")
            {
                settingModel.StateButton = "Start";
                settingModel.Status = "Connection status";

                stopSend = true;          // Останавливаем мультиотправку
                udpServer.StopReceive();  // Останавливаем прием сообщений

                if (hubConnection != null && hubConnection.State == ConnectionState.Connected)
                    hubConnection.Stop();
            }       

            // Выбрана отправка сообщения
            if(settingModel.StateButton == "Start" && settingModel.SendReceive == true)
            {
                settingModel.StateButton = "Stop";

                settingModel.Status = "Отправляем сообщения в цикле...";

                
                CycleSendMessageAsync(udpModel.IPAddress, udpModel.Port, GetGPSTrackerMessages((int)settingModel.Quantity));

                

                // Выбрана одиночная отправка
                //settingModel.Status = (string)await udpServer.SendMessageAsync(Encoding.ASCII.GetBytes(udpModel.Message), udpModel.IPAddress, udpModel.Port);
                //settingModel.StateButton = "Start";
            }

            // Выбран прием сообщений
            if (settingModel.StateButton == "Start" && settingModel.SendReceive == false)
            {
                settingModel.StateButton = "Stop";
                settingModel.Status = "Receiving message...";

                // Выбрано отправлять сообщение хабу SignalR
                if(SignalRCheckBox.IsChecked == true)
                {
                    this.hubConnection = new HubConnection(@"http://localhost:3254");
                    this.hubProxy = hubConnection.CreateHubProxy("PushNotify");
                    hubConnection.Start().Wait();
                }

                //// Циклический прием сообщений
                //if (settingModel.Random == true)
                //{
                //    udpServer.StartReceiveAsync(udpModel.Port);
                //}
                //// Прием одного сообщения
                //else
                //{
                //    byte[] receiveMessage = (byte[])await udpServer.ReceiveSingleMessageAsync(udpModel.Port);

                //    settingModel.StateButton = "Start";
                //    settingModel.Status = "Connection status";
                //}

                udpServer.StartReceiveAsync(udpModel.Port);
                return;
            }

                 
        }

        /// <summary>
        /// Отправляет случайные сообщения в цикле
        /// </summary>
        /// <param name="ipAddress">IP адрес получателя</param>
        /// <param name="port">Порт</param>
        private async Task CycleSendMessageAsync(IPAddress ipAddress, int port, IList<GPSTrackerMessage> messages)
        {          
            stopSend = false;

            List<GPSTracker> trackers = GetTrackers();
            int count = 0;

            foreach(var message in messages)     // В цикле
            {
                byte[] messageByte = GPSTrackerMessageConverter.MessageToBytes(message);

                string backMessage = (string)await udpServer.SendMessageAsync(messageByte, ipAddress, port);  // Отправляем его

                Dispatcher.Invoke(new Action(() => settingModel.Status = backMessage));

                // Выводим отправленное сообщение в текстбоксе
                Dispatcher.Invoke(new Action(() => udpModel.Message += (++count).ToString() + "." + message.ToString()));
                // Блокируем поток на 2 секунды
                Thread.Sleep(2000);
                // Если флаг остановки отправки сообщений, то выходим из цикла
                if (stopSend) break;
            }

            Dispatcher.Invoke(new Action(() => settingModel.StateButton = "Start"));
            Dispatcher.Invoke(new Action(() => settingModel.Status = "Connection status"));
        }

        private List<GPSTracker> GetTrackers()
        {            
            List<GPSTracker> trackers = new List<GPSTracker>();

            try
            {
                trackers = dbContext.GPSTrackers.ToList();
            }
            catch
            {
                trackers.Add(new GPSTracker()
                {
                    Id = "111111",
                    Name = "Tracker1"
                });

                trackers.Add(new GPSTracker()
                {
                    Id = "222222",
                    Name = "Tracker2"
                });
            }

            return trackers;
        }

        /// <summary>
        /// Создает сообщение типа GPSTrackerMessage со случайными параметрами
        /// </summary>
        /// <returns>byte[]</returns>
        private GPSTrackerMessage GetGPSTrackerMessage(string trackerId, double latitude, double longitude)
        {
            GPSTrackerMessage message = new GPSTrackerMessage()
            {
                Latitude = latitude,
                Longitude = longitude,
                Time = DateTime.Now,
                GPSTrackerId = trackerId
            };

            return message;
        }

        private IList<GPSTrackerMessage> GetGPSTrackerMessages(int quantity)
        {
            var trackers = GetTrackers();
            var messages = new List<GPSTrackerMessage>();

            CultureInfo usCulture = new CultureInfo("en-US");
            NumberFormatInfo dbNumberFormat = usCulture.NumberFormat;

            if(settingModel.Random == true)
            {
                for (int i = 0; i < quantity; i++)
                    messages.Add(GetGPSTrackerMessage(trackers[0].Id,
                                                      (double)rnd.Next(100),
                                                      (double)rnd.Next(100)));
                
            }
            else
            {
                using (StreamReader sr = new StreamReader(@"GPSPoints.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] coordinates = sr.ReadLine().Split(';');

                        messages.Add(GetGPSTrackerMessage(trackers[0].Id,
                                                          double.Parse(coordinates[0], dbNumberFormat),
                                                          double.Parse(coordinates[1], dbNumberFormat)));
                    }
                }
            }

            return messages.Take(quantity).ToList();
        }
        
        /// <summary>
        /// Вывод полученного сообщения в текстбоксе
        /// </summary>
        private void OnShowReceivedMessage(object sender, EventArgs e)
        {
            UDPMessage message = e as UDPMessage;
            if (message == null)
                return;   // Здесь можно ввести обработку ошибки

            GPSTrackerMessage gpsMessage = GPSTrackerMessageConverter.BytesToMessage(message.Message);

            udpModel.Message += gpsMessage.ToString();  // ToString() переопределен.

            if (settingModel.WriteToDB == true)
            {
                gpsMessage.GPSTracker = dbContext.GPSTrackers.Find(gpsMessage.GPSTrackerId);
                dbContext.GPSTrackerMessages.Add(gpsMessage);
                try
                {
                    dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    Dispatcher.Invoke(new Action(() => StatusLabel.Content = ex.Message));
                }                
            }

            if(settingModel.SignalR == true)
            {
                try
                {
                    hubProxy.Invoke("SendNewMessage", gpsMessage);
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(new Action(() => StatusLabel.Content = ex.Message));
                } 
            }
        }



    }
}
