using DAL;
using DAL.Entities;
using DAL.Logic;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UDPServer;

namespace UDPTestUIWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPDataModel dataUDP;
        UDPnet udpServer;

        bool stopSend = true;
        Random rnd = new Random();

        ApplicationDbContext dbContext;

        UDPDataModel udpModel;
        SettingModel settingModel;

        public MainWindow()
        {
            this.udpServer = new UDPnet();
            udpServer.eventReceivedMessage += OnShowReceivedMessage;  // Подписываемся на событие получения сообщения

            this.dbContext = new ApplicationDbContext();  // Для возможности записи сообщений в базу

            InitializeComponent();

            udpModel = (UDPDataModel)this.FindResource("UdpModel");     // Достаем модели из xaml
            settingModel = (SettingModel)this.FindResource("SettingModel");
        }

        /// <summary>
        /// Обработчик нажатия на копку "Start/Stop"
        /// </summary>
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var d = dbContext.GPSTrackers.ToArray();
            // Выбрана отправка сообщения
            if(settingModel.StateButton == "Start" && settingModel.SendReceive == true)
            {
                settingModel.StateButton = "Stop";

                // Выбрана мультиотправка в цикле
                if (settingModel.Cycle == true)
                {                    
                    settingModel.Status = "Отправляем сообщения в цикле...";
                    CycleSendMessage(udpModel.IPAddress, udpModel.Port);
                }
                // Выбрана одиночная отправка
                else
                {
                    settingModel.Status = (string)await udpServer.SendMessageAsync(Encoding.ASCII.GetBytes(udpModel.Message), udpModel.IPAddress, udpModel.Port);
                    settingModel.StateButton = "Start";
                }
                
                return;
            }

            // Выбран прием сообщений
            if(settingModel.StateButton == "Start" && ReceiveRadioButton.IsChecked == true)
            {
                settingModel.StateButton = "Stop";
                settingModel.Status = "Receiving message...";

                // Циклический прием сообщений
                if (settingModel.Cycle == true)
                {
                    udpServer.StartReceiveAsync(udpModel.Port);
                }
                // Прием одного сообщения
                else
                {
                    byte[] receiveMessage = (byte[])await udpServer.ReceiveSingleMessageAsync(udpModel.Port);

                    settingModel.StateButton = "Start";
                    settingModel.Status = "Connection status";
                }

                return;
            }

            // Нажата кнопка "Стоп"
            if (settingModel.StateButton == "Stop")
            {
                settingModel.StateButton = "Start";
                settingModel.Status = "Connection status";                

                stopSend = true; // Останавливаем мультиотправку
                udpServer.StopReceive();  // Останавливаем прием сообщений
            }            
        }

        /// <summary>
        /// Отправляет случайные сообщения в цикле
        /// </summary>
        /// <param name="ipAddress">IP адрес получателя</param>
        /// <param name="port">Порт</param>
        private void CycleSendMessage(IPAddress ipAddress, int port)
        {          
            stopSend = false;
            Task.Factory.StartNew(() =>
            {
                while (true)                                     // В бесконечном цикле
                {
                    byte[] message = GetRndGPSTreckerMessage();  // Создаем случайное сообщение
                    udpServer.SendMessageAsync(message, ipAddress, port);  // Отправляем его

                    // Выводим отправленное сообщение в текстбоксе
                    Dispatcher.Invoke(new Action(() => udpModel.Message += GPSTrackerMessageConverter.BytesToMessage(message).ToString()));
                    //MessageTextBox.Dispatcher.Invoke(new Action(() => MessageTextBox.Text += GPSTrackerMessageConverter.BytesToMessage(message).ToString()));
                    // Блокируем поток на 2 секунды
                    Thread.Sleep(2000);
                    // Если флаг остановки отправки сообщений, то выходим из цикла
                    if (stopSend) break;
                }
            });
        }

        /// <summary>
        /// Создает сообщение из экземпляра GPSTrackerMessage со случайными параметрами
        /// </summary>
        /// <returns>byte[]</returns>
        private byte[] GetRndGPSTreckerMessage()
        {
            GPSTracker tracker;
            string nameTracker = "Tracker" + rnd.Next(1, 3).ToString();

            try
            {
                tracker = dbContext.GPSTrackers.FirstOrDefault(x => x.Name == nameTracker);
            }
            catch
            {
                tracker = new GPSTracker()
                {
                    Id = "111111",
                    Name = "Tracker1"
                };
            }

            GPSTrackerMessage message = new GPSTrackerMessage()
            {
                Latitude = rnd.Next(1000),
                Longitude = rnd.Next(1000),
                Time = DateTime.Now,
                GPSTracker = tracker
            };

            return GPSTrackerMessageConverter.MessageToBytes(message);
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
                dbContext.GPSTrackerMessages.Add(gpsMessage);
                dbContext.SaveChanges();
            }
        }

    }
}
