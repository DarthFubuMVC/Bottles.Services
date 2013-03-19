using Bottles;

namespace BottleServiceRunner
{
    // TODO -- This gets fancier later
    public class TopshelfApplication
    {
        [SkipOverForProvenance]
        public Bottles.Services.BottleServiceRunner Bootstrap(string[] assemblyNames)
        {
            var facility = new TopshelfPackageFacility(assemblyNames);

            PackageRegistry.LoadPackages(x => x.Facility(facility));
            return facility.Aggregator.ServiceRunner();
        }
    }
}