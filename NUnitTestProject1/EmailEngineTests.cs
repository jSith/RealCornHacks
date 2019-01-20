using System;
using System.Collections.Generic;
using System.Text;
using Cornhacks2019.Engines;
using Cornhacks2019.Models;
using NUnit;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class EmailEngineTests
    { 
        [Test]
        public void TestEmailEngine()
        {
            EmailEngine ee = new EmailEngine();
            User user = new User();
            user.Email = "jharkendorff@gmail.com";
            ee.SendEmail(user);            
        }

    }
}
