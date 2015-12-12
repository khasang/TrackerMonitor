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

                return;
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

                return;
            }

            // Выбран прием сообщений
            if (settingModel.StateButton == "Start" && settingModel.SendReceive == false)
            {
                settingModel.StateButton = "Stop";
                settingModel.Status = "Receiving message...";

                // Выбрано отправлять сообщение хабу SignalR, соединяемся с хабом
                if(settingModel.SignalR == true)
                {
                    this.hubConnection = new HubConnection(@"http://localhost:3254");
                    this.hubProxy = hubConnection.CreateHubProxy("PushNotify");
                    hubConnection.Start().Wait();
                }

                if(settingModel.WriteToDB == true)
                {
                    dbContext = new ApplicationDbContext("UDPTestConnection");  // Для возможности записи сообщений в базу 
                }

                udpServer.StartReceiveAsync(udpModel.Port);
                return;
            }

                 
        }

        /// <summary>
        /// Отправляет случайные сообщения в цикле
        /// </summary>
        /// <param name="ipAddress">IP адрес получателя</param>
        /// <param name="port">Порт</param>
        private void CycleSendMessageAsync(IPAddress ipAddress, int port, IList<GPSTrackerMessage> messages)
        {          
            stopSend = false;            

            Task.Factory.StartNew(() =>
            {
                int count = 0;

                foreach (var message in messages)  // Проходим по списку сообщений
                {
                    byte[] messageByte = GPSTrackerMessageConverter.MessageToBytes(message);  // Конвертируем в массив байтов
                    udpServer.SendMessageAsync(messageByte, ipAddress, port);  // Отправляем его

                    // Выводим отправленное сообщение в текстбоксе
                    Dispatcher.Invoke(new Action(() => udpModel.Message += (++count).ToString() + "." + message.ToString()));
                    // Блокируем поток на 2 секунды
                    Thread.Sleep(2000);
                    // Если флаг остановки отправки сообщений, то выходим из цикла
                    if (stopSend) break;
                }

                Dispatcher.Invoke(new Action(() => settingModel.StateButton = "Start"));
                Dispatcher.Invoke(new Action(() => settingModel.Status = "Connection status"));
            });            
        }

        private IList<GPSTrackerMessage> GetGPSTrackerMessages(int quantity)
        {
            var messages = new List<GPSTrackerMessage>();

            CultureInfo usCulture = new CultureInfo("en-US");
            NumberFormatInfo dbNumberFormat = usCulture.NumberFormat;

            if(settingModel.Random == true)
            {
                for (int i = 0; i < quantity; i++)
                {
                    messages.Add(new GPSTrackerMessage()
                    {
                        GPSTrackerId = settingModel.IMEI,
                        Time = DateTime.Now,
                        Latitude = (double)rnd.Next(100),
                        Longitude = (double)rnd.Next(100)
                    });
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(@"GPSPoints.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] coordinates = sr.ReadLine().Split(';');

                        messages.Add(new GPSTrackerMessage()
                        {
                            GPSTrackerId = settingModel.IMEI,
                            Time = DateTime.Now,
                            Latitude = double.Parse(coordinates[0], dbNumberFormat),
                            Longitude = double.Parse(coordinates[1], dbNumberFormat)
                        });
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

            // Если выбрано писать сообщения в БД
            if (settingModel.WriteToDB == true)
            {
                try
                {
                    gpsMessage.GPSTracker = dbContext.GPSTrackers.Find(gpsMessage.GPSTrackerId);
                    if (gpsMessage.GPSTracker == null)
                    {
                        udpModel.Message += "Tracker is not found!\n";
                    }

                    dbContext.GPSTrackerMessages.Add(gpsMessage);
                    dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    udpModel.Message += "Error saving!\n";
                }                
            }

            // Если выбрано отправлять сообщения на хаб SignalR, отправляем
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
