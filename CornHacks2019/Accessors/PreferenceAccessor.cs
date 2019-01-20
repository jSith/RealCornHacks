using Cornhacks2019.Interfaces.AccessorInterfaces;
using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Accessors
{
    public class PreferenceAccessor: IPreferenceAccessor
    {
        public List<SizeEnum.Size> GetSizeOptions()
        {
            throw new NotImplementedException(); 
        }

        public List<string> GetTopicOptions()
        {
            throw new NotImplementedException();

        }

        public List<bool> GetBeginnerOptions()
        {
            throw new NotImplementedException();

        }

        public List<string> GetLanguageOptions()
        {
            throw new NotImplementedException();

        }
    }
}
