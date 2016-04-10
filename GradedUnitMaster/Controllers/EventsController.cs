using GradedUnitMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using What2Do.Data;

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
                                    type = u.Type ,
                                    restrictions = u.Restrictions, 
                                    Business = u.Business                                   
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

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="model">The Data to be inputted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(EventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (IsBusiness())
            {

                //Creating new event and assgning its values to the ones in putted
                Event newEvent = new Event()
                {
                    Business = model.Business,
                    Capacity = model.Capacity,
                    EventName = model.Name,
                    EventPrice = model.Price,
                    Description = model.Description,
                    Restrictions = model.Restrictions,
                    Type = model.Type,
                    Location = model.Location
                };

                this.db.Events.Add(newEvent);

                return RedirectToAction("AddDates", newEvent.EventID);
            }
            
        }


        public ActionResult AddDates(int id)
        {

            return View();
        }

        public ActionResult AddDates(int id, EventDatesInputModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
           this.db.Events.Where(e => e.EventID == id)
                .SingleOrDefault().Dates.Add(new EventDates()
            {
                bookings = 0,
                Date = model.Date
            });

            db.SaveChanges();

            

            return View();
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
