using System.Collections.Generic;
using Bottles.Diagnostics;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Services.Embedded.Tests
{
    [TestFixture]
    public class BottleServiceActivatorTester
    {
        private StubServiceFactory theServices;
        private BottleServiceActivator theActivator;
        private StubService theService;
        private StubActivator theStubActivator;

        [SetUp]
        public void SetUp()
        {
            theServices = new StubServiceFactory();
            theActivator = new BottleServiceActivator(theServices);

            theService = new StubService();
            theStubActivator = new StubActivator();

            theServices.Set<IActivator>(theService);
            theServices.Set<IActivator>(theStubActivator);
        }

        [Test]
        public void finds_the_services()
        {
            theActivator.FindServices(new PackageLog()).ShouldHaveTheSameElementsAs(BottleService.For(theService));
        }

        public class StubActivator : IActivator
        {
            public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
            {
                throw new System.NotImplementedException();
            }
        }

        public class StubService : IActivator, IDeactivator
        {
            public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
            {
                throw new System.NotImplementedException();
            }

            public void Deactivate(IPackageLog log)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}