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

        public async Task<SortedDictionary<Repository, Issue>> GetValidIssues(User user)
        {
            var allRepoIssues = await GetRepoIssues();
            var validRepoIssues = FilterRepositories(user, allRepoIssues);
            return validRepoIssues; 
        }

        private SortedDictionary<Repository, Issue> FilterRepositories(User user, Dictionary<Repository, Dictionary<Issue, List<string>>> issueLabels)
        {
            SortedDictionary<Repository, Issue> finalRepos = new SortedDictionary<Repository, Issue>();

            foreach (Repository repo in issueLabels.Keys)
            {
                if (finalRepos.Keys.Count >= 5)
                {
                    break; 
                }

                foreach (string topic in user.Preference.Topics)
                {
                    if (!repo.Description.Contains(topic))
                    {
                        continue;
                    }
                }

                foreach (string language in user.Preference.Languages)
                {
                    repo.Languages.ConvertAll(str => str.ToLower());
                    if (!repo.Languages.Contains(language.ToLower()))
                    {
                        continue;
                    }
                }


                var validIssues = new List<Issue>();
                if (user.Preference.IsBeginner)
                {
                    foreach (var issue in issueLabels[repo].Keys)
                    {
                        if (issueLabels[repo][issue].Contains("good first issue"))
                        {
                            validIssues.Add(issue);
                        }
                    }
                    if (validIssues.Count == 0)
                    {
                        continue;
                    }
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

                finalRepos[repo] = user.Preference.IsBeginner ? validIssues.First() : issueLabels[repo].Keys.First();
            }

            return finalRepos;
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
