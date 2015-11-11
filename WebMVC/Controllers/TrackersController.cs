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
    [Authorize]
    public class TrackersController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
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

        public ActionResult Edit(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dbContext.GPSTrackers.Find(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            EditViewModel model = new EditViewModel();

            model.Id = tracker.Id;
            model.Name = tracker.Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            GPSTracker tracker = dbContext.GPSTrackers.Find(model.Id);

            if(tracker == null)
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
        public ActionResult Delete()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}