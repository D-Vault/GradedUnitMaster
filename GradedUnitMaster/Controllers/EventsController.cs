using GradedUnitMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradedUnitMaster.Controllers
{
    public class EventsController : MainController
    {
       /// <summary>
       /// Displays a list of events within the system
       /// </summary>
       /// <returns>the view along with the list of events</returns>
        public ActionResult Index()
        {
            
            var events = db.Events;

            //Gets all entries in list and transfers them to the view model
            var eventList = from u in events
                            select new EventViewModel()
                            {

                                Id = u.EventID,
                                Business = u.Business,
                                capacity = u.Capacity,
                                name = u.EventName,
                                description = u.Description,
                                location = u.Location,
                                price = u.EventPrice,
                                type = u.Type,
                                restrictions = u.Restrictions,
                                                               
                            };

            ViewBag.isBusiness = IsBusiness();
            return View(eventList.ToList());
        }

         // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
