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
    public class EquipmentRepo
    {
        public ObservableCollection<Equipment> GetAllEquipments()
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("get_all_echipamente", con);
                ObservableCollection<Equipment> result = new ObservableCollection<Equipment>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int furnizorId = 0;
                    int.TryParse(reader[5].ToString(), out furnizorId);
                    Equipment a = new Equipment();
                    a.Id = (int)reader[0];
                    a.Name = reader[1].ToString();
                    a.Price = (int)reader[2];
                    a.Muscle = reader[3].ToString();
                    a.Quantity = (int)reader[4];
                    a.IdFurnizor = furnizorId;
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
        public void DeleteEq(Equipment ab)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("delete_echipament_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = ab.Id;

            cmd.Parameters.Add(paramID);
            con.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted succesfully!");

            con.Close();
        }
        public void ModifyEq(Equipment eq, string quantity)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("modify_nr_bucati_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = eq.Id;

            NpgsqlParameter paramID1 = new NpgsqlParameter("p_nr_bucati", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID1.Value = int.Parse(quantity);

            cmd.Parameters.Add(paramID1);
            cmd.Parameters.Add(paramID);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Modified succesfully!");
            con.Close();

        }
        public void AddEq(Equipment eq)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;

            NpgsqlCommand cmd = new NpgsqlCommand("insert_echipament", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramUserType = new NpgsqlParameter("p_nume", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType.Value = eq.Name;

            NpgsqlParameter paramUserType1 = new NpgsqlParameter("p_pret", NpgsqlTypes.NpgsqlDbType.Integer);
            paramUserType1.Value = eq.Price;

            NpgsqlParameter paramUserType2 = new NpgsqlParameter("p_grupa_muschi", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType2.Value = eq.Muscle;

            NpgsqlParameter paramUserType3 = new NpgsqlParameter("p_nr_bucati", NpgsqlTypes.NpgsqlDbType.Integer);
            paramUserType3.Value = eq.Quantity;

            NpgsqlParameter paramUserType4 = new NpgsqlParameter("p_id_furnizor", NpgsqlTypes.NpgsqlDbType.Integer);
            paramUserType4.Value = eq.IdFurnizor;

            cmd.Parameters.Add(paramUserType);
            cmd.Parameters.Add(paramUserType1);
            cmd.Parameters.Add(paramUserType2);
            cmd.Parameters.Add(paramUserType3);
            cmd.Parameters.Add(paramUserType4);

            con.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Added succesfully!");

            con.Close();
        }
    }
}
