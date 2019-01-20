using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cornhacks2019.Accessors;
using Cornhacks2019.Engines;
using Cornhacks2019.Models;
using NUnit;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class EmailEngineTests
    {
        private static GithubAccessor _githubAccessor = new GithubAccessor();
        EmailEngine _emailEngine = new EmailEngine();
        GithubEngine _githubEngine = new GithubEngine(_githubAccessor);


        [Test]
        public void TestEmailEngine()
        {
            EmailEngine ee = new EmailEngine();
            User user = new User();
            user.Email = "safutterman@outlook.com";
            ee.SendEmail(user);    
        }

        [Test]
        public async Task SendFullEmail()
        {
            User user = new User();
            user.Email = "safutterman@outlook.com";
            user.Preference = new Preference
            {
                IsBeginner = false, 
                Sizes = new List<SizeEnum.Size> { SizeEnum.Size.Medium} 
            }; 

            var dic = await _githubEngine.GetValidIssues(user);
            _emailEngine.CreateEmail(dic);
            _emailEngine.SendEmail(user);
            Assert.Pass();
        }

    }
}
