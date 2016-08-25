using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var webhookReceiverBaseAddress = $"http://{Environment.MachineName}.deheer-groep.nl:9090/";
            var webhookSenderBaseAddress = $"http://{Environment.MachineName}.deheer-groep.nl:9000";

            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };

            // Start OWIN host 
            using (WebApp.Start<Startup>(webhookReceiverBaseAddress))
            using (var httpClient = new HttpClient(handler))
            {
                Console.WriteLine("Press any key to register");
                Console.ReadKey();

                var registration = new Registration
                {
                    WebHookUri = $"{webhookReceiverBaseAddress}/api/webhooks/incoming/custom",
                    Description = "A message is posted.",
                    Secret = "12345678901234567890123456789012"
                };

                var result = httpClient.PostAsJsonAsync($"http://{Environment.MachineName}.deheer-groep.nl:9000/api/webhooks/registrations", registration).Result;
                Console.WriteLine(result.IsSuccessStatusCode ? "Registration succesful" : "Registration failed");

                Console.WriteLine("Press 'm' to send a message");
                Console.WriteLine("Press 'r' to remove a message");
                Console.WriteLine("Press 'q' to quit");

                ConsoleKey key = ConsoleKey.Spacebar;

                while (key != ConsoleKey.Q)
                {
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.M:
                            httpClient.PostAsJsonAsync($"{webhookSenderBaseAddress}/api/messages", new Message { Sender = "Receiver", Body = "Hello From Receiver" }).Wait();
                            break;
                        case ConsoleKey.R:
                            httpClient.DeleteAsync($"{webhookSenderBaseAddress}/api/messages?id=1").Wait();
                            break;
                    }
                }
            }
        }
    }
}
