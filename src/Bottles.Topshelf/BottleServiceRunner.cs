using System.Collections.Generic;

namespace Bottles.Topshelf
{
    public class BottleServiceRunner
    {
        private readonly IEnumerable<IBottleService> _services;

        public BottleServiceRunner(IEnumerable<IBottleService> services)
        {
            _services = services;
        }

        // Leave this public for testing
        public IEnumerable<IBottleService> Services
        {
            get { return _services; }
        }

        public void Start()
        {
            _services.Each(x => x.Start());
        }

        public void Stop()
        {
            _services.Each(x => x.Stop());
        }
    }
}