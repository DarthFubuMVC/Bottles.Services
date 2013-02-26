using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bottles;
using Bottles.Diagnostics;
using Bottles.PackageLoaders.Assemblies;
using FubuCore;

namespace BottleServiceRunner
{
    public class BottleServicePackageLoader : IPackageLoader
    {
        private readonly string[] _assemblyNames;
        private readonly IFileSystem _fileSystem = new FileSystem();

        public BottleServicePackageLoader(string[] assemblyNames)
        {
            _assemblyNames = assemblyNames;
        }

        public IEnumerable<IPackageInfo> Load(IPackageLog log)
        {
            var assemblyNames = new FileSet
            {
                Include = "*.dll",
                DeepSearch = true
            };

            var files = _fileSystem.FindFiles(TopshelfPackageFacility.GetBottlesDirectory(), assemblyNames);

            return files.Select(AssemblyPackageInfo.For).Union(findExplicitAssemblies());
        }

        private IEnumerable<IPackageInfo> findExplicitAssemblies()
        {
            foreach (var assemblyName in _assemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);
                yield return new AssemblyPackageInfo(assembly);
            }
        } 
    }
}