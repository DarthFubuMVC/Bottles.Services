using Bottles;

namespace BottleServiceRunner
{
    // TODO -- This gets fancier later
    public class TopshelfApplication
    {
        private TopshelfPackageFacility _facility;

        [SkipOverForProvenance]
        public Bottles.Services.BottleServiceRunner Bootstrap()
        {
            _facility = new TopshelfPackageFacility();

            PackageRegistry.LoadPackages(x => x.Facility(_facility));
            return _facility.Aggregator.ServiceRunner();
        }
    }
}