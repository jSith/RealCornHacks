using Cornhacks2019.Interfaces.AccessorInterfaces;
using Cornhacks2019.Models;
using Cornhacks2019.Interfaces.EngineInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cornhacks2019.Engines
{
    public class PreferenceEngine : IPreferenceEngine
    {
        private IPreferenceAccessor _preferenceAccessor; 

        public PreferenceEngine (IPreferenceAccessor preferenceAccessor)
        {
            _preferenceAccessor = preferenceAccessor; 
        }

        public Preference GetAllOptions()
        {
            var allSizes = _preferenceAccessor.GetSizeOptions();
            var allTopics = _preferenceAccessor.GetTopicOptions();
            var allLanguages = _preferenceAccessor.GetLanguageOptions();
            return new Preference
            {
                Sizes = allSizes,
                Topics = allTopics,
                Languages = allLanguages
            }; 
        }
    }
}