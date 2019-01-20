using Cornhacks2019.Interfaces.AccessorInterfaces;
using Cornhacks2019.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Accessors
{
    public class PreferenceAccessor: IPreferenceAccessor
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public PreferenceAccessor(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetSection("Database:ConnectionString").Value;
        }


        public List<SizeEnum.Size> GetSizeOptions()
        {
            var sizes = new List<SizeEnum.Size>(); 

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT SizeName FROM Size;"; 


                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = query;

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    sizes.Add(SizeEnum.GetSize((string)dataReader["SizeName"])); 
                }

                return sizes; 
            }
        }

        public List<string> GetTopicOptions()
        {
            var topic = new List<string>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT TopicName FROM Topic;";


                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = query;

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    topic.Add((string)dataReader["TopicName"]);
                }

                return topic;
            }
        }

        public List<bool> GetBeginnerOptions()
        {
            var options = new List<bool> { true, false };
            return options; 
        }

        public List<string> GetLanguageOptions()
        {
            var languages = new List<string>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT LanguageName FROM Language;";


                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = query;

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    languages.Add((string)dataReader["LanguageName"]);
                }

                return languages;

            }
        }
    }
}

