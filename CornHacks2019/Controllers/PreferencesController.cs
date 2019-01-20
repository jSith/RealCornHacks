using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CornHacks2019.Interfaces.EngineInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cornhacks2019.Controllers
{
    [Route("api/preferences")]
    public class PreferencesController : Controller
    {
        private readonly IPreferenceEngine _preferenceEngine;

        public PreferencesController(IPreferenceEngine preferenceEngine)
        {
            _preferenceEngine = preferenceEngine;
        }

        [HttpGet]
        public ActionResult GetPreferenceOptions()
        {
            throw new NotImplementedException();

        }
    }
}