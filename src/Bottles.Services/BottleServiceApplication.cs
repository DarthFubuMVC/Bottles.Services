using FubuCore;

namespace Bottles.Services
{
	public static class Caveman
	{
		public static readonly string File = @"c:\test\caveman.log";

		static Caveman()
		{
			new FileSystem().DeleteFile(File);
		}

		public static void Log(string message)
		{
			new FileSystem().AlterFlatFile(File, list => list.Add(message));
		}
	}

    // TODO -- This gets fancier later
    public class BottleServiceApplication
    {
        [SkipOverForProvenance]
        public BottleServiceRunner Bootstrap()
        {
			Caveman.Log("started");	
            var facility = new BottlesServicePackageFacility();
			Caveman.Log("registered facility");

            PackageRegistry.LoadPackages(x => x.Facility(facility));

			Caveman.Log("loaded packages:");
	
			

            return facility.Aggregator.ServiceRunner();
        }
    }
}