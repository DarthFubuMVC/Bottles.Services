using System;
using System.Diagnostics;
using System.Threading;
using Bottles.Diagnostics;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Services.Embedded.Tests
{
    [TestFixture]
    public class when_activating_the_bottle_service_activator
    {
        private StubServiceFactory theServices;
        private RecordingService theService;
        private BottleServiceActivator theActivator;

        [SetUp]
        public void SetUp()
        {
            theServices = new StubServiceFactory();
            theService = new RecordingService();
            theServices.Set<IActivator>(theService);

            theActivator = new BottleServiceActivator(theServices);
            theActivator.Activate(new IPackageInfo[0], new PackageLog());
        }

        [Test]
        public void runs_the_service()
        {
            Wait.Until(() => theService.Activated);
            theService.Activated.ShouldBeTrue();
        }
    }

    // TODO -- Just move this into FubuCore
    public static class Wait
    {
        public static void Until(Func<bool> condition, int millisecondPolling = 500, int timeoutInMilliseconds = 5000)
        {
            if (condition()) return;

            var clock = new Stopwatch();
            clock.Start();

            while (clock.ElapsedMilliseconds < timeoutInMilliseconds)
            {
                Thread.Yield();
                Thread.Sleep(500);

                if (condition()) return;
            }
        }
    }
}