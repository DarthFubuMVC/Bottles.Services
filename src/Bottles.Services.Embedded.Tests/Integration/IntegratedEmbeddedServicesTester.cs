using FubuMVC.TestingHarness;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Services.Embedded.Tests.Integration
{
    [TestFixture]
    public class IntegratedEmbeddedServicesTester : FubuRegistryHarness
    {
        private InnerService theHostedService;

        protected override void configureContainer(StructureMap.IContainer container)
        {
            theHostedService = new InnerService();
            container.Configure(x => x.For<InnerService>().Singleton().Use(theHostedService));
        }

        protected override void initializeBottles()
        {
            PackageRegistry.LoadPackages(x => x.Assembly(GetType().Assembly));
        }

        [Test]
        public void runs_the_service()
        {
            theHostedService.WaitForNextRun();
            theHostedService.Ran.ShouldBeTrue();
        }
    }
}