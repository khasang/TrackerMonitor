using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebMVC.Hubs
{
    /// <summary>
    /// Класс для работы с SignalR
    /// </summary>
    public class PushNotify : Hub
    {
        public void SendNewMessage(GPSTrackerMessage message)
        {
            Clients.Group(message.GPSTracker.OwnerId).ShowMessage(message);
        }

        /// <summary>
        /// Коннект нового пользователя, добавление новой группы с ConnectionId
        /// </summary>
        /// <returns>Task</returns>
        public override Task OnConnected()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Groups.Add(Context.ConnectionId, Context.User.Identity.GetUserId());
            }
            return base.OnConnected();
        }

    }
}
