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
<<<<<<< HEAD:NUnitTestProject1/EmailEngine.cs
            user.Email = "safutterman@outlook.com";
            ee.SendEmail(user);
=======
            user.Email = "jharkendorff@gmail.com";
            ee.SendEmail(user);            
>>>>>>> 972b46408455bc4cadca8c17147517582b89fb2e:NUnitTestProject1/EmailEngineTests.cs
        }

    }
}
