using Cornhacks2019.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Cornhacks2019.Accessors
{
    public class UserAccessor
    {

        private SqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;
        private string _connectionString;

        public UserAccessor()
        {
            _server = "localhost";
            _database = "Cornhacks";
            _uid = "root";
            _password = "lHarkendorff13M";
            _connectionString = "SERVER=" + _server + ";" + "DATABASE=" +
                _database + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";
        }

        public void Connect()
        {

        }

        public List<User> Select()
        {
            List<User> users = new List<User>(); 
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Select * from User";
                MySqlCommand cmd = connection.CreateCommand();          
                cmd.CommandText = query;

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    users.Add(new User
                    {
                        Email = (string)dataReader["Email"],
                        Password = (string)dataReader["Password"],
                        IsBeginner = (bool)dataReader["IsBeginner"]
                    }); 
                }
            }

            return users; 
        }


    }
}
