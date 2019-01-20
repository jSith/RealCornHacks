using System;
using System.Collections.Generic;
using System.Text;
using Cornhacks2019.Accessors;
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
    }
}
