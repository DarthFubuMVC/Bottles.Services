using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bottles.Services.Messaging;
using Newtonsoft.Json;

namespace Bottles.Services.Remote
{
    /*
     * Need a wait
     * 
     * 
     * 
     */




    [Serializable]
    public class ServicesToRun
    {
        private readonly IList<string> _assemblyNames = new List<string>();
        private readonly IList<string> _bootstrapperNames = new List<string>();

        public string[] Assemblies
        {
            get { return _assemblyNames.ToArray(); }
            set
            {
                _assemblyNames.Clear();
                _assemblyNames.AddRange(value);
            }
        }

        public string[] BootstrapperNames
        {
            get { return _bootstrapperNames.ToArray(); }
            set
            {
                _bootstrapperNames.Clear();
                _bootstrapperNames.AddRange(value);
            }
        }

        public void AddAssembly(string assemblyName)
        {
            _assemblyNames.Add(assemblyName);
        }

        public void AddBootstrapper(string typeName)
        {
            _bootstrapperNames.Add(typeName);
        }
    }

    public class RemoteProxy : MarshalByRefObject
    {
        public void Start(ServicesToRun services, MarshalByRefObject remoteListener)
        {
            EventAggregator.Start((IRemoteListener) remoteListener);

            // TODO -- need to run the TopShelf stuff here.
        }

        // TODO -- send messages to the remote service???

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

    public class RemoteDomainExpression
    {
        private readonly MessagingHub _listeners = new MessagingHub();

        private readonly AppDomainSetup _setup = new AppDomainSetup
        {
            ApplicationName = "Bottle-Services-AppDomain",
            ShadowCopyFiles = "true",
            ConfigurationFile = "BottleServiceRunner.exe.config"
        };

        public AppDomainSetup Setup
        {
            get { return _setup; }
        }

        // guesses at the directory

        public MessagingHub Listeners
        {
            get { return _listeners; }
        }

        public void LoadAssemblyContainingType<T>()
        {
            throw new NotImplementedException();
        }
    }

    public class RemoveServiceRunner : IDisposable
    {
        private AppDomain _domain;

        public RemoveServiceRunner(Action<RemoteDomainExpression> configure)
        {
            var expression = new RemoteDomainExpression();
            configure(expression);

            AppDomainSetup setup = expression.Setup;

            _domain = AppDomain.CreateDomain(expression.Setup.ApplicationName, null, setup);
        }

        /*
         * Spin up new AppDomain
         * 
         * 
         * 
         */

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }



}