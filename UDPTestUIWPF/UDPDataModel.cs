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
            PortProperty = DependencyProperty.Register("Port",
                                                        typeof(int),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(9050, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoercePortCallbackMethod)),
                                                        new ValidateValueCallback(ValidatePortCallbackMethod));
            
            MessageProperty = DependencyProperty.Register("Message",
                                                        typeof(string),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata("Message"));

            Octet1Property = DependencyProperty.Register("Octet1",
                                                        typeof(int),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(192, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            Octet2Property = DependencyProperty.Register("Octet2",
                                                        typeof(int),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(168, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            Octet3Property = DependencyProperty.Register("Octet3",
                                                        typeof(int),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(0, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));

            Octet4Property = DependencyProperty.Register("Octet4",
                                                        typeof(int),
                                                        typeof(UDPDataModel),
                                                        new PropertyMetadata(255, new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceOctetCallbackMethod)),
                                                        new ValidateValueCallback(ValidateOctetCallbackMethod));
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

        public string Octet1
        {
            set { SetValue(Octet1Property, value); }
            get { return (string)GetValue(Octet1Property); }
        }

        public string Octet2
        {
            set { SetValue(Octet2Property, value); }
            get { return (string)GetValue(Octet2Property); }
        }

        public string Octet3
        {
            set { SetValue(Octet3Property, value); }
            get { return (string)GetValue(Octet3Property); }
        }

        public string Octet4
        {
            set { SetValue(Octet4Property, value); }
            get { return (string)GetValue(Octet4Property); }
        }

        // Проверка на валидность введенных данных октета.
        private static bool ValidateOctetCallbackMethod(object value)
        {
            if ((int)value > 255 || (int)value < 0)
                return false;
            return true;
        }

        // Проверка на валидность введенных данных порта.
        private static bool ValidatePortCallbackMethod(object value)
        {
            if ((int)value < 9000 || (int)value > 10000)
                return false;
            return true;
        }

        // Метод, который будет срабатывать при обновлении значения свойства для корректирования значения октета, если оно не подходит.
        private static object CoerceOctetCallbackMethod(DependencyObject d, object baseValue)
        {
            if ((int)baseValue <= 255 && (int)baseValue >= 0)
                return baseValue;
            return 0;
        }

        // Метод, который будет срабатывать при обновлении значения свойства для корректирования значения порта, если оно не подходит.
        private static object CoercePortCallbackMethod(DependencyObject d, object baseValue)
        {
            if ((int)baseValue <= 10000 && (int)baseValue >= 9000)
                return baseValue;
            return 9050;
        }

       // Метод который будет срабатывать при обновлении значения свойства. (Указывается через метаданные)
        private static void ChangedCallbackMethod(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Application.Current.MainWindow.Title = e.NewValue.ToString();
        }
    }
}