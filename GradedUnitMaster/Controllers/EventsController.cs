using GradedUnitMaster.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        /// Displays the Businesses Created Events
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessEvents()
        {

            string user = User.Identity.GetUserId();

            var events = db.Events;


            //Gets all entries in list and transfers them to the view model
            var eventList = from u in events
                            where u.Business.Id.Equals(user)
                            select new EventViewModel()
                            {

                                Id = u.EventID,
                                capacity = u.Capacity,
                                name = u.EventName,
                                description = u.Description,
                                price = u.EventPrice,
                                type = u.Type,
                                restrictions = u.Restrictions,
                                Business = u.Business
                            };

            ViewBag.DatesAdd = true;
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

        [HttpGet]
        public ActionResult Create()
        {
            var locations = db.Locations;
            var types = db.Types;
            string user = User.Identity.GetUserId();
            

           
            EventInputModel model = new EventInputModel();
            

           model.Locations = from l in locations
                             where l.Business.Id.Equals(user)
                              select new SelectListItem
                              {
                                  Text = l.Street,
                                  Value = l.LocationID.ToString()
                              };
            model.Types = from t in types
                          select new SelectListItem
                          {
                              Text = t.TypeName,
                              Value = t.TypeID.ToString()
                          };
     

            model.Business = db.Businesses.Where(b => b.Id.Equals(user)).SingleOrDefault();

            return View(model);
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
              return RedirectToAction("Create");
            }
            if (IsBusiness())
            {
               
                var LocationFound = db.Locations.Where(l => l.LocationID == model.LocationSelectedID).SingleOrDefault();
                var TypeFound = db.Types.Where(l => l.TypeID == model.TypeSelectedID).SingleOrDefault();
                string user = User.Identity.GetUserId();
                model.Business = db.Businesses.Where(b => b.Id.Equals(user)).SingleOrDefault();

                Event newEvent = new Event()
                {
                    Business = model.Business,
                    Capacity = model.Capacity,
                    Description = model.Description,
                    EventName = model.Name,
                    EventPrice = model.Price,
                    Restrictions = model.Restrictions, 
                    Type = TypeFound, 
                    Location = LocationFound,
                    LocationId = LocationFound.LocationID, 
                    TypeId = TypeFound.TypeID, 
                    Dates = new HashSet<EventDates>()
                };

                db.Events.Add(newEvent);
                db.SaveChanges();

                
                return RedirectToAction("Index");
            }

            return View();
            
        }
        

        /// <summary>
        /// Initial date addition page
        /// </summary>
        /// <param name="id">the id of the event</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddDates(int id)
        {
            EventDatesInputModel model = new EventDatesInputModel();
            var thisEvent = db.Events.SingleOrDefault(e=> e.EventID== id);
            model.Event = thisEvent.EventID;
            return View(model);
        }

        /// <summary>
        /// Adds new dates to the event 
        /// </summary>
        /// <param name="id">the id of the event</param>
        /// <param name="model">the input model for the event date</param>
        /// <returns>The Page view</returns>
        [HttpPost]
        public ActionResult AddDates(EventDatesInputModel model)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddDates", new { id= model.Event });
            }

            

            EventDates eventDate = new EventDates()
            {
                Date = model.EventDate.Date,
                bookings = 0,
                EventId = model.Event
            };

            

            db.EventDates.Add(eventDate);
            db.SaveChanges();



            return RedirectToAction("Index");
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
