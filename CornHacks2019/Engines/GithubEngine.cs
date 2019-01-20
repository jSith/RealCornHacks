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
            var allRepoIssues = await _githubAccessor.GetPublicRepositoriesAsync(); 
            var validRepoIssues = FilterRepositories(user,allRepoIssues);
            return validRepoIssues; 
        }

        private List<Repository> FilterRepositories(User user, List<Repository> repos)
        {
            var finalRepos = new List<Repository>(); 

            foreach (Repository repo in repos)
            {
                if (repo.Issues == null)
                {
                    continue; 
                }

                if (finalRepos.Count >= 5)
                {
                    break; 
                }

                if (user.Preference == null)
                {
                    continue; 
                }
                else
                {
                    foreach (string topic in user.Preference.Topics)
                    {
                        if (topic == null)
                        {
                            continue;
                        }
                        else
                        {
                            if (repo.Description == null)
                            {
                                continue; 
                            }
                            if (!repo.Description.Contains(topic))
                            {
                                continue;
                            }
                        }
                    }
                }
                /*
                foreach (string language in user.Preference.Languages)
                {
                    if (repo.Languages != null)
                    {
                        if (!repo.Languages.Contains(language))
                        {
                            continue;
                        }
                    }
                } */

                /*
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
                } */

                if (repo.NumberOfContributors == null)
                {
                    continue; 
                } else
                {
                    var contributors = repo.NumberOfContributors;
                    bool matchedOne = false;

                    foreach (var size in user.Preference.Sizes)
                    {
                        var range = SizeEnum.GetRange(size);
                        var min = range["min"];
                        var max = range["max"];

                        if (contributors >= min && contributors <= max)
                        {
                            matchedOne = true;
                        }
                    }

                    if (!matchedOne)
                    {
                        continue;
                    }
                }



                finalRepos.Add(repo); 
            }

            if (finalRepos.Count < 5)
            {
                finalRepos = repos.Take(5).ToList(); 
            }

            return finalRepos; 
          
        }


    }
}
