using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Areas.admin.Controllers
{
    public class GpsTrackersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager;

        public GpsTrackersController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

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

        [HttpGet]
        public ActionResult Create()
        {
            var gpstracker = new GPSTracker();
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить трекер";
            return View("Dialog", gpstracker);
        }

        [HttpPost]
        public ActionResult Create(GPSTracker model)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить трекер";
            if (ModelState.IsValid)
            {
                var gpstracker = new GPSTracker()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Owner = model.Owner,
                    OwnerId = model.OwnerId,
                    PhoneNumber =model.PhoneNumber,
                     GPSTrackerMessages = model.GPSTrackerMessages
                };
                db.GPSTrackers.Add(gpstracker);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return View("Dialog", model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gpstracker = await db.GPSTrackers.FirstAsync(i => i.Id == id);
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (gpstracker == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", gpstracker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GPSTracker gpstracker)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (ModelState.IsValid)
            {
                var modifyGPSTracker = db.GPSTrackers.Find(gpstracker.Id);
                modifyGPSTracker.Id = gpstracker.Id;
                modifyGPSTracker.Name = gpstracker.Name;
                modifyGPSTracker.Owner = gpstracker.Owner;
                modifyGPSTracker.OwnerId = gpstracker.OwnerId;
                modifyGPSTracker.GPSTrackerMessages = gpstracker.GPSTrackerMessages;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Dialog", gpstracker);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gpstracker = await db.GPSTrackers.FirstAsync(i => i.Id == id);
            ViewBag.Selector = ViewSelector.Delete;
            ViewBag.ActionName = "Удалить";
            if (gpstracker == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", gpstracker);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var gpstracker = await db.GPSTrackers.FirstAsync(i => i.Id == id);
            db.GPSTrackers.Remove(gpstracker);
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}