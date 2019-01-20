using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Models
{
    public class SizeEnum {

        public enum Size
        {
            Small = 1, 
            Medium = 2, 
            Large = 3,
        };


        public static Size GetSize(string sizeName)
        {
            if (sizeName == "Small")
            {
                return Size.Small;
            }
            else if (sizeName == "Medium")
            {
                return Size.Medium;
            }
            else if (sizeName == "Large")
            {
                return Size.Large;
            }
            else
            {
                throw new FormatException();
            }

        }

        public static Dictionary<string, int> GetRange(Size size)
        {
            var dict = new Dictionary<string, int>(); 

            if (size == Size.Small)
            {
                dict["min"] = 0;
                dict["max"] = 10; 
            }
            else if (size == Size.Medium)
            {
                dict["min"] = 10;
                dict["max"] = 100; 
            }
            else
            {
                dict["min"] = 100;
                dict["max"] = int.MaxValue; 
            }

            return dict; 
        }
    }
}
