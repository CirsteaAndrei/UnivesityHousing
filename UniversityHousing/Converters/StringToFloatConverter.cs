using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UniversityHousing.Converters
{
    public class StringToFloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (decimal.TryParse(stringValue, NumberStyles.Float, culture, out decimal result))
                {
                    return result;
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float floatValue)
            {
                return floatValue.ToString("G", culture);
            }
            return string.Empty;
             // Default value or you can throw an exception if you prefer
        }
    }
}
