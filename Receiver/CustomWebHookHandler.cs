using System;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

namespace Receiver
{
    public class CustomWebHookHandler : WebHookHandler
    {
        public CustomWebHookHandler()
        {
            Receiver = "custom";
        } 

        public override Task ExecuteAsync(string generator, WebHookHandlerContext context)
        {
            //var notifications = context.GetDataOrDefault<CustomNotifications>();
            //foreach (var notification in notifications.Notifications)
            //{
            //    Console.WriteLine(string.Join(", ", notification.Keys));
            //}

            return Task.FromResult(true);
        }

        
    }
}