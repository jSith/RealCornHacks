using System;
using System.Collections.Generic;
using System.Text;

namespace Cornhacks2019.Models
{
    public class Preference
    {
        public bool IsBeginner { get; set; }
        public List<string> Topics { get; set; }
        public List<string> Languages { get; set; }
        public List<int> Sizes { get; set; }
    }
}
