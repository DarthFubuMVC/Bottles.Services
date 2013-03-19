using System;
using System.Collections.Generic;
using Bottles;
using Bottles.Diagnostics;
using Bottles.Services.Messaging;
using Bottles.Services.Remote;
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

    public class RemoteService : IActivator, IDeactivator, IListener<TestSignal>
    {
        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            EventAggregator.SendMessage(new WasStarted{Name = "RemoteService", AppDomainBase = AppDomain.CurrentDomain.BaseDirectory});
        }



        public void Deactivate(IPackageLog log)
        {
            
        }

        public void Receive(TestSignal message)
        {
            EventAggregator.SendMessage(new TestResponse{Number = message.Number});
        }
    }

    public class WasStarted
    {
        public string Name { get; set; }
        public string AppDomainBase { get; set; }

        protected bool Equals(WasStarted other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WasStarted) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return string.Format("WasStarted: {0}", Name);
        }
    }

    public class TestSignal
    {
        public int Number { get; set; }

        protected bool Equals(TestSignal other)
        {
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestSignal) obj);
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public override string ToString()
        {
            return string.Format("Signal: {0}", Number);
        }
    }

    public class TestResponse
    {
        public int Number { get; set; }

        protected bool Equals(TestResponse other)
        {
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestResponse) obj);
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public override string ToString()
        {
            return string.Format("Response: {0}", Number);
        }
    }
}
