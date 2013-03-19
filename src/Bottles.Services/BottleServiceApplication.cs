namespace Bottles.Services
{
    // TODO -- This gets fancier later
    public class BottleServiceApplication
    {
        [SkipOverForProvenance]
        public BottleServiceRunner Bootstrap(string[] assemblyNames)
        {
            var facility = new BottlesServicePackageFacility(assemblyNames);

            PackageRegistry.LoadPackages(x => x.Facility(facility));
            return facility.Aggregator.ServiceRunner();
        }
    }
}