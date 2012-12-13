using System.Collections.Generic;
using Bottles.Diagnostics;

namespace Bottles.Services.Embedded.Tests
{
    public class RecordingService : IActivator, IDeactivator
    {
        public bool Activated { get; set; }
        public bool Deactivated { get; set; }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            Activated = true;
        }

        public void Deactivate(IPackageLog log)
        {
            Deactivated = true;
        }
    }
}