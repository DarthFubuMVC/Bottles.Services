using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bottles.Diagnostics;
using FubuCore;

namespace Bottles.Services
{
    public class BottleServiceFinder
    {
        public static IEnumerable<IBootstrapper> FindBootstrappers(IEnumerable<Assembly> packageAssemblies)
        {
            var bootstrappers = new List<Type>();

            packageAssemblies.Each(x =>
            {
                try
                {
                    var types = x.GetExportedTypes();
                    bootstrappers.AddRange(types.Where(type => type.CanBeCastTo<IBootstrapper>() && type.IsConcreteWithDefaultCtor()));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to find exported types from assembly " + x.FullName, ex);
                }
            });

            return bootstrappers.Select(x => (IBootstrapper) Activator.CreateInstance(x));
        }

        public static IEnumerable<IBottleService> Find(IEnumerable<Assembly> packageAssemblies, IPackageLog log)
        {
            var bootstrappers = FindBootstrappers(packageAssemblies);
            return bootstrappers
                .SelectMany(x => x.Bootstrap(log))
                .Where(BottleService.IsBottleService)
                .Select(x => new BottleService(x, log))
                .ToList();
        }
    }
}