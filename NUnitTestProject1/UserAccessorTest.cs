using System;
using System.Collections.Generic;
using System.Text;
using Cornhacks2019.Accessors;
using NUnit.Framework;

namespace Tests
{ 
    public class UserAccessorTests
    {
        UserAccessor _userAccessor = new UserAccessor();

        [Test]
        public void UserAccessorTest()
        {
            var response = _userAccessor.Select(); 
        }
    }
}
