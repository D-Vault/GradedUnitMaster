using GradedUnitMaster.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using What2Do.Data;

namespace GradedUnitMaster.Controllers
{
    public class BusinessesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Businesses
        public ActionResult Index()
        {
            return View(db.Businesses.ToList());
        }

        // GET: Businesses/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var business = this.db.Businesses
                            .Where(b => b.BusinessID==id)
                            .Select(BusinessViewModel.ViewModel)
                            .SingleOrDefault();

           
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        

        

        // GET: Businesses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
