using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PaceCommon
{
    [Serializable]
    public class MessageQueue : MarshalByRefObject
    {
        private ConcurrentDictionary<string, ConcurrentQueue<Message>> _concurrentDictionary;
        private static MessageQueue _messageQueue;

        public MessageQueue()
        {
            _concurrentDictionary = new ConcurrentDictionary<string, ConcurrentQueue<Message>>();
        }

        public static MessageQueue GetRemote(string ip, int port)
        {
            if (_messageQueue == null)
            {
                _messageQueue =
                    (MessageQueue)
                    Activator.GetObject(typeof (MessageQueue), "http://" + ip + ":" + port + "/MessageQueue.rem");
            }

            return _messageQueue;
        }
        
        public Message GetMessage(string destination)
        {
            if (destination != null)
            {
                Message m;
                var cq = _concurrentDictionary.GetOrAdd(destination, ConcurrentQueueFactory);
                cq.TryDequeue(out m);
                return m;
            }
            return null;
        }

        public int GetCount(string destination)
        {
            if (destination != null)
            {
                Message m;
                var cq = _concurrentDictionary.GetOrAdd(destination, ConcurrentQueueFactory);
                return cq.Count;
            }
            return 0;
        }

        private ConcurrentQueue<Message> ConcurrentQueueFactory(string s)
        {
            var cq = new ConcurrentQueue<Message>();
            var p = new string[,] {{"p1", "v1"}, {"p2", "v2"}};
            var m = new Message(p, true, "registered as '" + s + "'", "");
            cq.Enqueue(m);
            return cq;
        }

        public bool SetMessage(Message message)
        {
            var cq = _concurrentDictionary.GetOrAdd(message.GetDestination(), new ConcurrentQueue<Message>());
            cq.Enqueue(message);
            return true;
        }

    }
}