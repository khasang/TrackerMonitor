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
                                                        new PropertyMetadata(192, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)));

            Octet2Property = DependencyProperty.Register("Octet2",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(168, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)));

            Octet3Property = DependencyProperty.Register("Octet3",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(0, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)));

            Octet4Property = DependencyProperty.Register("Octet4",
                                                        typeof(byte),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(255, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)));

            PortProperty = DependencyProperty.Register("Port",
                                                        typeof(ushort),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(9050, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoercePortCallbackMethod)));
            
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

        public ushort Port
        {
            set { SetValue(PortProperty, value); }
            get { return (ushort)GetValue(PortProperty); }
        }
        
        public string Message
        {
            set { SetValue(MessageProperty, value); }
            get { return (string)GetValue(MessageProperty); }
        }

        // Метод, который будет срабатывать при обновлении значения свойства для корректирования значения октета, если оно не подходит.
        private static object CoerceOctetCallbackMethod(DependencyObject d, object baseValue)
        {
            if ((int)baseValue <= 255 && (int)baseValue >= 0)
                return baseValue;
            return (byte)0;
        }

        // Метод, который будет срабатывать при обновлении значения свойства для корректирования значения порта, если оно не подходит.
        private static object CoercePortCallbackMethod(DependencyObject d, object baseValue)
        {
            if ((int)baseValue <= 10000 && (int)baseValue >= 9000)
                return baseValue;
            return (ushort)9050;
        }

       // Метод который будет срабатывать при обновлении значения свойства. (Указывается через метаданные)
        private static void ChangedCallbackMethod(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Application.Current.MainWindow.Title = e.NewValue.ToString();
        }
    }
}