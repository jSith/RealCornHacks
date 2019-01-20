using Cornhacks2019.Accessors;
using Cornhacks2019.Models;
using Cornhacks2019.Interfaces.EngineInterfaces;
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
            var validRepoIssues = FilterRepositories(user, allRepoIssues);
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

                bool containsTopic = false;

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
                            if (repo.Description.Contains(topic))
                            {
                                containsTopic = true;
                            }
                        }
                    }
                }


                var containsLanguage = false;
                foreach (string language in user.Preference.Languages)
                {
                    if (repo.Languages != null)
                    {
                        if (repo.Languages.Contains(language))
                        {
                            containsLanguage = true;
                        }
                    }
                }

                /*
                var validIssues = new List<Issue>();
                if (user.Preference.IsBeginner)
                {
                    foreach (var issue in repo.Issues)
                    {
                        if (issue.Labels.Contains("good first issue"))
                        {
                            validIssues.Add(issue);
                        }
                    }
                    if (validIssues.Count == 0)
                    {
                        continue;
                    }
                } */ // TODO: figure out encoding

                var goodContributors = false;

                var contributors = repo.NumberOfContributors;

                foreach (var size in user.Preference.Sizes)
                {
                    var range = SizeEnum.GetRange(size);
                    var min = range["min"];
                    var max = range["max"];

                    if (contributors >= min && contributors <= max)
                    {
                        goodContributors = true;
                    }
                }

                // goodContributors && containsTopic && 
                // removed because they were too restrictive; may add later if paginate for more results

                if (containsLanguage)
                {
                    finalRepos.Add(repo);
                }
            }

            if (finalRepos.Count < 3)
            {
                var padding = repos.Take(3 - finalRepos.Count).ToList();
                foreach (var item in padding)
                {
                    finalRepos.Add(item);
                }
            }

            return finalRepos;

        }


    }
}