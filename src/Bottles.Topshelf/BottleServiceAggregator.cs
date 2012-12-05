using System.Collections.Generic;
using Bottles.Diagnostics;

namespace Bottles.Topshelf
{
    public class BottleServiceAggregator : IBootstrapper
    {
        private readonly IList<BottleService> _services = new List<BottleService>();
 
        public IEnumerable<IActivator> Bootstrap(IPackageLog log)
        {
            _services.Clear();
            _services.AddRange(BottleServiceFinder.Find(PackageRegistry.PackageAssemblies, log));

            return new IActivator[0];
        }

        public BottleServiceRunner ServiceRunner()
        {
            return new BottleServiceRunner(_services);
        }
    }
}