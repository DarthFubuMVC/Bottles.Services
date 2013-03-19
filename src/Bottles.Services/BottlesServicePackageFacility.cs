using System;
using FubuCore;

namespace Bottles.Services
{
    public class BottlesServicePackageFacility : PackageFacility
    {
        private readonly BottleServiceAggregator _aggregator = new BottleServiceAggregator();

        public BottlesServicePackageFacility(string[] assemblyNames)
        {
            Bootstrapper(_aggregator);
            Loader(new BottleServicePackageLoader(assemblyNames));
        }

        public BottleServiceAggregator Aggregator { get { return _aggregator; }}

        public static string GetBottlesDirectory()
        {
            return FileSystem.Combine(GetApplicationDirectory(), BottleFiles.BottlesFolder);
        }

        public static string GetApplicationDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override string ToString()
        {
            return "Topshelf Package Facility";
        }
    }
}