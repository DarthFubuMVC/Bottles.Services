using System;
using FubuCore;

namespace Bottles.Topshelf
{
    public class TopshelfPackageFacility : PackageFacility
    {
        private readonly BottleServiceAggregator _aggregator = new BottleServiceAggregator();

        public TopshelfPackageFacility()
        {
            Bootstrapper(_aggregator);
            Loader(new ServiceBottleLoader());
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