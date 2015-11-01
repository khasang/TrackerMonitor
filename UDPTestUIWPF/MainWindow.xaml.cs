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

        Task taskMultiSend;
        CancellationTokenSource cancellToken = new CancellationTokenSource();

        public MainWindow()
        {
            this.udpServer = new UDPnet();
            this.dataUDP = new UDPDataModel();

            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartButton.Content.ToString() == "Start")
            {
                if(SendRadioButton.IsChecked == true)
                {
                    if (MultiSendCheckBox.IsChecked == true)
                    {
                        StartButton.Content = "Stop";
                        StatusLabel.Content = "Отправляем сообщения в цикле...";

                        var ipAddress = IPAddress.Parse(IPAddressTextBox.Text);
                        var port = Convert.ToInt32(PortTextBox.Text);

                        var t = Task.Factory.StartNew(() => MultiSendMessageAsync(ipAddress, port), cancellToken.Token);
                        var s = t.AsyncState;
                    }
                    else
                    {
                        StatusLabel.Content = (string) await udpServer.SendMessageAsync(MessageTextBox.Text,
                                                                                        IPAddress.Parse(IPAddressTextBox.Text),
                                                                                        Convert.ToInt32(PortTextBox.Text));
                    }
                }
                else
                {
                    StartButton.Content = "Stop";
                    StatusLabel.Content = "Принимаем сообщения...";

                    udpServer.StartReceiveAsync(Convert.ToInt32(PortTextBox.Text));
                }                
            }
            else
            {
                StartButton.Content = "Start";
                StatusLabel.Content = "Статус соединения";

                cancellToken.Cancel();
                udpServer.StopReceive();
            }
        }

        private Task MultiSendMessageAsync(IPAddress ipAddress, int port)
        {
            Random rnd = new Random();
            return new Task(() =>
            {
                while (true)
                {
                    udpServer.SendMessageAsync(rnd.Next(1000, 10000).ToString(), ipAddress, port);
                    Thread.Sleep(2000);
                }
            });
        }

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
