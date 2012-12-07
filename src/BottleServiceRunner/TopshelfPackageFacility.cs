using System;
using Bottles;
using Bottles.Services;
using FubuCore;

namespace BottleServiceRunner
{
    public class TopshelfPackageFacility : PackageFacility
    {
        private readonly BottleServiceAggregator _aggregator = new BottleServiceAggregator();

        public TopshelfPackageFacility()
        {
            Bootstrapper(_aggregator);
            Loader(new BottleServicePackageLoader());
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