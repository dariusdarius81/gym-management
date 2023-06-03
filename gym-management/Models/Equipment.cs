using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gym_management.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Muscle { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int IdFurnizor { get; set; }
    }
}
