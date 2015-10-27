using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Hubs
{
    /// <summary>
    /// Класс для работы с SignalR
    /// </summary>
    public class PushNotify:Hub
    {
        UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _context = new ApplicationDbContext();

        public PushNotify()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        public void SendNewMessage(GPSTrackerMessage message)
        {
            //if (message.GPSTracker.Owner == _userManager.FindById(Context.User.Identity.GetUserId()))
            //{

            //Cогласно докам по сигнал р используется IPrincipal.Identity.Name as the user name, но можно изменить
            Clients.User("3277c404-708b-43b8-9bbd-e6e65a869d3f").ShowMessage(message); //айди админа
            //}
            //Clients.All.ShowMessage(message);
        }

    }
}
