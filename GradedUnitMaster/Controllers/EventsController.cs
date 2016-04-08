using GradedUnitMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradedUnitMaster.Controllers
{

    /// <summary>
    /// Handles all actions relating to event listing 
    /// </summary>
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
                                    capacity = u.Capacity,
                                    name = u.EventName,
                                    description = u.Description,
                                    price = u.EventPrice,
                                    type = u.Type,
                                    restrictions = u.Restrictions

                                };
          

            ViewBag.isBusiness = IsBusiness();
            return View(eventList.ToList());
            
           
        }

         /// <summary>
         /// Returns a specific event's details
         /// </summary>
         /// <param name="id">the unique id of the event</param>
         /// <returns>the view with the requested event</returns>
        public ActionResult Details(int id)
        {

            var newEvent = db.Events
                .Where(e => e.EventID == id)
                .Select(EventViewModel.ViewModel)
                .SingleOrDefault();
            

            return View(newEvent);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(EventViewModel model)
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
