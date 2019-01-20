using Cornhacks2019.Models;
using CornHacks2019.Interfaces.AccessorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cornhacks2019.Accessors
{
    public class GithubAccessor : IGithubAccessor
    {
        private readonly string _githubUrl = "https://api.github.com";
        private readonly string clientId = Guid.NewGuid().ToString();
        private readonly string clientSecret = Guid.NewGuid().ToString();

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
            var dbos = new List<RepoDBO>(); 

            List<Repository> repo = new List<Repository>(); 
            string url = _githubUrl + "/repositories?per_page=200";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                dbos = await response.Content.ReadAsAsync<List<RepoDBO>>();
            }

            foreach (var dbo in dbos)
            {
                var cleanUrl = dbo.Issues_Url.Replace("{/number}", ""); 
                var issues = await GetIssuesAsync(cleanUrl);
                var languages = await GetLanguagesAsync(dbo.Languages_Url);

                repo.Add(new Repository
                {
                    Name = dbo.Name,
                    Description = dbo.Description,
                    NumberOfContributors = dbo.NumberOfContributors,
                    Issues = issues,
                    Url = dbo.Html_Url,
                    Owner = dbo.Owner,
                    Languages = languages.Keys.ToList()
                }); 
            }
            return repo; 
        }

        public async Task<List<Issue>> GetIssuesAsync(string url)
        {
            var issues = new List<Issue>(); 
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                issues = await response.Content.ReadAsAsync<List<Issue>>();
            }
            return issues; 
        }

        public async Task<Dictionary<string, int>> GetLanguagesAsync(string url)
        {
            var languages = new Dictionary<string, int>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                languages = await response.Content.ReadAsAsync<Dictionary<string, int>>();
            }
            return languages;
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
