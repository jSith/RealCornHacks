﻿using Cornhacks2019.Models;
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
        private readonly IEmailEngine _emailEngine; 

        public UserController(ISubscriptionEngine subscriptionEngine)
        {
            _subscriptionEngine = subscriptionEngine;
        }

        [HttpPost]
        public ActionResult<User> Subscribe(User user)
        {
            var subscribedUser = _subscriptionEngine.Subscribe(user);
            _emailEngine.SendDigest(subscribedUser); 
            return subscribedUser;
        }

        [HttpDelete]
        public ActionResult<User> Unsubscribe(User user)
        {
            var unsubscribedUser = _subscriptionEngine.Unsubscribe(user);
            return unsubscribedUser;
        }

    }
}
