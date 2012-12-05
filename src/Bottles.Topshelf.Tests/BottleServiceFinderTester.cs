using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bottles.Diagnostics;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Topshelf.Tests
{
    [TestFixture]
    public class BottleServiceFinderTester
    {
        private IEnumerable<Assembly> theAssemblies
        {
            get { return new[] {GetType().Assembly}; }
        }

        [Test]
        public void finds_the_bootstrappers()
        {
            var bootstrappers = BottleServiceFinder.FindBootstrappers(theAssemblies);
            bootstrappers.ShouldHaveCount(3);

            var types = bootstrappers.Select(x => x.GetType());
            types.ShouldHaveTheSameElementsAs(typeof(EmptyBootstrapper),
                typeof(NonBottleServiceBootstrapper), typeof(StubServiceBootstrapper));
        }

        [Test]
        public void finds_the_services()
        {
            var log = new PackageLog();
            var theService = new BottleService(new StubService(), log);
            var services = BottleServiceFinder.Find(theAssemblies, log);
            
            services.ShouldHaveTheSameElementsAs(theService);
        }
    }
}