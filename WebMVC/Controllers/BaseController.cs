using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class BaseController : Controller
    {
        //protected ApplicationDbContext dbContext = new ApplicationDbContext();

        protected IDataManager dataManager = new DataManager();

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dataManager != null)
                {
                    dataManager.Dispose();
                    dataManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}