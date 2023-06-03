using gym_management.Models;
using gym_management.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gym_management.Converter
{
    public class EquipmentConvert : IMultiValueConverter
    {
        SupplierRepo er = new SupplierRepo();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null)
            {
                ObservableCollection<Supplier> equipment = new ObservableCollection<Supplier>();
                equipment = er.GetAllSuppliers();
                int id = 0;
                foreach (var sup in equipment)
                    if (sup.Name == values[4].ToString())
                    {
                        id = sup.Id;
                        return new Equipment()
                        {
                            Name = values[0].ToString(),
                            Price = int.Parse(values[1].ToString()),
                            Muscle = values[3].ToString(),
                            Quantity = int.Parse(values[2].ToString()),
                            IdFurnizor = id,
                        };
                    }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
