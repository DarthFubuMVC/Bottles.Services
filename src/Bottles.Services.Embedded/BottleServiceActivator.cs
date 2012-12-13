using System;
using System.Collections.Generic;
using Bottles.Diagnostics;

namespace Bottles.Services.Embedded
{
    public class BottleServiceActivator : IActivator, IDisposable
    {
        private BottleServiceRunner _runner;

        public virtual IEnumerable<IBottleService> FindServices(IPackageLog log)
        {
            return BottleServiceFinder.Find(PackageRegistry.PackageAssemblies, log);
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _runner = new BottleServiceRunner(FindServices(log));
            _runner.Start();
        }

        public void Dispose()
        {
            if(_runner != null)
            {
                _runner.Stop();
            }
        }
    }
} 