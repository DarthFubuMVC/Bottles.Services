using System.Linq;
using FubuMVC.Core;
using FubuTestingSupport;
using NUnit.Framework;

namespace Bottles.Services.Embedded.Tests
{
    [TestFixture]
    public class AssemblyNeedsTheFubuModuleAttribute
    {
        [Test]
        public void the_attribute_exists()
        {
            var assembly = typeof(EmbeddedServices).Assembly;

            assembly.GetCustomAttributes(typeof(FubuModuleAttribute), true)
                .Any().ShouldBeTrue();
        }
    }
}