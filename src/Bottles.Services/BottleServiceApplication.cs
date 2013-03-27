using System;
using FubuCore;

namespace Bottles.Services
{
//    public static class BottleLogger
//    {
//        public static readonly string File = @"c:\test\BottleService.log";
//        private static readonly IFileSystem fileSystem = new FileSystem();
//
//        static BottleLogger()
//        {
//            fileSystem.DeleteFile(File);
//        }
//
//        public static void Log(string message)
//        {
//            try
//            {
//                fileSystem.AlterFlatFile(File, list => list.Add(message));
//            }
//            catch (Exception)
//            {
//                // nothing
//            }
//        }
//
//        public static void WriteFailure(string failure)
//        {
//            fileSystem.WriteStringToFile(@"c:\test\failure.log", failure);
//        }
//    }

    // TODO -- This gets fancier later
    public class BottleServiceApplication
    {
        [SkipOverForProvenance]
        public BottleServiceRunner Bootstrap()
        {
            var facility = new BottlesServicePackageFacility();
            PackageRegistry.LoadPackages(x => x.Facility(facility));
            

            PackageRegistry.AssertNoFailures();

            return facility.Aggregator.ServiceRunner();
        }
    }
}