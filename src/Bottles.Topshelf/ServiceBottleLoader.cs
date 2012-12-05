using System.Collections.Generic;
using System.Linq;
using Bottles.Diagnostics;
using Bottles.PackageLoaders.Assemblies;
using FubuCore;

namespace Bottles.Topshelf
{
    public class ServiceBottleLoader : IPackageLoader
    {
        private readonly IFileSystem _fileSystem = new FileSystem();

        public IEnumerable<IPackageInfo> Load(IPackageLog log)
        {
            var assemblyNames = new FileSet
            {
                Include = "*.dll",
                DeepSearch = false
            };

            var files = _fileSystem.FindFiles(TopshelfPackageFacility.GetBottlesDirectory(), assemblyNames);
            return files.Select(AssemblyPackageInfo.For);
        }
    }
}