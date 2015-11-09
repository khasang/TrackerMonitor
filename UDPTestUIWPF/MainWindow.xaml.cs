using DAL;
using DAL.Entities;
using DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            settingModel = (SettingModel)this.FindName("SettingModel");
        }

        /// <summary>
        /// Обработчик нажатия на копку "Start/Stop"
        /// </summary>
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {            
            string contentButton = StartButton.Content.ToString();       // Состояние кнопки (не совсем правильно, потом переделать в модель)

            // Выбрана отправка сообщения
            if(contentButton == "Start" && SendRadioButton.IsChecked == true)
            {
                // Выбрана мультиотправка в цикле
                if (CycleCheckBox.IsChecked == true)
                {
                    CycleSendMessage(udpModel.IPAddress, udpModel.Port);
                }
                // Выбрана одиночная отправка
                else
                {
                    StatusLabel.Content = (string)await udpServer.SendMessageAsync(Encoding.ASCII.GetBytes(udpModel.Message), udpModel.IPAddress, udpModel.Port);
                }
            }

            // Выбран прием сообщений
            if(contentButton == "Start" && ReceiveRadioButton.IsChecked == true)
            {
                StartButton.Content = "Stop";
                StatusLabel.Content = "Принимаем сообщения...";

                // Циклический прием сообщений
                if (CycleCheckBox.IsChecked == true)
                {
                    udpServer.StartReceiveAsync(udpModel.Port);
                }
                // Прием одного сообщения
                else
                {
                    byte[] receiveMessage = (byte[])await udpServer.ReceiveSingleMessageAsync(udpModel.Port);

                    StartButton.Content = "Start";
                    StatusLabel.Content = "Статус соединения...";
                }
            }

            // Нажата кнопка "Стоп"
            if (contentButton == "Stop")
            {
                StartButton.Content = "Start";
                StatusLabel.Content = "Статус соединения";                

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
            StartButton.Content = "Stop";
            StatusLabel.Content = "Отправляем сообщения в цикле...";

            stopSend = false;
            Task.Factory.StartNew(() =>
            {
                while (true)                                     // В бесконечном цикле
                {
                    byte[] message = GetRndGPSTreckerMessage();  // Создаем случайное сообщение
                    udpServer.SendMessageAsync(message, ipAddress, port);  // Отправляем его

                    // Выводим отправленное сообщение в текстбоксе
                    MessageTextBox.Dispatcher.Invoke(new Action(() => MessageTextBox.Text += GPSTrackerMessageConverter.BytesToMessage(message).ToString()));
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

            //if(WriteDBCheckBox.Dispatcher.Invoke(new Action(() => XamlGeneratedNames WriteDBCheckBox.IsChecked)) == true)
            //{
            //    tracker = dbContext.GPSTrackers.FirstOrDefault(x => x.Name == nameTracker);
            //}
            //else
            //{
            //    tracker = new GPSTracker()
            //    {
            //        Id = "111111",
            //        Name = nameTracker
            //    };
            //}

            GPSTrackerMessage message = new GPSTrackerMessage()
            {
                Latitude = rnd.Next(1000),
                Longitude = rnd.Next(1000),
                Time = DateTime.Now,
                GPSTrackerId = "111111"
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

            MessageTextBox.Text += "\n";
            MessageTextBox.Text += gpsMessage.ToString();  // ToString() переопределен.

            if (WriteDBCheckBox.IsChecked == true)
            {
                gpsMessage.GPSTracker.GPSTrackerMessages.Add(gpsMessage);
                dbContext.SaveChanges();
            }
        }

    }
}
