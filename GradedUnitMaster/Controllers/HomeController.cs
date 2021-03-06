﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using What2Do.Data;

namespace GradedUnitMaster.Controllers
{
    public class HomeController : MainController
    {

        /// <summary>
        /// Action for the home page of system
        /// </summary>
        /// <returns>a view</returns>
        public ActionResult Index()
        {
                        
            return View();
            
        }

        /// <summary>
        /// Displays details abour What2Do
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Displays What2Do contact details 
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}