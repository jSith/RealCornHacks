using Cornhacks2019.Models;
using MySql.Data.MySqlClient;
﻿using CornHacks2019.Interfaces.AccessorInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Cornhacks2019.Accessors
{
    public class UserAccessor : IUserAccessor
    {

        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public UserAccessor(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetSection("Database:ConnectionString").Value;
        }

        public User Insert(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                /* Get User table query */
                cmd.CommandText =
                    @"INSERT INTO User (Email, Password, IsBeginner)
                      VALUES (@Email, @Password, @IsBeginner);";
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@IsBeginner", user.Preference.IsBeginner);

                cmd.CommandText = @"SELECT UserId FROM User
                    WHERE Email = @Email"; 

                int userId = (int)cmd.ExecuteScalar();

                foreach (string language in user.Preference.Languages)
                {
                    /* Language table query */
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        @"SELECT LanguageId FROM Language
                            WHERE LanguageName = @LanguageName;";
                    cmd.Parameters.AddWithValue("@LanguageName", language);
                    int languageId = (int)cmd.ExecuteScalar();

                    /* UserLanguage table query */
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        @"INSERT INTO UserLanguage (UserId, LanguageId)
                          VALUES (@UserId, @LanguageId);";
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@LanguageId", languageId);
                    cmd.ExecuteNonQuery();
                }

                foreach (string topic in user.Preference.Topics)
                {
                    /* Topic table query */
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        @"SELECT TopicId FROM Topic
                          WHERE TopicName = @TopicName;";
                    cmd.Parameters.AddWithValue("@TopicName", topic);
                    int topicId = (int)cmd.ExecuteScalar();

                    /* UserTopic table query */
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        @"INSERT INTO UserTopic (UserId, TopicId)
                          VALUES (@UserId, @TopicId);";
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@TopicName", topicId);
                    cmd.ExecuteNonQuery();
                }
                
                foreach (SizeEnum.Size size in user.Preference.Sizes)
                {
                    /* Size table query */
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        @"SELECT SizeId FROM Size
                            WHERE SizeName = @SizeName;";
                    cmd.Parameters.AddWithValue("@SizeName", size);
                    int sizeId = (int)cmd.ExecuteScalar();

                    /* UserSize table query */
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        @"INSERT INTO UserSize (UserId, SizeId)
                          VALUES (@UserId, @SizeId);";
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@SizeId", sizeId);               
                }
            }
            return user;


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

        public User Delete (User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT UserId FROM User WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", user.Email); 

                int userId = (int)cmd.ExecuteScalar();
                cmd.Parameters.AddWithValue("@UserId", userId);

                cmd.CommandText = "DELETE FROM UserLanguage WHERE UserId = @UserId";
                cmd.ExecuteNonQuery(); 

                cmd.CommandText = "DELETE FROM UserTopic WHERE UserId = @UserId";
                cmd.ExecuteNonQuery(); 

                cmd.CommandText = "DELETE FROM UserSize WHERE UserId = @UserId";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM User WHERE UserId = @UserId"; 
            }
            return user;
        }

    }
}
