using Cornhacks2019.Models;
using CornHacks2019.Interfaces.AccessorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Issue>> GetIssuesAsync(Repository repo)
        {
            List<Issue> issues = new List<Issue>();
            string url = _githubUrl + "/repos/" + repo.Owner.Login + "/" + repo.Name + "/issues";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                issues = await response.Content.ReadAsAsync<List<Issue>>();
            }
            return issues;
        }

        public async Task<List<Repository>> GetPublicRepositoriesAsync()
        {
            List<Repository> repo = new List<Repository>(); 
            string url = _githubUrl + "/repositories";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                repo = await response.Content.ReadAsAsync<List<Repository>>();
            }
            return repo; 
        }

        public async Task<List<string>> GetIssueLabels(Repository repo, int issueId)
        {
            string url = $"{_githubUrl}/repositories/{repo.Owner.Login}/{repo.Name}/issues/{issueId}/labels";
            var response = await _client.GetAsync(url);
            var labelNames = new List<string>(); 

            if (response.IsSuccessStatusCode)
            {
                var gitIssueLabels = await response.Content.ReadAsAsync<List<Label>>();
                labelNames = gitIssueLabels.Select(x => x.name).ToList(); 
            }

            return labelNames; 

        }
    }
}
