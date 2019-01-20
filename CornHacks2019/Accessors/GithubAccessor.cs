using Cornhacks2019.Models;
using CornHacks2019.Interfaces.AccessorInterfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cornhacks2019.Accessors
{
    public class GithubAccessor : IGithubAccessor
    {
        private readonly string _githubUrl = "https://api.github.com";

        private static HttpClient _client = new HttpClient();

        public GithubAccessor()
        {
            _client.DefaultRequestHeaders.Add("user-agent", "unl"); 
        }

        public async Task<List<Repository>> GetPublicRepositoriesAsync()
        {
            List<Repository> repo = new List<Repository>(); 
            string url = _githubUrl + "/repositories?per_page=1000";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                repo = await response.Content.ReadAsAsync<List<Repository>>();
            }
            return repo; 
        }
    }
}
