using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadTesting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://51.137.28.90")
            };

            var tasks = new List<Task<HttpResponseMessage>>();

            // asynchronous start all the tasks.
            for (int i = 0; i < 20; i++)
            {
                var responseTask = client.GetAsync("api/order");
                tasks.Add(responseTask);
            }

            // loop over all the task results.
            foreach (var task in tasks)
            {
                var response = await task;

                var hostIP = response.Headers.SingleOrDefault(x => x.Key == "X-Host-IP");
                Console.WriteLine($"Response from {hostIP.Value.First()}");
            }
        }
    }
}
