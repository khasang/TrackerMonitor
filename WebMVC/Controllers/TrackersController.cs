using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class TrackersController : BaseController
    {
        // GET: Trackers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult ConfirmDelete()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}