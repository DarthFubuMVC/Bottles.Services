using System.Linq;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Services.Embedded.Tests
{
    [TestFixture]
    public class EmbeddedServicesExtensionTester
    {
        private BehaviorGraph theGraph;

        [SetUp]
        public void SetUp()
        {
            theGraph = BehaviorGraph.BuildFrom(r => r.Import<EmbeddedServices>());
        }

        [Test]
        public void registers_the_BottleServiceActivator()
        {
            theGraph
                .Services
                .ServicesFor<IActivator>()
                .Any(def => def.Type == typeof (BottleServiceActivator))
                .ShouldBeTrue();
        }
    }
}