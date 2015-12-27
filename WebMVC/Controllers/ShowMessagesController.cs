using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class ShowMessagesController : BaseController
    {

        UserManager<ApplicationUser> userManager;

        public ShowMessagesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
        }


        // GET: ShowMessagesController
        public ActionResult Index()
        {
            string ownerId = User.Identity.GetUserId();

            SelectList trackers = new SelectList(dbContext.UserProfiles.Find(ownerId).GPSTrackers, "Id", "Name");

            ViewBag.trackers = trackers;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string ownerId, FilterMessageViewModel filterMessage)
        {
            ownerId = User.Identity.GetUserId();

            SelectList trackers = new SelectList(dbContext.UserProfiles.Find(ownerId).GPSTrackers, "Id", "Name");

            ViewBag.trackers = trackers;

            filterMessage.Messages = dbContext
                                    .GPSTrackers
                                    .Find(filterMessage
                                    .Id)
                                    .GPSTrackerMessages
                                    .Where(x => (x.Time < filterMessage.SecondDate && x.Time > filterMessage.FirstDate))
                                    .ToList();

            ViewBag.messages = filterMessage.Messages;
            return View(/*filterMessage*/);
        }
    }
}
