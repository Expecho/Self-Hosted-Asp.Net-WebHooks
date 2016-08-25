using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.Owin.Hosting;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = $"http://{Environment.MachineName}.deheer-groep.nl:9090/";

            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };


            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            using (var httpClient = new HttpClient(handler))
            {
                Console.WriteLine("Press any key to register");
                Console.ReadKey();

                var registration = new Registration
                {
                    WebHookUri = $"http://{Environment.MachineName}.deheer-groep.nl:9090/api/webhooks/incoming/custom",
                    Description = "A message is posted.",
                    Secret = "12345678901234567890123456789012"
                };

                var result = httpClient.PostAsJsonAsync($"http://{Environment.MachineName}.deheer-groep.nl:9000/api/webhooks/registrations", registration).Result;

                Console.WriteLine($"{result.StatusCode}: {result.ReasonPhrase}");

                Console.WriteLine("Press any key to send a message");
                Console.ReadKey();

                httpClient.PostAsJsonAsync($"http://{Environment.MachineName}.deheer-groep.nl:9000/api/messages", new Message { Sender = "Receiver", Body = "Hello From Receiver" }).Wait();

                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}
