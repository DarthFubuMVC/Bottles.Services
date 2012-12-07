using System;
using System.Collections.Generic;
using System.Linq;
using Bottles.Diagnostics;
using FubuCore.Binding;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace Bottles.Services.Embedded.Tests
{
    public class RecordingService : IActivator, IDeactivator
    {
        public bool Activated { get; set; }
        public bool Deactivated { get; set; }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            Activated = true;
        }

        public void Deactivate(IPackageLog log)
        {
            Deactivated = true;
        }
    }

    public class StubServiceFactory : IServiceFactory
    {
        private readonly IList<object> _items = new List<object>();

        public void Set<T>(T item)
        {
            _items.Add(item);
        }

        public IActionBehavior BuildBehavior(ServiceArguments arguments, Guid behaviorId)
        {
            throw new NotImplementedException();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _items.OfType<T>();
        }
    }
}