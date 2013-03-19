using System.Threading;
using Bottles.Services.Remote;
using NUnit.Framework;
using System.Linq;
using FubuTestingSupport;
using SampleService;

namespace Bottles.Services.Tests.Remote
{
    [TestFixture]
    public class BigRemoteServicesIntegrationTester
    {
        private RemoteServiceRunner start()
        {
            return new RemoteServiceRunner(x => x.LoadAssemblyContainingType<SampleService.SampleService>());
        }

        [Test]
        public void spin_up_the_remote_service_for_the_sample_and_send_messages_back_and_forth()
        {
            using (var runner = start())
            {
                runner.WaitForServiceToStart<SampleService.SampleService>();
                runner.WaitForServiceToStart<SampleService.RemoteService>();

                runner.Started.Any().ShouldBeTrue();
            }
        }

        [Test]
        public void spin_up_and_send_and_receive_messages()
        {
            using (var runner = start())
            {
                runner.WaitForServiceToStart<SampleService.RemoteService>();

                runner.WaitForMessage<TestResponse>(() => {
                    runner.SendRemotely(new TestSignal { Number = 1 });
                }).Number.ShouldEqual(1);


                runner.WaitForMessage<TestResponse>(() =>
                {
                    runner.SendRemotely(new TestSignal { Number = 3 });
                }).Number.ShouldEqual(3);

                runner.WaitForMessage<TestResponse>(() =>
                {
                    runner.SendRemotely(new TestSignal { Number = 5 });
                }).Number.ShouldEqual(5);
                
            }
        }
    }
}