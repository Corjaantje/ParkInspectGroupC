using System;
using System.Globalization;
using System.Windows.Data;

namespace ParkInspectGroupC.Converter
{
    internal class StatusToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "/ParkInspectGroupC;component/Image/StatusImage/" + value + ".png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}