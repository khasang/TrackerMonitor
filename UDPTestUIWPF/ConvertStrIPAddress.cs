using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UDPTestUIWPF
{
    public class ConvertStrIPAddress : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                
                return ((IPAddress)value).ToString();
            }
            catch
            {
                return "192.168.0.255";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {

                return IPAddress.Parse((string)value);
            }
            catch
            {
                return new IPAddress(new byte[] { 192, 168, 0, 255 });
            }
            
        }
    }
}
