﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountSystem.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult PageNotFound()
        {
            return View();
        }
    }
}