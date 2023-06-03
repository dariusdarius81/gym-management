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
    public class SupplierRepo
    {
        public ObservableCollection<Supplier> GetAllSuppliers()
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("get_all_furnizori", con);
                ObservableCollection<Supplier> result = new ObservableCollection<Supplier>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Supplier a = new Supplier();
                    a.Name = reader[1].ToString();
                    a.Id = (int)reader[0];
                    a.Location = reader[2].ToString();
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
        public void DeleteSupplier(Supplier sup)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("delete_furnizor_by_name_and_location", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_nume", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramID.Value = sup.Name;

            NpgsqlParameter paramID1 = new NpgsqlParameter("p_locatie", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramID1.Value = sup.Location;

            cmd.Parameters.Add(paramID);
            cmd.Parameters.Add(paramID1);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted succesfully!");
            }
            catch
            {
                MessageBox.Show("Deleted succesfully!");
            }

            con.Close();


        }
        public void ModifySupplier(Supplier sup, string loc)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("modify_furnizor_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_id_furnizor", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = sup.Id;

            NpgsqlParameter paramID1 = new NpgsqlParameter("p_locatie", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramID1.Value = loc;

            cmd.Parameters.Add(paramID1);
            cmd.Parameters.Add(paramID);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Modified succesfully!");
            con.Close();

        }
        public void AddSupplier(Supplier sup)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;

            NpgsqlCommand cmd = new NpgsqlCommand("insert_furnizor", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramUserType = new NpgsqlParameter("p_nume", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType.Value = sup.Name;

            NpgsqlParameter paramUserType1 = new NpgsqlParameter("p_locatie", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType1.Value = sup.Location;

            cmd.Parameters.Add(paramUserType);
            cmd.Parameters.Add(paramUserType1);

            con.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Added succesfully!");

            con.Close();
        }
    }
}
