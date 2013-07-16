using System;

namespace Bottles.Services.Messaging.Tracking
{
    public class MessageSent
    {
        public string FullName { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class MessageReceived
    {
        public string FullName { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
    }
}