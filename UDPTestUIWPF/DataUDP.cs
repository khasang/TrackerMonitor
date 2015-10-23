using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UDPTestUIWPF
{
    class UDPDataModel : DependencyObject
    {
        static public DependencyProperty InPortProperty;
        static public DependencyProperty OutPortProperty;
        static public DependencyProperty IpHostProperty;

        static UDPDataModel()
        {
            InPortProperty = DependencyProperty.Register("InPort", typeof(int), typeof(UDPDataModel), new PropertyMetadata(1111));
            OutPortProperty = DependencyProperty.Register("OutPort", typeof(int), typeof(UDPDataModel), new PropertyMetadata(2222));
            IpHostProperty = DependencyProperty.Register("IpHost", typeof(IPAddress), typeof(UDPDataModel));
        }

        public int InPort
        {
            set
            {
                SetValue(InPortProperty, value);
            }
            get
            {
                return (int)GetValue(InPortProperty);
            }
        }

        public int OutPort
        {
            set
            {
                SetValue(OutPortProperty, value);
            }
            get
            {
                return (int)GetValue(OutPortProperty);
            }
        }

        public IPAddress IpHost
        {
            set
            {
                SetValue(IpHostProperty, value);
            }
            get
            {
                return (IPAddress)GetValue(IpHostProperty);
            }
        }

    }
}
