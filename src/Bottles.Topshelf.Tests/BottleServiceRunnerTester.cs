using NUnit.Framework;
using Rhino.Mocks;

namespace Bottles.Topshelf.Tests
{
    [TestFixture]
    public class BottleServiceRunnerTester
    {
        private IBottleService s1;
        private IBottleService s2;
        private BottleServiceRunner theRunner;

        [SetUp]
        public void SetUp()
        {
            s1 = MockRepository.GenerateMock<IBottleService>();
            s2 = MockRepository.GenerateMock<IBottleService>();

            theRunner = new BottleServiceRunner(new[] { s1, s2 });
        }

        [Test]
        public void starts_all_the_services()
        {
            theRunner.Start();

            s1.AssertWasCalled(x => x.Start());
            s2.AssertWasCalled(x => x.Start());
        }

        [Test]
        public void stops_all_the_services()
        {
            theRunner.Stop();

            s1.AssertWasCalled(x => x.Stop());
            s2.AssertWasCalled(x => x.Stop());
        }
    }
}