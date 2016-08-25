using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace WebApiHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = $"http://{Environment.MachineName}.deheer-groep.nl:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            using (var client = new HttpClient())
            {
                Console.WriteLine("Press any key to send a message");
                Console.ReadKey();

                client.PostAsJsonAsync(baseAddress + "api/messages", new Message { Sender = "WebApiHost", Body = "Hello From Sender" }).Wait();
                var response = client.GetAsync(baseAddress + "api/messages").Result;
                
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
