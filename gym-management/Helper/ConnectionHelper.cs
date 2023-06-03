using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gym_management.Helper
{
    public class ConnectionHelper
    {
        public static NpgsqlConnection connection = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=facultatheobrasov2002;Database=SalaFitness1;");
        public static NpgsqlConnection Connection
        {
            get
            {
                return connection;
            }
        }
    }
}
