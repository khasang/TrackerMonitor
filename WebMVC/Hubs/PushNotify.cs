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
using System.Diagnostics;

namespace WebMVC.Hubs
{
    /// <summary>
    /// Класс для работы с SignalR
    /// </summary>
    //[Authorize]
    public class PushNotify : Hub
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public void SendNewMessage(GPSTrackerMessage message)
        {
            Debug.WriteLine("Send -> " + message.GPSTracker.OwnerId);

            if(message.GPSTracker == null)
            {
                // Здесь логирование сообщений от неизвестных трекеров
                return;
            }

            Clients.Group(message.GPSTracker.OwnerId).ShowMessage(message);            
        }

        /// <summary>
        /// Коннект нового пользователя, добавление новой группы с ConnectionId
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Debug.WriteLine("OnConnected");
            Debug.WriteLine("IsAuthenticated = " + Context.User.Identity.IsAuthenticated.ToString());

            if (!Context.User.Identity.IsAuthenticated) return base.OnConnected();

            Debug.WriteLine("ConnectionId = " + Context.ConnectionId);
            Debug.WriteLine("UserId = " + Context.User.Identity.GetUserId());

            Groups.Add(Context.ConnectionId, Context.User.Identity.GetUserId()); //имя группы - имя пользователя
            return base.OnConnected();
        }

    }
}
