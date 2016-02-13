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
            UserProfile model = dataManager.UserProfiles.GetByUserId(currentUserId);

            // Если в личный кабинет пользователь заходит первый раз и профайла у него еще нет, то его надо создать
            if (model == null)
            {
                model = new UserProfile()
                {
                    User = dataManager.Users.GetById(currentUserId)
                };

                dataManager.UserProfiles.Add(model);
                dataManager.Save();
            }

            return View(model);
        }

        public ActionResult Details()
        {
            UserProfile profile = dataManager.UserProfiles.GetById(User.Identity.GetUserId());

            return View(profile);
        }

        public ActionResult Edit()
        {
            UserProfile profile = dataManager.UserProfiles.GetById(User.Identity.GetUserId());

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

            UserProfile profile = dataManager.UserProfiles.GetById(User.Identity.GetUserId());

            profile.Name = model.Name;

            dataManager.Save();

            return new JsonCamelCaseResult(model, JsonRequestBehavior.DenyGet);
        }
    }
}