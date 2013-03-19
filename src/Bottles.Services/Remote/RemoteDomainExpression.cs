using System;
using Bottles.Services.Messaging;
using FubuCore;

namespace Bottles.Services.Remote
{
    public class RemoteDomainExpression
    {
        private readonly MessagingHub _listeners = new MessagingHub();
        private readonly ServicesToRun _serviceList;

        private readonly AppDomainSetup _setup = new AppDomainSetup
        {
            ApplicationName = "Bottle-Services-AppDomain",
            ShadowCopyFiles = "true",
            ConfigurationFile = "BottleServiceRunner.exe.config",
            ApplicationBase = ".".ToFullPath()
        };

        public RemoteDomainExpression(ServicesToRun serviceList)
        {
            _serviceList = serviceList;
        }

        public AppDomainSetup Setup
        {
            get { return _setup; }
        }

        public string ServiceDirectory
        {
            get { return _setup.ApplicationBase; }
            set { _setup.ApplicationBase = value; }
        }

        // guesses at the directory

        public MessagingHub Listeners
        {
            get { return _listeners; }
        }

        public void LoadAssemblyContainingType<T>(string compileTarget = "Debug")
        {
            string assemblyName = typeof (T).Assembly.GetName().Name;
            string domainPath = ".".ToFullPath().ParentDirectory().ParentDirectory().ParentDirectory()
                                   .AppendPath(assemblyName, "bin", compileTarget);

            _serviceList.AddAssembly(assemblyName);
            _setup.ApplicationBase = domainPath;
        }
    }
}