using System;

namespace Bottles.Services.Messaging
{
    public class RemoteListener : MarshalByRefObject, IRemoteListener
    {
        private readonly MessagingHub _listener;

        public RemoteListener(MessagingHub listener)
        {
            _listener = listener;
        }

        public void Send(string json)
        {
            _listener.SendJson(json);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public T WaitForMessage<T>(Action action)
        {
            return WaitForMessage<T>(t => true, action);
        }

        public T WaitForMessage<T>(Func<T, bool> condition, Action action)
        {
            throw new NotImplementedException();
        }

    }
}