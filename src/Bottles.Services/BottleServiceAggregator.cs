using System.Collections.Generic;
using Bottles.Diagnostics;
using FubuCore;

namespace Bottles.Services
{
    public class BottleServiceAggregator : IBootstrapper
    {
        private readonly IList<IBottleService> _services = new List<IBottleService>();
 
        public IEnumerable<IActivator> Bootstrap(IPackageLog log)
        {
            _services.Clear();

			Caveman.Log("Looking for bootstrappers now");

            _services.AddRange(BottleServiceFinder.Find(PackageRegistry.PackageAssemblies, log));

            return new IActivator[0];
        }

        public BottleServiceRunner ServiceRunner()
        {
			Caveman.Log("building service runner");
	        _services.Each(x => Caveman.Log("Service:  " + x.As<BottleService>().Activator.GetType().Name));

            return new BottleServiceRunner(_services);
        }
    }
}