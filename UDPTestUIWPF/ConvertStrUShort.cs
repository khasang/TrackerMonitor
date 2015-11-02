using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UDPTestUIWPF
{
    public class ConvertStrUShort : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((ushort)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            try
            {
                return ushort.Parse(value.ToString());
            }
            catch
            {
                return (ushort)0;
            }
        }
    }
}
