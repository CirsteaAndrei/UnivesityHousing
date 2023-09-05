using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Converters
{
    public class StudentNameOrFreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<Student> students && int.TryParse(parameter as string, out int index) && index < students.Count)
            {
                return students[index].LastName;
            }
            return "Free";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
