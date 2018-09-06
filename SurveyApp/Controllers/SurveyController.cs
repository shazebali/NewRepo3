﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        //
        // GET: /Survey/

        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Survey(int? surveyID)
        {
            return View(surveyID);
        }

    }
}
