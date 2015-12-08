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
using System.Globalization;

namespace TestMessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            //id двух пользователей из бд


            HubConnection hubConnection = new HubConnection(@"http://localhost:3254");
            IHubProxy hubProxy = hubConnection.CreateHubProxy("PushNotify");
            hubConnection.Start().Wait();


            Random r = new Random();
            Console.WriteLine("Service started...");
            foreach (var message in GetGPSMessages())
            {
                //вызов метода из хаба PushNotify и передача ему сообщения.
                hubProxy.Invoke("SendNewMessage", message);
                Console.WriteLine("...Send {0} - {1} - {2} - {3} - {4} ... ok", message.GPSTracker.OwnerId, message.GPSTrackerId, message.Latitude, message.Longitude, message.Time);
                Console.WriteLine();
                Thread.Sleep(new Random().Next(1000, 4000));
            }
        }

        private static IEnumerable<GPSTrackerMessage> GetGPSMessages()
        {
            Random r = new Random();
            List<GPSTrackerMessage> messages = new List<GPSTrackerMessage>();

            string gpsPointsFilePath = @"C:\Users\Николай\Documents\Visual Studio 2015\Projects\GPSTracker\GPSPoints\GPSPoints.csv";
            string[] guids = new string[] { "3277c404-708b-43b8-9bbd-e6e65a869d3f", "a49b8728-6d68-4029-a3a9-a1581edb2d7e" };

            using (StreamReader sr = new StreamReader(gpsPointsFilePath))
            {
                CultureInfo usCulture = new CultureInfo("en-US");
                NumberFormatInfo dbNumberFormat = usCulture.NumberFormat;

                while (!sr.EndOfStream)
                {
                    string[] coordinates = sr.ReadLine().Split(';');

                    messages.Add(new GPSTrackerMessage
                    {
                        Id = 0,
                        GPSTrackerId = "111111",
                        Latitude = double.Parse(coordinates[0], dbNumberFormat),
                        Longitude = double.Parse(coordinates[1], dbNumberFormat),
                        GPSTracker = new GPSTracker()
                        {
                            OwnerId = guids[r.Next(2)]
                        },
                        Time = DateTime.Now
                    });
                }
            }

            return messages;
        }
    }
}
