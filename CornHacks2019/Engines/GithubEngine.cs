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
        GithubAccessor _githubAccessor = new GithubAccessor();

        public GithubEngine()
        {

        }

        public List<Repository> FilterRepositories(List<Repository> repos, User user)
        {
            List<Repository> finalRepos = new List<Repository>();

            foreach (Repository repo in repos)
            {
                bool descriptionContainsTopic = false;
                foreach (string topic in user.Preference.Topics)
                {
                    if (repo.Description.Contains(topic))
                    {
                        descriptionContainsTopic = true;
                    }
                }

                bool repoSupportsLanguage = false;
                foreach (string language in user.Preference.Languages)
                {
                    repo.Languages.ConvertAll(str => str.ToLower());
                    if (repo.Languages.Contains(language.ToLower()))
                    {
                        repoSupportsLanguage = true;
                    }
                }

                if (descriptionContainsTopic && repoSupportsLanguage)
                {
                    finalRepos.Add(repo);
                }
            }
            return repos;
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
