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
    }
}