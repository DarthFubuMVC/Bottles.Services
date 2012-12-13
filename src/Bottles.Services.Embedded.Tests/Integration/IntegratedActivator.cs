using System.Collections.Generic;
using System.Threading;
using Bottles.Diagnostics;
using Timer = System.Timers.Timer;

namespace Bottles.Services.Embedded.Tests.Integration
{
    public class IntegratedActivator : IActivator, IDeactivator
    {
        private readonly InnerService _inner;

        public IntegratedActivator(InnerService inner)
        {
            _inner = inner;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _inner.Start();
        }

        public void Deactivate(IPackageLog log)
        {
        }
    }

    public class InnerService
    {
        private readonly Timer _timer;
        private readonly ManualResetEvent _reset;

        public InnerService()
        {
            _reset = new ManualResetEvent(false);
            _timer = new Timer(1000)
            {
                AutoReset = false
            };

            _timer.Elapsed += (e, args) => Execute();
        }

        public bool Ran { get; set; }

        public void Start()
        {
            _timer.Start();
        }

        public void WaitForNextRun()
        {
            Ran = false;
            _reset.Reset();
            _reset.WaitOne(5000);
        }

        public void Execute()
        {
            Ran = true;
            _reset.Set();
            _timer.Start();
        }
    }
}