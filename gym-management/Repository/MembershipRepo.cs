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
    public class MembershipRepo
    {
        public ObservableCollection<Abonament> GetAllMemberships()
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("get_all_ab", con);
                ObservableCollection<Abonament> result = new ObservableCollection<Abonament>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Abonament a = new Abonament();
                    a.Name = reader[1].ToString();
                    a.Id = (int)reader[0];
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
        public int DeleteMember(Abonament ab)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("delete_abonament_by_type", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_membership_type", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramID.Value = ab.Name;

            cmd.Parameters.Add(paramID);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted succesfully!");

                con.Close();
                return 1;
            }
            catch
            {
                MessageBox.Show("Can't delete ongoing memberships!");

                con.Close();
                return 0;
            }


        }
        public void AddAb(Abonament ab)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;

            NpgsqlCommand cmd = new NpgsqlCommand("insert_abonament", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramUserType = new NpgsqlParameter("p_membership_type", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType.Value = ab.Name;

            cmd.Parameters.Add(paramUserType);

            con.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Added succesfully!");

            con.Close();
        }
    }
}
