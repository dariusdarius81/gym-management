using gym_management.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gym_management.Converter
{
    public class UtilityConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null)
            {
                int nr = 0;
                int.TryParse(values[1].ToString(), out nr);
                return new Utility()
                {
                    Month = values[0].ToString(),
                    Rent = nr,
                    Util = values[2].ToString()
                };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
