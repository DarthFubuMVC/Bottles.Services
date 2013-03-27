using System;
using Bottles.Services;
using FubuCore;
using Topshelf;
using System.Linq;
using System.Collections.Generic;

namespace BottleServiceRunner
{
    internal static class Program
    {
        public static void Main(params string[] args)
        {
            var application = new BottleServiceApplication();
            
            var runner = application.Bootstrap();

            if (!runner.Services.Any())
            {
                throw new ApplicationException("No services were detected.  Shutting down.");
            }
            else
            {
                runner.Services.Each(x => {
                    Console.WriteLine("Started " + x);
                });
            }


            var directory = BottlesServicePackageFacility.GetApplicationDirectory();
            
            var settings = new FileSystem().LoadFromFile<BottleServiceConfiguration>(directory,
                                                                                     BottleServiceConfiguration.FILE);
            

            RunService(settings, runner);
        }

        public static void RunService(BottleServiceConfiguration settings, Bottles.Services.BottleServiceRunner runner)
        {
            HostFactory.Run(x => {
                x.SetServiceName(settings.Name);
                x.SetDisplayName(settings.DisplayName);
                x.SetDescription(settings.Description);

                x.RunAsLocalService();


                x.Service<Bottles.Services.BottleServiceRunner>(s => {
                    s.ConstructUsing(name => runner);
                    s.WhenStarted(r => r.Start());
                    s.WhenStopped(r => r.Stop());
                    s.WhenPaused(r => r.Stop());
                    s.WhenContinued(r => r.Start());
                    s.WhenShutdown(r => r.Stop());
                });

                x.StartAutomatically();
            });
        }
    }
}