using System;
using System.Collections.Generic;
using System.Linq;

namespace Bottles.Services.Remote
{
    [Serializable]
    public class ServicesToRun
    {
        private readonly IList<string> _assemblyNames = new List<string>();


        public string[] Assemblies
        {
            get { return _assemblyNames.ToArray(); }
            set
            {
                _assemblyNames.Clear();
                _assemblyNames.AddRange(value);
            }
        }

//        private readonly IList<string> _bootstrapperNames = new List<string>();
//
//        public string[] BootstrapperNames
//        {
//            get { return _bootstrapperNames.ToArray(); }
//            set
//            {
//                _bootstrapperNames.Clear();
//                _bootstrapperNames.AddRange(value);
//            }
//        }
//
//        public void AddBootstrapper(string typeName)
//        {
//            _bootstrapperNames.Add(typeName);
//        }

        public void AddAssembly(string assemblyName)
        {
            _assemblyNames.Add(assemblyName);
        }
    }
}