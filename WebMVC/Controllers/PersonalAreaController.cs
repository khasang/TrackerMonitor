using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebMVC.Models.PersonalAreaViewModels;

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

        public ActionResult Edit()
        {
            UserProfile profile = dbContext.UserProfiles.Find(User.Identity.GetUserId());

            EditViewModel model = new EditViewModel();

            model.Name = "Admin";

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UserProfile profile = dbContext.UserProfiles.Find(User.Identity.GetUserId());
            
            return View();
        }
    }
}