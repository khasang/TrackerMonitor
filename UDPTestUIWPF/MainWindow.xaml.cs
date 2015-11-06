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

        public MainWindow()
        {
            this.udpServer = new UDPnet();
            udpServer.eventReceivedMessage += OnShowReceivedMessage;

            this.dbContext = new ApplicationDbContext();

            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var udpModel = (UDPDataModel) this.FindResource("UdpModel");
            string contentButton = StartButton.Content.ToString();

            // Выбрана отправка сообщения
            if(contentButton == "Start" && SendRadioButton.IsChecked == true)
            {

                // Выбрана мультиотправка в цикле
                if (CycleCheckBox.IsChecked == true)
                {
                    MessageTextBox.Text = string.Empty;
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
                while (true)
                {
                    byte[] message = GetRndGPSTreckerMessage();
                    udpServer.SendMessageAsync(message, ipAddress, port);

                    MessageTextBox.Dispatcher.Invoke(new Action(() => MessageTextBox.Text += string.Format("отправлено: {0}\n", Encoding.ASCII.GetString(message))));
                    Thread.Sleep(2000);

                    if (stopSend) break;
                }
            });
        }

        private byte[] GetRndGPSTreckerMessage()
        {
            var tracker = dbContext.GPSTrackers.FirstOrDefault(x => x.Name == "NewTracker");
            GPSTrackerMessage message = new GPSTrackerMessage()
            {
                Time = DateTime.Now,
                GPSTracker = tracker,
                Latitude = rnd.Next(1000),
                Longitude = rnd.Next(1000)
            };

            return GPSTrackerMessageConverter.MessageToByte(message);
        }

        private void OnShowReceivedMessage(object sender, EventArgs e)
        {
            UDPMessage message = e as UDPMessage;
            if (message == null)
                return;   // Здесь можно ввести обработку ошибки

            MessageTextBox.Text += "\n";
            MessageTextBox.Text += message.ToString();  // Переделать

            if (WriteDBCheckBox.IsChecked == true)
                WriteToDB(message.Message);
        }
        private void WriteToDB(byte[] bytes)
        {
            GPSTrackerMessage message = GPSTrackerMessageConverter.ByteToMessage(bytes);

            message.GPSTracker.GPSTrackerMessages.Add(message);
            dbContext.SaveChanges();
        }

    }
}
