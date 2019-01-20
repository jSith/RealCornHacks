using System;
using System.Collections.Generic;
using System.Text;

namespace Cornhacks2019.Models
{
    public class User
    {
        public string Email { get; set; }

        public Preference Preference { get; set; }

        public string Password { get; set; }

    }
}
