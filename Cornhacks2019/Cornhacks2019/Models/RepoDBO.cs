using System;

namespace Cornhacks2019.Models
{
    public class RepoDBO
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public int NumberOfContributors { get; set; }

        public string Issues_Url { get; set; }

        public string Languages_Url { get; set; }

        public string Url { get; set; }

        public Owner Owner { get; set; }

        public string Html_Url { get; set; }
    }
}