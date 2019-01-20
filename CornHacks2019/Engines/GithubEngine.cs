using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cornhacks2019.Engines
{
    public class GithubEngine
    {
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
            return finalRepos;
        }
    }
}
