using System;
using System.Collections.Generic;
using System.Linq;
using Bottles.Diagnostics;
using FubuCore;

namespace Bottles.Services.Embedded
{
    public class BottleServiceActivator : IActivator, IDisposable
    {
        private readonly IServiceLocator _services;
        private BottleServiceRunner _runner;

        public BottleServiceActivator(IServiceLocator services)
        {
            _services = services;
        }

        public virtual IEnumerable<IBottleService> FindServices(IPackageLog log)
        {
            return BottleServiceFinder
                .FindTypes(PackageRegistry.PackageAssemblies)
                .Select(type => _services.GetInstance(type).As<IActivator>())
                .Select(activator => new BottleService(activator, log))
                .ToList();
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