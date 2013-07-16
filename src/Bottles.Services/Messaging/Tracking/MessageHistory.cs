using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FubuCore;

namespace Bottles.Services.Messaging.Tracking
{
    public static class MessageHistory
    {
        private readonly static IList<MessageTrack> _sent = new List<MessageTrack>();
        private readonly static IList<MessageTrack> _received = new List<MessageTrack>();
        private readonly static IList<MessageTrack> _outstanding = new List<MessageTrack>();
        
        private readonly static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public static void ClearAll()
        {
            _lock.Write(() => {
                _sent.Clear();
                _received.Clear();
                _outstanding.Clear();
            });
        }

        public static void Record(MessageTrack track)
        {
            _lock.Write(() => {
                if (track.Status == MessageTrack.Sent)
                {
                    _sent.Add(track);
                    _outstanding.Add(track);
                }
                else
                {
                    _received.Add(track);
                    _outstanding.Remove(track);
                }
            });

            _lock.Read(() => {
                if (!_outstanding.Any())
                {
                    EventAggregator.SendMessage(new AllMessagesComplete());
                }

                return true;
            });


        }

        public static IEnumerable<MessageTrack> Received()
        {
            return _lock.Read(() => _received.ToArray());
        } 

        public static IEnumerable<MessageTrack> Outstanding()
        {
            return _lock.Read(() => _outstanding.ToArray());
        } 

        public static IEnumerable<MessageTrack> All()
        {
            return _lock.Read(() => _sent.Union(_received).ToList());
        } 

    }
}