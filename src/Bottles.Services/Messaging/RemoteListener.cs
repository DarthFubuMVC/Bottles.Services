using System;

namespace Bottles.Services.Messaging
{
    public class RemoteListener : MarshalByRefObject, IRemoteListener
    {
        private readonly IMessagingHub _messagingHub;

        public RemoteListener(IMessagingHub messagingHub)
        {
            _messagingHub = messagingHub;
        }

        public void Send(string json)
        {
            _messagingHub.SendJson(json);
        }

        /// <summary>
        /// Really only for testing
        /// </summary>
        /// <param name="message"></param>
        public void SendObject(object message)
        {
            Send(MessagingHub.ToJson(message));
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public T WaitForMessage<T>(Func<T, bool> filter, Action action, int wait = 5000)
        {
            var condition = new MessageWaitCondition<T>(filter);
            _messagingHub.AddListener(condition);
            action();

            try
            {
                return condition.Wait();
            }
            finally
            {
                _messagingHub.RemoveListener(condition);
            }
        }

    }
}