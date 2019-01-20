using Cornhacks2019.Accessors;
using Cornhacks2019.Models;
﻿using CornHacks2019.Interfaces.EngineInterfaces;
using System;
using System.Collections.Generic;
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

        public List<Repository> FilterRepositories(List<Repository> repos, User user)
        {
            List<Repository> finalRepos = new List<Repository>();

            foreach (Repository repo in repos)
            {
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

                 finalRepos.Add(repo);
            }
            return finalRepos;
        }

        public async Task<Dictionary<Issue, List<string>>> GetIssueLabels(Repository repo, List<Issue> issues)
        {
            var dict = new Dictionary<Issue, List<string>>(); 
            foreach (var issue in issues)
            {
                var labels = await _githubAccessor.GetIssueLabels(repo, issue.Id);
                dict[issue] = labels; 
            }
            return dict; 
        }

        public async Task<Dictionary<int, KeyValuePair<Repository, Issue>>> CreateRepositoryIssueDictionary(List<Repository> repos)
        {
            Dictionary<int, KeyValuePair<Repository, Issue>> dictionary = new Dictionary<int, KeyValuePair<Repository, Issue>>();

            for (int i = 0; i < 5; i++)
            {            
                List<Issue> issues = await _githubAccessor.GetIssuesAsync(repos[i]);
                KeyValuePair<Repository, Issue> keyValuePair = new KeyValuePair<Repository, Issue>(repos[i], issues[0]);

                dictionary.Add(i, keyValuePair);
            }

            return dictionary;
        }
    }
}
