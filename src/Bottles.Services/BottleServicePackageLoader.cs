using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bottles.Diagnostics;
using Bottles.PackageLoaders.Assemblies;
using FubuCore;

namespace Bottles.Services
{
    public class BottleServicePackageLoader : IPackageLoader
    {
        private readonly IFileSystem _fileSystem = new FileSystem();

	    public BottleServicePackageLoader()
	    {
			Caveman.Log("Created BottleServicePackageLoader");
	    }

	    public IEnumerable<IPackageInfo> Load(IPackageLog log)
        {
			Caveman.Log("Starting the BottleServicePackageLoader");
		    try
		    {
			   var  assemblyNames = new FileSet
				    {
					    Include = "*.dll",
					    DeepSearch = true
				    };
				Caveman.Log("Looking for assemblies in " + BottlesServicePackageFacility.GetBottlesDirectory());
				var files = _fileSystem.FindFiles(BottlesServicePackageFacility.GetBottlesDirectory(), assemblyNames);
				Caveman.Log("Found assemblies: " + files.Count());

				return files.Select(AssemblyPackageInfo.For).ToArray();
		    }
		    catch (Exception e)
		    {
			    Caveman.Log(e.ToString());
			    throw;
		    }
			
        }

    }
}