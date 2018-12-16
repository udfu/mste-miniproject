using System;
using System.Globalization;
using System.Windows.Data;

namespace AutoReservation.UI
{
    public class IsIntEqualEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null) return false;

            if (value.GetType().IsEnum)
            {
                return int.Parse((string) parameter) == (int)value;
            }
            return false;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}