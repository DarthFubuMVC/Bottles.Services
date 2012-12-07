using FubuMVC.Core;

namespace Bottles.Services.Embedded
{
    public class EmbeddedServices : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            // TODO -- Consider switching between activator/bootstrapper based on some settings
            registry.Services(x => x.AddService<IActivator, BottleServiceActivator>());
        }
    }
}