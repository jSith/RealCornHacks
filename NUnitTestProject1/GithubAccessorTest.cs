using NUnit.Framework;
using Cornhacks2019.Accessors;
using Cornhacks2019.Models; 
using CornHacks2019.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        GithubAccessor _githubAccessor = new GithubAccessor();

        [SetUp]
        public void Setup()
        {
        }

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
    }
}