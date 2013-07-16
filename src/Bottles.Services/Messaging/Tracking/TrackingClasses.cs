using System;
using System.Linq;
using FubuCore;

namespace Bottles.Services.Messaging.Tracking
{
    public static class MessageHistory
    {
        
    }

    public class MessageTrack
    {
        public static readonly string Sent = "Sent";
        public static readonly string Received = "Received";



        public static MessageTrack ForSent(object message, string id = null)
        {
            var track = derive(message, id);
            track.Status = Sent;
            return track;
        }

        private static MessageTrack derive(object message, string id)
        {
            var messageType = message.GetType();
            var track = new MessageTrack
            {
                Description = message.ToString(),
                FullName = messageType.FullName,
                Type = messageType.Name,
                Timestamp = DateTime.UtcNow,
                Id = id
            };

            if (id.IsEmpty())
            {
                autodetermineId(message, messageType, track);
            }

            return track;
        }

        private static void autodetermineId(object message, Type messageType, MessageTrack track)
        {
            var property = messageType.GetProperties().FirstOrDefault(x => x.Name.EqualsIgnoreCase("Id"));
            if (property != null)
            {
                var rawValue = property.GetValue(message, null);
                if (rawValue != null)
                {
                    track.Id = rawValue.ToString();
                }
            }
        }

        public static MessageTrack ForReceived(object message, string id = null)
        {
            var track = derive(message, id);
            track.Status = Received;
            return track;
        }

        public string FullName { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }

        
    }

}