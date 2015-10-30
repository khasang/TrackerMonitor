using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UDPTestUIWPF
{
    public class UDPDataModel : DependencyObject
    {
        static public DependencyProperty PortProperty;
        static public DependencyProperty IpAddressProperty;
        static public DependencyProperty MessageProperty;

        static UDPDataModel()
        {
            PortProperty = DependencyProperty.Register("Port", typeof(int), typeof(UDPDataModel), new PropertyMetadata(9050));
            IpAddressProperty = DependencyProperty.Register("IpAddress", typeof(IPAddress), typeof(UDPDataModel), new PropertyMetadata(new IPAddress(new byte[]{192,168,0,1});
            MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(UDPDataModel), new PropertyMetadata("Message"));
        }

        public int Port 
        {
            set
            {
                SetValue(PortProperty, value);
            }
            get
            {
                return (int)GetValue(PortProperty);
            }
        }

        public IPAddress IpAddress
        {
            set
            {
                SetValue(IpAddressProperty, value);
            }
            get
            {
                return (IPAddress)GetValue(IpAddressProperty);
            }
        }

        public string Message
        {
            set
            {
                SetValue(MessageProperty, value);
            }
            get
            {
                return (string)GetValue(MessageProperty);
            }
        }

    }
}
