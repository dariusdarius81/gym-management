using gym_management.Helper;
using gym_management.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gym_management.Repository
{
    public class UtilityRepo
    {
        public ObservableCollection<Utility> GetAllUtil()
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("get_all_payment_utilities", con);
                ObservableCollection<Utility> result = new ObservableCollection<Utility>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Utility a = new Utility();
                    a.Id = (int)reader[0];
                    a.Month = reader[1].ToString();
                    a.Rent = (int)reader[2];
                    a.Util = reader[3].ToString();
                    result.Add(a);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }
        public void DeleteUtil(Utility ab)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("delete_payment_utility", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_id_pay", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = ab.Id;

            cmd.Parameters.Add(paramID);
            con.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted succesfully!");

            con.Close();
        }
        public void AddEq(Utility eq)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;

            NpgsqlCommand cmd = new NpgsqlCommand("add_payment_utility", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramUserType = new NpgsqlParameter("p_luna", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType.Value = eq.Month;

            NpgsqlParameter paramUserType1 = new NpgsqlParameter("p_chirie", NpgsqlTypes.NpgsqlDbType.Integer);
            paramUserType1.Value = eq.Rent;

            NpgsqlParameter paramUserType3 = new NpgsqlParameter("p_utilitati", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType3.Value = eq.Util;


            cmd.Parameters.Add(paramUserType);
            cmd.Parameters.Add(paramUserType1);
            cmd.Parameters.Add(paramUserType3);

            con.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Added succesfully!");

            con.Close();
        }
    }
}
