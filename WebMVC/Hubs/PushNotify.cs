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
    public class PushNotify:Hub
    {
        //[Authorize]
        public void SendNewMessage(GPSTrackerMessage message)
        {
            Debug.WriteLine(message.GPSTracker.OwnerId);

            Clients.Group(message.GPSTracker.OwnerId).ShowMessage(message);
        }

        /// <summary>
        /// Коннект нового пользователя, добавление новой группы с ConnectionId
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Debug.WriteLine(Context.User.Identity.IsAuthenticated);
            if (!Context.User.Identity.IsAuthenticated) return base.OnConnected();

            Debug.WriteLine(Context.ConnectionId);
            Debug.WriteLine(Context.User.Identity.GetUserId());

            Groups.Add(Context.ConnectionId, Context.User.Identity.GetUserId()); //имя группы - имя пользователя
            return base.OnConnected();
        }

    }
}
