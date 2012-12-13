using Bottles.Diagnostics;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace Bottles.Services.Embedded.Tests
{
    [TestFixture]
    public class when_activating_the_bottle_service_activator : InteractionContext<BottleServiceActivator>
    {
        private RecordingService theService;
        private PackageLog theLog;

        protected override void beforeEach()
        {
            theService = new RecordingService();
            theLog = new PackageLog();

            Services.PartialMockTheClassUnderTest();
            ClassUnderTest.Stub(x => x.FindServices(theLog)).Return(new[] {BottleService.For(theService)});

            ClassUnderTest.Activate(new IPackageInfo[0], theLog);
        }

        [Test]
        public void runs_the_service()
        {
            Wait.Until(() => theService.Activated);
            theService.Activated.ShouldBeTrue();
        }
    }
}