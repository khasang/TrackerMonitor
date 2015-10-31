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
            if (message.Id == 0)
            {
                //Debug.WriteLine(Context.User.Identity.Name);
                Clients.Group("admin@admin.com").ShowMessage(message);
            }
            else if (message.Id == 1)
            {
                //у определенного пользователя группа в честь его username, которая содержит все его ConnectionId
                Clients.Group("admin@ad.com").ShowMessage(message);
            }
        }

        /// <summary>
        /// Коннект нового пользователя, добавление новой группы с ConnectionId
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Debug.WriteLine(Context.ConnectionId);
            Groups.Add(Context.ConnectionId, Context.User.Identity.Name); //имя группы - имя пользователя
            return base.OnConnected();
        }

    }
}
