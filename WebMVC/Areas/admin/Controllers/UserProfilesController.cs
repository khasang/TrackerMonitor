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
    public class UserProfilesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager;

        public UserProfilesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: admin/UserProfiles
        /// <summary>
        /// Возвратит нам всех профайлы пользователей.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var userProfiles = db.UserProfiles;
            if (userProfiles == null)
            {
                return HttpNotFound();
            }
            return View(await userProfiles.ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var profile = new UserProfile();
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить профайл";
            return View("Dialog", profile);
        }

        [HttpPost]
        public ActionResult Create(UserProfile model)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить профайл";
            if (ModelState.IsValid)
            {
                var profile = new UserProfile()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    User = model.User,
                    GPSTrackers = model.GPSTrackers                    
                };
                db.UserProfiles.Add(profile);
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
            var profile = await db.UserProfiles.FirstAsync(i => i.UserId == id);
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (profile == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile profile)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (ModelState.IsValid)
            {
                var modifyProfile = db.UserProfiles.Find(profile.UserId);
                modifyProfile.UserId = profile.UserId;
                modifyProfile.User = profile.User;
                modifyProfile.Name = profile.Name;
                modifyProfile.GPSTrackers = profile.GPSTrackers;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Dialog", profile);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profile = await db.UserProfiles.FirstAsync(i => i.UserId == id); //не нашел FindAsync ????
            ViewBag.Selector = ViewSelector.Delete;
            ViewBag.ActionName = "Удалить";
            if (profile == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var profile = await db.UserProfiles.FirstAsync(i => i.UserId == id);
            db.UserProfiles.Remove(profile);
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}