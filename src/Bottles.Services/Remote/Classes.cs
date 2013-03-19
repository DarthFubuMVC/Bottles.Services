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


    public class CallbackProxy : MarshalByRefObject, IRemoteListener
    {
        private readonly MessagingHub _listener;

        public CallbackProxy(MessagingHub listener)
        {
            _listener = listener;
        }

        public void Send(string json)
        {
            _listener.SendJson(json);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

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
            ServiceListener.Start((IRemoteListener) remoteListener);

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


    public interface IRemoteListener
    {
        void Send(string json);
    }

    public static class ServiceListener
    {
        private static readonly BlockingCollection<object> _messages;
        private static IRemoteListener _remoteListener;
        private static CancellationTokenSource _cancellationSource;
        private static Task _task;

        static ServiceListener()
        {
            _messages = new BlockingCollection<object>(new ConcurrentQueue<object>());
        }

        public static void Start(IRemoteListener remoteListener)
        {
            _remoteListener = remoteListener;

            _cancellationSource = new CancellationTokenSource();
            _task = Task.Factory.StartNew(read, _cancellationSource.Token);
            _task.Start();
        }

        private static void read()
        {
            foreach (object o in _messages.GetConsumingEnumerable(_cancellationSource.Token))
            {
                //var json = _serializer.Serialize(o, true);
                //_remoteListener.Send(json);
            }
        }

        public static void Stop()
        {
            _cancellationSource.Cancel();
        }

        public static void SendMessage(string category, string message)
        {
            SendMessage(new ServiceMessage
            {
                Category = category,
                Message = message
            });
        }

        public static void SendMessage(object message)
        {
            _messages.Add(message);
        }
    }

    public class ServiceMessage
    {
        public string Category { get; set; }
        public string Message { get; set; }
    }
}