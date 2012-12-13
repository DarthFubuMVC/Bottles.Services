using System.Collections.Generic;
using Bottles.Diagnostics;

namespace Bottles.Services.Embedded.Tests.Integration
{
    public class IntegratedBootstrapper : IBootstrapper
    {
        public static readonly InnerService Service;

        static IntegratedBootstrapper()
        {
            Service = new InnerService();
        }

        public IEnumerable<IActivator> Bootstrap(IPackageLog log)
        {
            yield return new IntegratedActivator(Service);
        }
    }

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
}