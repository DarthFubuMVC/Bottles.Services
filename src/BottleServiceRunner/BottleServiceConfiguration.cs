using System.Xml.Serialization;

namespace BottleServiceRunner
{
    [XmlType("service")]
    public class BottleServiceConfiguration
    {
        public const string FILE = "bottle-service.config";

        public BottleServiceConfiguration()
        {
            var defaultValue = typeof (Bottles.Services.BottleServiceRunner).Name + "Service";

            Name = defaultValue;
            DisplayName = defaultValue;
            Description = defaultValue;
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}