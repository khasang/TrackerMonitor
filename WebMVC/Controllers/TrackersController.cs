using DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models.TrackersViewModels;

namespace WebMVC.Controllers
{
    //[Authorize]
    public class TrackersController : BaseController
    {
        public ActionResult Index()
        {
            List<GPSTracker> trackers = new List<GPSTracker>()
            {
                new GPSTracker() { Id = "1", Name = "Автомобиль" },
                new GPSTracker() { Id = "2", Name = "Мотоцикл" },
                new GPSTracker() { Id = "3", Name = "Велосипед" }
            };

            return View(trackers);
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

            GPSTracker tracker = new GPSTracker();
            tracker.Name = model.Name;
            tracker.OwnerId = User.Identity.GetUserId();

            dbContext.GPSTrackers.Add(tracker);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            EditViewModel model = new EditViewModel()
                                    {
                                        Id = tracker.Id,
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

            if(tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            tracker.Name = model.Name;
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult ConfirmDelete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            return View(tracker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            dbContext.GPSTrackers.Remove(tracker);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}