using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using WebApiHost.Models;

namespace WebApiHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var webHookSenderBaseAddress = $"http://localhost:9002";

            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };

            // Start OWIN host 
            using (WebApp.Start<Startup>(webHookSenderBaseAddress))
            using (var client = new HttpClient(handler))
            {
                Console.WriteLine("Server up and running.");

                // User should wait until the webhook receiver has completed the registration.
                Console.WriteLine("Press any key to send a message");
                Console.ReadKey();

                // trigger a webhook call
                client.PostAsJsonAsync($"{webHookSenderBaseAddress}/api/messages", new Message { Sender = "WebApiHost", Body = "Hello From Sender" }).Wait();
                
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
