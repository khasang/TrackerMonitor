using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
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
    public class GpsTrackerMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager;

        public GpsTrackerMessagesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: admin/GpsTrackersMessages
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 20;              //количество объектов на странице
            int pageNumber = (page ?? 1);
            var gpsTrackersMessages =await db.GPSTrackerMessages.ToListAsync();
            if (gpsTrackersMessages == null)
            {
                return HttpNotFound();
            }
            return PartialView(gpsTrackersMessages.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var gpsTrackersMessage = new GPSTrackerMessage();
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить сообщение";
            return View("Dialog", gpsTrackersMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GPSTrackerMessage model)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить сообщение";
            if (ModelState.IsValid)
            {
                var gpsTrackersMessage = new GPSTrackerMessage()
                {
                    Id = model.Id,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Time = DateTime.Now,
                    GPSTrackerId = model.GPSTrackerId
                };
                db.GPSTrackerMessages.Add(gpsTrackersMessage);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return View("Dialog", model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gpsTrackersMessage = await db.GPSTrackerMessages.FirstAsync(i => i.Id == id);
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (gpsTrackersMessage == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", gpsTrackersMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GPSTrackerMessage gpsTrackersMessage)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (ModelState.IsValid)
            {
                var modifyMessage = db.GPSTrackerMessages.Find(gpsTrackersMessage.Id);
                modifyMessage.Id = gpsTrackersMessage.Id;
                modifyMessage.Latitude = gpsTrackersMessage.Latitude;
                modifyMessage.Longitude = gpsTrackersMessage.Longitude;
                modifyMessage.Time = gpsTrackersMessage.Time;
                modifyMessage.GPSTrackerId = gpsTrackersMessage.GPSTrackerId;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Dialog", gpsTrackersMessage);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gpsTrackersMessage = await db.GPSTrackerMessages.FirstAsync(i => i.Id == id);
            ViewBag.Selector = ViewSelector.Delete;
            ViewBag.ActionName = "Удалить";
            if (gpsTrackersMessage == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", gpsTrackersMessage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int? id)
        {
            var gpsTrackersMessage = await db.GPSTrackerMessages.FirstAsync(i => i.Id == id);
            db.GPSTrackerMessages.Remove(gpsTrackersMessage);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}