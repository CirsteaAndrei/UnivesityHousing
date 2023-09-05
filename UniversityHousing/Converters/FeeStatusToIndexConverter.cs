using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Converters
{
    class FeeStatusToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FeeStatus status)
            {
                return (int)status;
            }
            return 0; // Default index
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            return (FeeStatus)index;
        }
    }
}
