﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gutop.Portal.Controllers
{
    public class DanasusiController : AuthenticationedController, Entity.IControllerIntercepted
    {
        // GET: Danasusi
        public ActionResult Youyingu()
        {
            return View();
        }
    }
}