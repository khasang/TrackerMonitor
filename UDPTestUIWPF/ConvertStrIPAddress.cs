using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UDPTestUIWPF
{
    public class ConvertStrIPAddress : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ipAddress = value as IPAddress;

            if (ipAddress != null)
            {
                return ipAddress.ToString();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            IPAddress ipAddress;

            if (text != null && IPAddress.TryParse(text, out ipAddress))
            {
                return ipAddress;
            }

            return Binding.DoNothing;
        }
    }
}
