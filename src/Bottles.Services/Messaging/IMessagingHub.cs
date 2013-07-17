using System;
using System.Collections.Generic;

namespace Bottles.Services.Messaging
{
    public interface IMessagingHub
    {
        IEnumerable<object> Listeners { get; }
        void AddListener(object listener);
        void RemoveListener(object listener);
        void Send<T>(T message);
        void SendJson(string json);
    }

    public static class MessagingHubExtensions
    {
        public static T WaitForMessage<T>(this IMessagingHub hub, Func<T, bool> filter, Action action, int wait = 5000)
        {
            var condition = new MessageWaitCondition<T>(filter);
            hub.AddListener(condition);
            action();

            try
            {
                return condition.Wait(wait);
            }
            finally
            {
                hub.RemoveListener(condition);
            }
        }

        public static T WaitForMessage<T>(this IMessagingHub hub, Action action, int wait = 5000)
        {
            Func<T, bool> filter = x => true;
            return hub.WaitForMessage(filter, action, wait);
        }
    }
}