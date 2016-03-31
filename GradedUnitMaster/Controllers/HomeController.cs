using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using What2Do.Data;

namespace GradedUnitMaster.Controllers
{
    public class HomeController : MainController
    {

        
        public ActionResult Index()
        {
            this.IsBusiness();
            this.IsCustomer();
            this.IsStaff();
            
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}