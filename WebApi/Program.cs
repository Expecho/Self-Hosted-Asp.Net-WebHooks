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
            var webHookSenderBaseAddress = $"http://{Environment.MachineName}.deheer-groep.nl:9000";

            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };

            // Start OWIN host 
            using (WebApp.Start<Startup>(webHookSenderBaseAddress))
            using (var client = new HttpClient(handler))
            {
                Console.WriteLine("Server up and running.");
                Console.WriteLine("Press any key to send a message");
                Console.ReadKey();

                client.PostAsJsonAsync($"{webHookSenderBaseAddress}/api/messages", new Message { Sender = "WebApiHost", Body = "Hello From Sender" }).Wait();
                
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
