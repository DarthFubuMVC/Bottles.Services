using System;
using System.Collections.Generic;
using System.Linq;
using Bottles.Diagnostics;
using FubuMVC.Core.Runtime;

namespace Bottles.Services.Embedded
{
    public class BottleServiceActivator : IActivator, IDisposable
    {
        private readonly IServiceFactory _services;
        private BottleServiceRunner _runner;

        public BottleServiceActivator(IServiceFactory services)
        {
            _services = services;
        }

        public IEnumerable<IBottleService> FindServices(IPackageLog log)
        {
            return _services
                .GetAll<IActivator>()
                .Where(BottleService.IsBottleService)
                .Select(x => new BottleService(x, log))
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