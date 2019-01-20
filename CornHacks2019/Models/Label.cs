using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Models
{
    public class Label
    {
        public string Id { get; set; }
        public string NodeId { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public bool isDefault { get; set; }

    }
}
