using Cornhacks2019.Models;
using CornHacks2019.Interfaces.AccessorInterfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cornhacks2019.Accessors
{
    class GithubAccessor : IGithubAccessor
    {
        private readonly string _githubUrl = "https://api.github.com";

        private static HttpClient _client = new HttpClient();

        public GithubAccessor()
        {
        }

        public async Task<List<Repository>> GetPublicRepositoriesAsync()
        {
            string url = _githubUrl + "/repositories";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string repo = await response.Content.ReadAsStringAsync();
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}
