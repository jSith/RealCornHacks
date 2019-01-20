using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Interfaces.AccessorInterfaces
{
    public interface IPreferenceAccessor
    {
        List<SizeEnum.Size> GetSizeOptions();
        List<string> GetTopicOptions();
        List<bool> GetBeginnerOptions();
        List<string> GetLanguageOptions(); 
    }
}
