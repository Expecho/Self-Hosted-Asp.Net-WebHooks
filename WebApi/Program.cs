using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;
using Microsoft.AspNet.WebHooks.Services;
using Microsoft.Owin.Hosting;
using WebApiHost.Models;

namespace WebApiHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string webHookSenderBaseAddress = "http://localhost:9002";

            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };

            // Microsoft.AspNet.WebHooks.Custom.SqlStorage
            // Microsoft.AspNet.WebHooks.Custom.AzureStorage
            // Microsoft.AspNet.WebHooks.Custom.MongoStorage
            //CustomServices.SetStore(new MemoryWebHookStore());

            // Start OWIN host 
            using (WebApp.Start<Startup>(webHookSenderBaseAddress))
            using (var client = new HttpClient(handler))
            {
                Console.WriteLine("Webhook sender up and running. Waiting for webhook receiver to register");

                // User should wait until the webhook receiver has completed the registration.
                Console.WriteLine("Press any key to trigger the webhook by sending a message");
                Console.ReadKey();

                // trigger a webhook call
                await client.PostAsJsonAsync($"{webHookSenderBaseAddress}/api/messages", new Message { Sender = "WebApiHost", Body = "Hello From Sender" });
                
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
