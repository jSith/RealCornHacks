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
        GithubAccessor _githubAccessor = new GithubAccessor();
        GithubEngine _githubEngine = new GithubEngine();
        EmailEngine _emailEngine = new EmailEngine();

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

            var repositories = await _githubAccessor.GetPublicRepositoriesAsync();
            //repositories = _githubEngine.FilterRepositories(repositories, user);
            var dic = await _githubEngine.CreateRepositoryIssueDictionary(repositories);
            _emailEngine.CreateEmail(dic);
            _emailEngine.SendEmail(user);
            Assert.Pass();
        }

    }
}
