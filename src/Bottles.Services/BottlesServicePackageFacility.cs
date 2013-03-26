using System;
using FubuCore;

namespace Bottles.Services
{
    public class BottlesServicePackageFacility : PackageFacility
    {
        private readonly BottleServiceAggregator _aggregator = new BottleServiceAggregator();

        public BottlesServicePackageFacility()
        {
			Caveman.Log("Adding bootstrapper");
            Bootstrapper(_aggregator);
			Caveman.Log("Adding package loader");
            Loader(new BottleServicePackageLoader());
			Caveman.Log("Able to add package loader");
        }

        public BottleServiceAggregator Aggregator { get { return _aggregator; }}

        public static string GetApplicationDirectory()
        {
	        return AppDomain.CurrentDomain.BaseDirectory;
        }

		public static string GetBottlesDirectory()
		{
			return GetApplicationDirectory().AppendPath("bottles");
		}

        public override string ToString()
        {
            return "Topshelf Package Facility";
        }
    }
}