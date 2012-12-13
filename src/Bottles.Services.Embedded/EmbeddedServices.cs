using FubuMVC.Core;

namespace Bottles.Services.Embedded
{
    public class EmbeddedServices : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Services(x => x.AddService<IActivator, BottleServiceActivator>());
        }
    }
}