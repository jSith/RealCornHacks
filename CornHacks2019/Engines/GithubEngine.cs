using Cornhacks2019.Accessors;
using Cornhacks2019.Models;
﻿using CornHacks2019.Interfaces.EngineInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cornhacks2019.Engines
{
    public class GithubEngine : IGithubEngine
    {
        GithubAccessor _githubAccessor;

        public GithubEngine(GithubAccessor githubAccessor)
        {
            _githubAccessor = githubAccessor;
        }

        public async Task<List<Repository>> GetValidIssues(User user)
        {
            // var allRepoIssues = await GetRepoIssues();
            var repos = await _githubAccessor.GetPublicRepositoriesAsync(); 
            var validRepoIssues = FilterRepositories(user, repos);
            return validRepoIssues; 
        }

        private List<Repository> FilterRepositories(User user, List<Repository> repos)
        {
            SortedDictionary<Repository, Issue> finalRepos = new SortedDictionary<Repository, Issue>();
            List<Repository> finrep = new List<Repository>(); 

            foreach (Repository repo in repos)
            {
                if (finrep.Count == 5)
                {
                    break; 
                }

                var contributors = repo.NumberOfContributors;
                bool matchedOne = false;

                foreach (var size in user.Preference.Sizes)
                {
                    var range = SizeEnum.GetRange(size);
                    var min = range["min"];
                    var max = range["max"];

                    if (contributors > min && contributors < max)
                    {
                        matchedOne = true;
                    }
                }

                if (!matchedOne)
                {
                    continue;
                }

                finrep.Add(repo); 
            }

            if (finrep.Count < 6)
            {
                var diff = 6 - finrep.Count;
                finrep = repos.Take(6).ToList(); 
            }
            return finrep; 
        }


        private async Task<Dictionary<Repository, Dictionary<Issue, List<string>>>> GetRepoIssues()
        {
            var repos = await _githubAccessor.GetPublicRepositoriesAsync(); 
            var dict = new Dictionary<Repository, Dictionary<Issue, List<string>>>(); 

            foreach (var repo in repos)
            {
                var issues = await _githubAccessor.GetIssuesAsync(repo); 
                var smallerDict = new Dictionary<Issue, List<string>>(); 

                foreach (var issue in issues)
                {
                    var labels = await _githubAccessor.GetIssueLabels(repo, issue.Id);
                    smallerDict[issue] = labels; 
                }
                dict[repo] = smallerDict; 
            }
            return dict; 
        }
    }
}
