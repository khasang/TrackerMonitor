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
            return PartialView(await userProfiles.ToListAsync());
        }

        public ActionResult Details(int id = 0)
        {
            var userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", userProfile);
        }
    }
}