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
            Clients.Group(message.GPSTracker.OwnerId).ShowMessage(message);
            //у определенного пользователя группа в честь  его id, которая содержит все его ConnectionId
        }

        /// <summary>
        /// Коннект нового пользователя, добавление новой группы с ConnectionId
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            if (Context != null && Context.User.Identity.IsAuthenticated)
            {
                Groups.Add(Context.ConnectionId, Context.User.Identity.GetUserId()); //имя группы - id пользователя
            }
            else
            {
                Groups.Add(Context.ConnectionId, "unknown");
            }
            return base.OnConnected();
        }


        /// <summary>
        /// Дисконнект
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    Groups.Remove(Context.ConnectionId, Context.User.Identity.Name);
        //    return base.OnDisconnected(stopCalled);
        //}

    }
}
