using NUnit.Framework;
using Cornhacks2019.Accessors;
using Cornhacks2019.Models; 
using CornHacks2019.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cornhacks2019.Engines;

namespace Tests
{
    public class Tests
    {
        private static GithubAccessor _githubAccessor = new GithubAccessor();
        GithubEngine _githubEngine = new GithubEngine(_githubAccessor);

        [Test]
        public async Task GithubAccessTestAsync()
        {
            var response = await _githubAccessor.GetPublicRepositoriesAsync();
            Assert.Pass();  
        }

        [Test]
        public async Task GetIssuesTestAsync()
        {
            Repository repo = new Repository();
            Owner owner = new Owner();
            owner.Login = "jbhuang0604";
            repo.Owner = owner;
            repo.Name = "awesome-computer-vision";
            var response = await _githubAccessor.GetIssuesAsync(repo);
            Assert.Pass();
        }

        [Test]
        public async Task TestFullApi()
        {
            User user = new User();
            var repos = _githubEngine.GetValidIssues(user); 
            Assert.Pass();
        }
    }
}