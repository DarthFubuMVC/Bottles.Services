namespace Bottles.Services
{
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