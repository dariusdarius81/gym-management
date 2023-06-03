using gym_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gym_management.Converter
{
    public class UserConvert : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null)
            {
                return new User()
                {
                    Nume = values[0].ToString(),
                    Prenume = values[1].ToString(),
                    Password = values[2].ToString(),
                    Contact = ""
                };
            }
            return null;
        }
        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
