using System;
using System.Collections.Generic;
using FubuCore;
using Topshelf;
using System.Linq;

namespace BottleServiceRunner
{
    internal static class Program
    {
        public static void Main(params string[] args)
        {
            var application = new TopshelfApplication();
            var runner = application.Bootstrap(findAssemblies(args).ToArray());



            var directory = TopshelfPackageFacility.GetApplicationDirectory().ToFullPath();
            var settings = new FileSystem().LoadFromFile<BottleServiceConfiguration>(directory,
                                                                                     BottleServiceConfiguration.FILE);

            HostFactory.Run(x => {
                x.ApplyCommandLine("");
                x.AddCommandLineDefinition("assembly", s => { });
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

        private static IEnumerable<string> findAssemblies(IEnumerable<string> args)
        {
            bool useNext = false;
            foreach (var arg in args)
            {
                if (useNext)
                {
                    yield return arg;

                    
                }

                useNext = arg == "--assembly";
            }
        } 
    }
}