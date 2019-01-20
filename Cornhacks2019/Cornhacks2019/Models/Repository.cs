using System;
using System.Collections.Generic;
using System.Text;

namespace Cornhacks2019.Models
{
    public class Repository
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public int NumberOfContributors { get; set; }
         
        public List<Issue> Issues { get; set; }

        public List<string> Languages { get; set; }

        public string Url { get; set; }

        public Owner Owner { get; set; }
    }

    public class Owner
    {
        public string Login { get; set; }

        public string Avatar_Url { get; set; }
    }
}
