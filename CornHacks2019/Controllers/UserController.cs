using CornHacks2019.Interfaces.EngineInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cornhacks2019.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly ISubscriptionEngine _subscriptionEngine;

        public UserController(ISubscriptionEngine subscriptionEngine)
        {
            _subscriptionEngine = subscriptionEngine;
        }

    }
}
