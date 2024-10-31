using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HydroTaskpane2.Converters
{
    #region converter methods

    public class BooleanToVisibilityConverter : IValueConverter
    {
        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo language)
        {
            return GetVisibility(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }

    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
                return 0.7 * System.Convert.ToDouble(value);


            string[] split = parameter.ToString().Split('.');
            double parameterDouble = System.Convert.ToDouble(split[0]) + System.Convert.ToDouble(split[1]) / (Math.Pow(10, split[1].Length));
            return System.Convert.ToDouble(value) * parameterDouble;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Don't need to implement this
            return null;
        }
    }

    #endregion
}
