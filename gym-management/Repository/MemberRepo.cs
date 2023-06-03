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
    public class MemberRepo
    {
        public ObservableCollection<Member> GetAllMembers()
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("get_all_members", con);
                ObservableCollection<Member> result = new ObservableCollection<Member>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Member u = new Member();
                    u.UserId = (int)reader[0];
                    u.Id = (int)reader[1];
                    u.MemebershipId = (int)reader[2];
                    result.Add(u);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteMember(User user)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("delete_member_and_user", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_user_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = user.Id;

            cmd.Parameters.Add(paramID);
            con.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Deleted succesfully!");
            con.Close();

        }
        public void ModifyMember(User user, Abonament ab)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("modify_membership_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_membership_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = ab.Id;

            NpgsqlParameter paramID1 = new NpgsqlParameter("p_user_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID1.Value = user.Id;

            cmd.Parameters.Add(paramID1);
            cmd.Parameters.Add(paramID);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Modified succesfully!");
            con.Close();

        }
        public void AddMember(User user, Abonament abonament)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;

            NpgsqlCommand cmd = new NpgsqlCommand("insert_member", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramUserType = new NpgsqlParameter("p_user_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramUserType.Value = user.Id;

            NpgsqlParameter paramUsername = new NpgsqlParameter("p_membership_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramUsername.Value = abonament.Id;

            cmd.Parameters.Add(paramUserType);
            cmd.Parameters.Add(paramUsername);

            con.Open();

            cmd.ExecuteNonQuery();

            MessageBox.Show("Added succesfully!");

            con.Close();
        }
    }
}
