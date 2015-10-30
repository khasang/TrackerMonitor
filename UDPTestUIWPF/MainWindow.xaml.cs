using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
                    if (MultiSendCheckBox.IsChecked)
                    {
                        
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

                udpServer.StopReceive();
            }
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
