using System;
using System.Collections.Generic;
using System.Text;
using Cornhacks2019.Accessors;
using Cornhacks2019.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Tests
{ 
    public class UserAccessorTests
    {
        private static IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        UserAccessor _userAccessor = new UserAccessor(_config);

        [Test]
        public void UserAccessorTest()
        {
            var response = _userAccessor.Select(); 
        }

        [Test]
        public void TestInsert()
        {
            UserAccessor userAccessor = new UserAccessor(_config);

            User user = new User();
            user.Email = "email@gmail.com";
            user.Password = "password123";
            user.Preference.IsBeginner = true;
            user.Preference.Languages = new List<string>()
            {
                "Python",
                "C#",
                "Ruby"
            };
            user.Preference.Topics = new List<string>()
            {
                "Machine Learning",
                "Database"
            };
            user.Preference.Sizes = new List<SizeEnum.Size>()
            {
                SizeEnum.Size.Medium
            };

            userAccessor.Insert(user);
        }
    }
}
