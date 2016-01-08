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
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager;

        public UsersController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: admin/Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users;
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(await users.ToListAsync());
        }

        //Эксперименты с Json
        [HttpGet]
        public ActionResult Create()
        {
            var user = new ApplicationUser();
            return View("Create", user);
        }

        [HttpPost]
        public JsonResult Create(ApplicationUser model)
        {
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
                    //Claims = model.Claims, только чтение
                    LockoutEnabled = model.LockoutEnabled,
                    LockoutEndDateUtc = model.LockoutEndDateUtc,
                    //Logins = model.Logins, только чтение
                    PasswordHash = model.PasswordHash,
                    //Roles = model.Roles, только чтение
                    SecurityStamp = model.SecurityStamp,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    UserProfile = model.UserProfile,
                    UserProfileId = model.UserProfileId
                };
                db.Users.Add(user);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = await db.Users.FirstAsync(i => i.Id == id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("Details",user);
        //}

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await db.Users.FirstAsync(i => i.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
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
                modifyUser.UserProfile = user.UserProfile;
                modifyUser.UserProfileId = user.UserProfileId;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit",user);
        }
    }
}