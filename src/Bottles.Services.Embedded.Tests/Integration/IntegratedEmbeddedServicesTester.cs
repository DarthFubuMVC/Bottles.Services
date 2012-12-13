using FubuMVC.TestingHarness;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Services.Embedded.Tests.Integration
{
    [TestFixture]
    public class IntegratedEmbeddedServicesTester : FubuRegistryHarness
    {
        private InnerService theHostedService;

        protected override void beforeRunning()
        {
            theHostedService = IntegratedBootstrapper.Service;
        }

        [Test]
        public void runs_the_service()
        {
            theHostedService.WaitForNextRun();
            theHostedService.Ran.ShouldBeTrue();
        }
    }
}