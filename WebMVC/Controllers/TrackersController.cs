using DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models.TrackersViewModels;

namespace WebMVC.Controllers
{
    [Authorize]
    public class TrackersController : BaseController
    {
        public ActionResult Index(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            ICollection<GPSTrackerMessage> trackerMasseges = dbContext.GPSTrackerMessages
                                                                        .Where(m => m.GPSTrackerId == id)
                                                                        .OrderByDescending(m => m.Id)
                                                                        .Take(10)
                                                                        .ToList();

            if (trackerMasseges.Count() == 0)
                trackerMasseges.Add(new GPSTrackerMessage()
                {
                    GPSTrackerId = "111111",
                    Latitude = 55.69873893333814,
                    Longitude = 52.34677000002305
                });

            return View(trackerMasseges);
        }

        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var currenUserId = User.Identity.GetUserId();

            GPSTracker tracker = new GPSTracker()
            {
                Id = model.Id,
                Owner = dbContext.Users.Find(currenUserId).UserProfile,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name
            };

            dbContext.GPSTrackers.Add(tracker);
            dbContext.SaveChanges();

            return PartialView("~/Views/Trackers/TrackerListNode.cshtml", tracker);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            EditViewModel model = new EditViewModel()
            {
                Id = tracker.Id,
                PhoneNumber = tracker.PhoneNumber,
                Name = tracker.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            GPSTracker tracker = dbContext.GPSTrackers.Find(model.Id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            tracker.PhoneNumber = model.PhoneNumber;
            tracker.Name = model.Name;

            dbContext.SaveChanges();

            return PartialView("TrackerListNode", tracker);
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult ConfirmDelete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            return View(tracker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            dbContext.GPSTrackers.Remove(tracker);
            dbContext.SaveChanges();

            return Json(id);
        }



    }
}