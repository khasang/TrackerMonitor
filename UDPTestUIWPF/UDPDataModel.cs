using System.Net;
using System.Windows;

namespace UDPTestUIWPF
{
    public class UDPDataModel : DependencyObject
    {
        static public DependencyProperty PortProperty;
        static public DependencyProperty MessageProperty;

        static public DependencyProperty Octet1Property;
        static public DependencyProperty Octet2Property;
        static public DependencyProperty Octet3Property;
        static public DependencyProperty Octet4Property;

        static UDPDataModel()
        {
            Octet1Property = DependencyProperty.Register("Octet1",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata((byte)192),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            Octet2Property = DependencyProperty.Register("Octet2",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata((byte)168),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            Octet3Property = DependencyProperty.Register("Octet3",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata((byte)0),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            Octet4Property = DependencyProperty.Register("Octet4",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata((byte)255),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            PortProperty = DependencyProperty.Register("Port",
                                                        typeof(int),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(9050),
                                                        new ValidateValueCallback(ValidatePortCallbackMethod));
            
            MessageProperty = DependencyProperty.Register("Message",
                                                        typeof(string),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata("Message"));
        }

        public byte Octet1
        {
            set { SetValue(Octet1Property, value); }
            get { return (byte)GetValue(Octet1Property); }
        }

        public byte Octet2
        {
            set { SetValue(Octet2Property, value); }
            get { return (byte)GetValue(Octet2Property); }
        }

        public byte Octet3
        {
            set { SetValue(Octet3Property, value); }
            get { return (byte)GetValue(Octet3Property); }
        }

        public byte Octet4
        {
            set { SetValue(Octet4Property, value); }
            get { return (byte)GetValue(Octet4Property); }
        }

        public IPAddress IPAddress
        {
            get { return new IPAddress(new byte[] { Octet1, Octet2, Octet3, Octet4 }); }
            set
            {
                byte[] ipAddress = value.GetAddressBytes();
                Octet1 = ipAddress[0];
                Octet2 = ipAddress[1];
                Octet3 = ipAddress[2];
                Octet4 = ipAddress[3];
            }
        }

        public int Port
        {
            set { SetValue(PortProperty, value); }
            get { return (int)GetValue(PortProperty); }
        }
        
        public string Message
        {
            set { SetValue(MessageProperty, value); }
            get { return (string)GetValue(MessageProperty); }
        }

        static bool ValidateOctetCallbackMethod(object value)
        {
            if (value is byte)
                return true;
            return false;
        }

        static bool ValidatePortCallbackMethod(object value)
        {
            if (value is int)
                return true;
            return false;
        }
    }
}