﻿using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Bottles.Services.Messaging
{
    public static class EventAggregator
    {
        private static readonly BlockingCollection<object> _messages;
        private static IRemoteListener _remoteListener;
        private static CancellationTokenSource _cancellationSource;

        static EventAggregator()
        {
            _messages = new BlockingCollection<object>(new ConcurrentQueue<object>());
        }

        public static void Start(IRemoteListener remoteListener)
        {
            _remoteListener = remoteListener;

            _cancellationSource = new CancellationTokenSource();
            Task.Factory.StartNew(read, _cancellationSource.Token);
        }

        private static void read()
        {
            foreach (object o in _messages.GetConsumingEnumerable(_cancellationSource.Token))
            {
                // TODO -- should this be async as well?  Or assume that the remote listener will handle it?
                var json = MessagingHub.ToJson(o);
                _remoteListener.Send(json);

                // TODO -- send to a local messaging hub?
            }
        }

        public static void Stop()
        {
            if (_cancellationSource != null) _cancellationSource.Cancel();
        }

        public static void SendMessage(string category, string message)
        {
            SendMessage(new ServiceMessage
            {
                Category = category,
                Message = message
            });
        }

        public static void SendMessage(object message)
        {
            _messages.Add(message);
        }
    }
}