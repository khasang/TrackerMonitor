using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebMVC.Models.PersonalAreaViewModels;
using WebMVC.Json;

namespace WebMVC.Controllers
{
    [Authorize]
    public class PersonalAreaController : BaseController
    {
        // GET: PersonalArea
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();

            // Профайл текущего пользователя
            UserProfile model = dbContext.UserProfiles.FirstOrDefault(x => x.User.Id == currentUserId);

            // Если в личный кабинет пользователь заходит первый раз и профайла у него еще нет, то его надо создать
            if (model == null)
            {
                model = new UserProfile()
                {
                    User = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId)
                };

                dbContext.UserProfiles.Add(model);
                dbContext.SaveChanges();
            }

            return View(model);
        }

        public ActionResult Details()
        {
            UserProfile profile = dbContext.UserProfiles.Find(User.Identity.GetUserId());

            return View(profile);
        }

        public ActionResult Edit()
        {
            UserProfile profile = dbContext.UserProfiles.Find(User.Identity.GetUserId());

            EditViewModel model = new EditViewModel();

            model.Name = profile.Name;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UserProfile profile = dbContext.UserProfiles.Find(User.Identity.GetUserId());

            profile.Name = model.Name;

            dbContext.SaveChanges();

            return new JsonCamelCaseResult(model, JsonRequestBehavior.DenyGet);
        }
    }
}