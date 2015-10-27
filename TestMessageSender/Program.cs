using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;

namespace TestMessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection hubConnection = new HubConnection(@"http://localhost:3254");
            IHubProxy hubProxy = hubConnection.CreateHubProxy("PushNotify");
            hubConnection.Start().Wait();


            Random r = new Random();
            Console.WriteLine("Service started...");
            while (true)
            {
                GPSTrackerMessage message = new GPSTrackerMessage()
                {
                    Id = r.Next(10),
                    GPSTrackerId = r.Next(3),
                    Latitude = 31 + r.Next(20),
                    Longitude = 31 - r.Next(20),
                    Time = DateTime.Now,
                };
                //вызов метода из хаба PushNotify
                hubProxy.Invoke("SendNewMessage", message);
                Console.WriteLine($"...Send {message.Id} - {message.GPSTrackerId} - {message.Latitude} - {message.Longitude} - {message.Time} ..... ok");
                Console.WriteLine();
                Thread.Sleep(new Random().Next(1000, 4000));
            }
        }
    }
}
