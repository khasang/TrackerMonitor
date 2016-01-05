using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Areas.admin.Controllers
{
    public class GpsTrackersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: admin/GpsTrackers
        public async Task<ActionResult> Index()
        {
            var gpsTrackers = db.GPSTrackers;
            if (gpsTrackers == null)
            {
                return HttpNotFound();
            }
            return PartialView(await gpsTrackers.ToListAsync());
        }
    }
}