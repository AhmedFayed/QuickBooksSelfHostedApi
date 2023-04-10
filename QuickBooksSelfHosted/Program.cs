using Microsoft.Owin.Hosting;
using QuickBooksSelfHostedApi.Utilites;
using System;
using System.ServiceProcess;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace QuickBooksSelfHostedApi
{
    class Program
    {

        static void Main(string[] args)
        {
            var port = ConfigurationReader.GetValue("Port");
            var startOptions = new StartOptions(url: $"http://*:{port}/");

            // Start OWIN host 
            using (WebApp.Start<Startup>(startOptions))
            {
                Console.WriteLine($"Service Started Successfully On Port: {port}...");
                while (true)
                {
                    Console.ReadLine();
                }
            }

        }
    }
}
