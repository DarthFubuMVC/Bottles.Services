﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bottles.Services
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
            var tasks = _services.Select(x => x.ToTask()).ToArray();
            tasks.Each(x => x.Start());

            Task.WaitAll(tasks);
        }

        public void Stop()
        {
            _services.Each(x => x.Stop());
        }
    }
}