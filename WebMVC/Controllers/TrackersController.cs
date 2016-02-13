using DAL.Entities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC.Json;
using WebMVC.Models.TrackersViewModels;

namespace WebMVC.Controllers
{
    [Authorize]
    public class TrackersController : BaseController
    {
        public string GetTrackers(string id1, string id2, string id3)
        {
            var messages = new List<GPSTrackerMessage>();
            messages.Add(new GPSTrackerMessage()
                {
                    GPSTrackerId = id1 + "0",
                    Latitude = 54.69873893333814,
                    Longitude = 52.34677000002305
                });
            messages.Add(new GPSTrackerMessage()
                {
                    GPSTrackerId = id2 + "1",
                    Latitude = 42.69873893333814,
                    Longitude = 52.34677000002305
                });
            messages.Add(new GPSTrackerMessage()
                {
                    GPSTrackerId = id3+ "2",
                    Latitude = 99.69873893333814,
                    Longitude = 52.34677000002305
                });

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter
            {
                CamelCaseText = true
            });

            var json = JsonConvert.SerializeObject(messages, Formatting.None, settings);

            return json;
        }

        public ActionResult Index(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dataManager.GPSTrackers.GetById(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            ICollection<GPSTrackerMessage> trackerMasseges = dataManager.GPSTrackerMessages
                                                                        .GetMessagesByTrackerId(id)
                                                                        .OrderByDescending(m => m.Id)
                                                                        .Take(10)
                                                                        .ToList();

            if (trackerMasseges.Count() == 0)
                trackerMasseges.Add(new GPSTrackerMessage()
                {
                    GPSTrackerId = id,
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

            GPSTracker tracker = dataManager.GPSTrackers.GetById(model.Id);

            if (tracker != null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tracker = new GPSTracker()
            {
                Id = model.Id,
                Owner = dataManager.Users.GetById(currenUserId).UserProfile,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name
            };

            dataManager.GPSTrackers.Add(tracker);
            dataManager.Save();

            return new JsonCamelCaseResult(model, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dataManager.GPSTrackers.GetById(id);

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

            GPSTracker tracker = dataManager.GPSTrackers.GetById(model.Id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            tracker.PhoneNumber = model.PhoneNumber;
            tracker.Name = model.Name;

            dataManager.Save();

            return new JsonCamelCaseResult(model, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult ConfirmDelete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dataManager.GPSTrackers.GetById(id);

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

            GPSTracker tracker = dataManager.GPSTrackers.GetById(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            dataManager.GPSTrackers.Delete(tracker);
            dataManager.Save();

            return Json(id);
        }

        public ActionResult GetLastLocation(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            GPSTracker tracker = dataManager.GPSTrackers.GetById(id);

            if (tracker == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (tracker.Owner.User.Id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            GPSTrackerMessage message = dataManager.GPSTrackerMessages.GetMessagesByTrackerId(id).OrderBy(m => m.Time).First();
            message.GPSTracker = null;

            return new JsonCamelCaseResult(message, JsonRequestBehavior.AllowGet);
        }



    }
}