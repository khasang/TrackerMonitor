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
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager;
        public UsersController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 20;              //количество объектов на странице
            int pageNumber = (page ?? 1); 
            var users = await db.Users.ToListAsync();
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        //Эксперименты с Json
        [HttpGet]
        public ActionResult Create()
        {
            var user = new ApplicationUser();
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить пользователя";
            return View("Dialog", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser model)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Добавить пользователя";
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    AccessFailedCount = model.AccessFailedCount,
                    LockoutEnabled = model.LockoutEnabled,
                    LockoutEndDateUtc = model.LockoutEndDateUtc,
                    PasswordHash = model.PasswordHash,
                    SecurityStamp = model.SecurityStamp,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    UserProfileId = model.UserProfileId
                };
                db.Users.Add(user);
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
            var user = await db.Users.FirstAsync(i => i.Id == id); //не нашел FindAsync ????
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
            ViewBag.Selector = ViewSelector.Edit;
            ViewBag.ActionName = "Редактировать";
            if (ModelState.IsValid)
            {
                var modifyUser = db.Users.Find(user.Id);
                modifyUser.Id = user.Id;
                modifyUser.UserName = user.UserName;
                modifyUser.Email = user.Email;
                modifyUser.EmailConfirmed = user.EmailConfirmed;
                modifyUser.PhoneNumber = user.PhoneNumber;
                modifyUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                modifyUser.AccessFailedCount = user.AccessFailedCount;
                modifyUser.LockoutEnabled = user.LockoutEnabled;
                modifyUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
                modifyUser.PasswordHash = user.PasswordHash;
                modifyUser.SecurityStamp = user.SecurityStamp;
                modifyUser.TwoFactorEnabled = user.TwoFactorEnabled;
                modifyUser.UserProfileId = user.UserProfileId;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Dialog", user);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await db.Users.FirstAsync(i => i.Id == id);
            ViewBag.Selector = ViewSelector.Delete;
            ViewBag.ActionName = "Удалить";
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("Dialog", user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var user = await db.Users.FirstAsync(i => i.Id == id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}