using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bottles;
using Bottles.Services;
using FubuCore;
using Topshelf;

namespace BottleServiceRunner
{
    internal static class Program
    {
        public static void Main(params string[] args)
        {
            var application = new BottleServiceApplication();
            var runner = application.Bootstrap();
			Caveman.Log("Got the runner");
			var directory = BottlesServicePackageFacility.GetApplicationDirectory();

            
            var settings = new FileSystem().LoadFromFile<BottleServiceConfiguration>(directory,
                                                                                     BottleServiceConfiguration.FILE);
			Caveman.Log("Starting TopShelf stuff");
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