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
    public class StudentConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null)
            {
                var student = new Student()
                {
                    FirstName = values[0].ToString(),
                    LastName = values[1].ToString(),
                    CNP = values[3].ToString(),
                    Faculty = values[4].ToString(),
                };
                if (values[4] is FeeStatus feeStatus)
                {
                    student.FeeStatus = feeStatus;
                }
                return student;
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
