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
    public class UserRepository
    {
        public ObservableCollection<User> GetAllUsers()
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("get_all_users", con);
                ObservableCollection<User> result = new ObservableCollection<User>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User u = new User();
                    u.Id = (int)reader[0];
                    u.Nume = reader[1].ToString();
                    u.Prenume = reader[2].ToString();
                    u.Password = reader[3].ToString();
                    u.Contact = reader[4].ToString();
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
        public void AddUser(User user)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;

            NpgsqlCommand cmd = new NpgsqlCommand("insert_user", con);
            cmd.CommandType = CommandType.StoredProcedure; 

            NpgsqlParameter paramUserType = new NpgsqlParameter("p_nume", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUserType.Value = user.Nume;

            NpgsqlParameter paramUsername = new NpgsqlParameter("p_prenume", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramUsername.Value = user.Prenume;

            NpgsqlParameter paramPassword = new NpgsqlParameter("p_parola", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramPassword.Value = user.Password;

            NpgsqlParameter contact = new NpgsqlParameter("p_contact", NpgsqlTypes.NpgsqlDbType.Varchar);
            contact.Value = user.Contact;

            cmd.Parameters.Add(paramUserType);
            cmd.Parameters.Add(paramUsername);
            cmd.Parameters.Add(paramPassword);
            cmd.Parameters.Add(contact);

            con.Open();

            cmd.ExecuteNonQuery();

            MessageBox.Show("Account created successfully!");

            con.Close();
        }
        public void ModifyUserContact(User user, string contact)
        {
            NpgsqlConnection con = ConnectionHelper.Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("modify_user_contact_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter paramID = new NpgsqlParameter("p_user_id", NpgsqlTypes.NpgsqlDbType.Integer);
            paramID.Value = user.Id;

            NpgsqlParameter paramID1 = new NpgsqlParameter("p_contact", NpgsqlTypes.NpgsqlDbType.Varchar);
            paramID1.Value = contact;

            cmd.Parameters.Add(paramID1);
            cmd.Parameters.Add(paramID);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Modified succesfully!");
            con.Close();

        }

        public void GetType(User user)
        {
            ObservableCollection<User> usr = GetAllUsers();
            User u = usr.First(x => x.Password == user.Password && x.Nume == user.Nume && x.Prenume == user.Prenume);
            string type = user.Password;
            if (type == "admin123")
            {
                AdminWindow admin = new AdminWindow();
                admin.Show();
            }else if (u != null)
            {
                MemberWindow member = new MemberWindow(u);
                member.Show();
            }
        }
    }
}
