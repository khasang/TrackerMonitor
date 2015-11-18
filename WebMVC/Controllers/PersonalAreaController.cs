using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models.TrackersViewModels;
using Microsoft.AspNet.Identity;

namespace WebMVC.Controllers
{
    public class PersonalAreaController : BaseController
    {
        // GET: PersonalArea
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            UserProfile model = dbContext.UserProfiles.FirstOrDefault(x => x.UserId == userId);

            if (model == null)
            {
                model = new UserProfile()
                {
                    User = dbContext.Users.FirstOrDefault(x => x.Id == userId),
                    UserId = userId,
                    GPSTrackers = dbContext.GPSTrackers.Where(x => x.OwnerId == userId).ToList()
                };

                dbContext.UserProfiles.Add(model);
                dbContext.SaveChanges();
            }

            return View(model);
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

            GPSTracker tracker = new GPSTracker()
            {
                Id = model.Id,
                Owner = dbContext.Users.Find(User.Identity.GetUserId()),
                //OwnerId = User.Identity.GetUserId()
                Name = model.Name
            };
            
            dbContext.GPSTrackers.Add(tracker);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            GPSTracker tracker = dbContext.GPSTrackers.Find(model.Id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            tracker.Name = model.Name;
            dbContext.SaveChanges();

            return RedirectToAction("Index");
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

            if (tracker.OwnerId != User.Identity.GetUserId())
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

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            dbContext.GPSTrackers.Remove(tracker);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}