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
                    PhoneNumber = model.PhoneNumber
                };
                db.Users.Add(user);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}