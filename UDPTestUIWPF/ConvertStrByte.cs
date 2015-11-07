using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UDPTestUIWPF
{
    public class ConvertStrByte : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return byte.Parse(value.ToString());
            }
            catch
            {
                return (byte)0;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
