using System;
using System.Collections.Generic;
using Bottles;
using Bottles.Diagnostics;
using StructureMap;
using StructureMap.Configuration.DSL;
using IBootstrapper = Bottles.IBootstrapper;

namespace SampleService
{
    public class SampleBootstrapper : IBootstrapper
    {
        public IEnumerable<IActivator> Bootstrap(IPackageLog log)
        {
            ObjectFactory.Initialize(x => x.AddRegistry<SampleRegistry>());
            yield return ObjectFactory.GetInstance<SampleService>();
        }
    }

    public class SampleRegistry : Registry
    {
        public SampleRegistry()
        {
            Scan(x =>
                 {
                     x.TheCallingAssembly();
                     x.WithDefaultConventions();
                 });
        }
    }

    public interface IDependency { }
    public class Dependency : IDependency { }

    public class SampleService : IActivator, IDeactivator
    {
        private readonly IDependency _dependency;

        public SampleService(IDependency dependency)
        {
            _dependency = dependency;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            Write("Starting...");
        }

        public void Deactivate(IPackageLog log)
        {
            Write("Stopping...");
        }

        public void Write(string message)
        {
            Console.WriteLine("From {0}: {1}", GetType().Name, message);
        }
    }
}
