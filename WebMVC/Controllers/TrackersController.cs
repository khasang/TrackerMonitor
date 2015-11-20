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
        public ActionResult Index(ICollection<GPSTracker> trackers)
        {
            ICollection<GPSTrackerMessage> trackerMasseges = new List<GPSTrackerMessage>();

            foreach(GPSTracker tracker in trackers)
            {
                trackerMasseges.Add(tracker.GPSTrackerMessages.Last());
            }

            return View(trackerMasseges);
        }
        
               
    
    }
}