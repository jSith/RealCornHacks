using Cornhacks2019.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cornhacks2019.Accessors
{
    public class UserAccessor
    {

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
                string query = @"SELECT u.Email, u.Password, u.IsBeginner, topic.TopicName, language.LanguageName, size.SizeName FROM
                                   User AS u

                            LEFT JOIN(SELECT ut.UserId, ut.TopicId, t.TopicName FROM UserTopic AS ut

                                        INNER JOIN Topic AS t ON ut.TopicId = t.TopicId)
                                        AS topic ON u.UserId = topic.UserId

                            LEFT JOIN(SELECT ul.UserId, ul.LanguageId, l.LanguageName FROM UserLanguage AS ul
                                        INNER JOIN Language AS l ON ul.LanguageId = l.LanguageId)
                                        AS language ON u.UserId = language.UserId


                            LEFT JOIN(SELECT us.UserId, us.SizeId, s.SizeName FROM UserSize AS us
                                        INNER JOIN Size AS s ON us.SizeId = s.SizeId)
                                        AS size ON u.UserId = size.UserId;";


                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = query;

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    users.Add(new User
                    {
                        Email = (string)dataReader["Email"],
                        Password = (string)dataReader["Password"],
                        Preference = new Preference
                        {
                            IsBeginner = (bool)dataReader["IsBeginner"],
                            Topics = new List<string> { Convert.IsDBNull(dataReader["TopicName"]) ?
                                null : (string)dataReader["TopicName"] },
                            Languages = new List<string> { Convert.IsDBNull(dataReader["LanguageName"]) ? 
                                null : (string)dataReader["LanguageName"] },
                            Sizes = new List<SizeEnum.Size> {SizeEnum.GetSize((string)dataReader["SizeName"]) }
                        }
                    });
                }
            }

            var userGroups = users.GroupBy(x => x.Email);
            List<User> cleanedUsers = new List<User>();

            foreach (var userGroup in userGroups)
            {
                var cleanedPreferences = new Preference
                {
                    Languages = userGroup.SelectMany(x => x.Preference.Languages).Distinct().ToList(),
                    Topics = userGroup.SelectMany(x => x.Preference.Topics).ToList().Distinct().ToList(), 
                    Sizes = userGroup.SelectMany(x => x.Preference.Sizes).Distinct().ToList()
                };
                cleanedUsers.Add(new User
                {
                    Email = userGroup.Key,
                    Password = userGroup.First().Password,
                    Preference = cleanedPreferences
                }); 
            }

            return cleanedUsers;
        }

    }
}
