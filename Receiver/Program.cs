using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var webhookReceiverBaseAddress = $"http://localhost:9001/";
            var webhookSenderBaseAddress = $"http://localhost:9002";

            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };

            // Start OWIN host 
            using (WebApp.Start<Startup>(webhookReceiverBaseAddress))
            using (var httpClient = new HttpClient(handler))
            {
                // User should wait until the server (WebApiHost) is up and running before registering
                Console.WriteLine("Press any key to register"); 
                Console.ReadKey();

                // Create a webhook registration to our custom controller
                var registration = new Registration
                {
                    // To skip webhook verification use: WebHookUri = $"{webhookReceiverBaseAddress}/api/webhook?NoEcho=1",
                    WebHookUri = $"{webhookReceiverBaseAddress}/api/webhook",
                    Description = "A message is posted.",
                    Secret = "12345678901234567890123456789012",

                    // Remove the line below to receive all events, including the MessageRemovedEvent event.
                    Filters = new List<string> { "MessagePostedEvent" } 
                };

                // Register our webhook using the custom controller
                var result = httpClient.PostAsJsonAsync($"{webhookSenderBaseAddress}/api/webhooks/registrations", registration).Result;
                Console.WriteLine(result.IsSuccessStatusCode ? "Registration succesful" : "Registration failed");

                // Create a webhook registration to the build in webhook controller
                registration = new Registration
                {
                    WebHookUri = $"{webhookReceiverBaseAddress}/api/webhooks/incoming/custom",
                    Description = "A message is removed.",
                    Secret = "12345678901234567890123456789012",

                    // Remove the line below to receive all events, including the MessageRemovedEvent event.
                    Filters = new List<string> { "MessageRemovedEvent" }
                };

                // Register our webhook using the build in WebHookHandler
                result = httpClient.PostAsJsonAsync($"{webhookSenderBaseAddress}/api/webhooks/registrations", registration).Result;
                Console.WriteLine(result.IsSuccessStatusCode ? "Registration succesful" : "Registration failed");

                Console.WriteLine("Press 'm' to send a message");
                Console.WriteLine("Press 'r' to remove a message");
                Console.WriteLine("Press 'q' to quit");

                ConsoleKey key;

                do
                {
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.M:
                            httpClient.PostAsJsonAsync($"{webhookSenderBaseAddress}/api/messages",
                                new Message {Sender = "Receiver", Body = "Hello From Receiver"}).Wait();
                            break;
                        case ConsoleKey.R:
                            httpClient.DeleteAsync($"{webhookSenderBaseAddress}/api/messages?id=1").Wait();
                            break;
                    }
                } while (key != ConsoleKey.Q);
            }
        }
    }
}
