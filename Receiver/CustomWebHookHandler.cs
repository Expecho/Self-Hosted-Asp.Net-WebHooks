using System;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

namespace Receiver
{
    public class CustomWebHookHandler : WebHookHandler
    {
        public override Task ExecuteAsync(string generator, WebHookHandlerContext context)
        {
            var notifications = context.GetDataOrDefault<CustomNotifications>();
            
            Console.WriteLine("Received notification with payload:");
            foreach (var notification in notifications.Notifications)
            {
                Console.WriteLine(string.Join(", ", notification.Values));
            }

            return Task.FromResult(true);
        }
    }
}