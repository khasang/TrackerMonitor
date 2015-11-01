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

        public MainWindow()
        {
            this.udpServer = new UDPnet();
            this.dataUDP = new UDPDataModel();            

            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Нажата кнопка "Старт"
            if (StartButton.Content.ToString() == "Start")
            {
                // Выбрана отправка сообщения
                if(SendRadioButton.IsChecked == true)
                {
                    IPAddress ipAddress = IPAddress.Parse(IPAddressTextBox.Text);
                    int port = Convert.ToInt32(PortTextBox.Text);
                    string message = MessageTextBox.Text;

                    // Выбрана мультиотправка в цикле
                    if (MultiSendCheckBox.IsChecked == true)
                    {
                        StartButton.Content = "Stop";
                        StatusLabel.Content = "Отправляем сообщения в цикле...";                        

                        stopSend = false;
                        Task.Factory.StartNew(() =>
                        {
                            while (true)
                            {
                                message = rnd.Next(1000, 10000).ToString();
                                udpServer.SendMessageAsync(message, ipAddress, port);
                                Thread.Sleep(2000);

                                if (stopSend) break;
                            }
                        });
                    }
                    // Выбрана одиночная отправка
                    else
                    {
                        StatusLabel.Content = (string) await udpServer.SendMessageAsync(message, ipAddress, port);
                    }
                }
                // Выбрано "Принимать сообщения"
                else
                {
                    StartButton.Content = "Stop";
                    StatusLabel.Content = "Принимаем сообщения...";

                    udpServer.StartReceiveAsync(Convert.ToInt32(PortTextBox.Text));
                }                
            }
            // Нажата кнопка "Стоп"
            else
            {
                StartButton.Content = "Start";
                StatusLabel.Content = "Статус соединения";

                stopSend = true; // Останавливаем мультиотправку
                udpServer.StopReceive();  // Останавливаем прием сообщений
            }
        }

        /// <summary>
        /// Обновляет в текстбоксе список полученных сообщений
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder str = new StringBuilder();
            foreach(var message in udpServer.Messages)
            {
                str.AppendLine(Encoding.ASCII.GetString(message.Value));
            }
            MessageTextBox.Text = str.ToString();
        }
    }
}
