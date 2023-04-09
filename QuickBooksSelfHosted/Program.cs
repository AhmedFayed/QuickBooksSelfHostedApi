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

        #region Nested classes to support running as service
        public const string ServiceName = "QuickBooksCommunicationService";

        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = Program.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.Start(args);
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }
        #endregion

        static void Main(string[] args)
        { 
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            else
            {
                // running as console app
                Start(args);
                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);
                Stop();
            }

        }

        private static void Start(string[] args)
        {
            

            var port = ConfigurationReader.GetValue("Port");
            var startOptions = new StartOptions(url: $"http://*:{port}/");

            Console.WriteLine($"Service Started Successfully On Port: {port}...");
            // Start OWIN host 
            using (WebApp.Start<Startup>(startOptions))
            {
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        private static void Stop()
        {
            // onstop code here
        }
    }
}
