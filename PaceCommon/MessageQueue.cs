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

        public MessageQueue()
        {
            _concurrentDictionary = new ConcurrentDictionary<string, ConcurrentQueue<Message>>();
        }

        public static MessageQueue GetRemote(string ip, int port)
        {
            return (MessageQueue)Activator.GetObject(typeof(MessageQueue), "http://"+ip+":"+port+"/MessageQueue.rem");
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

        private ConcurrentQueue<Message> ConcurrentQueueFactory(string s)
        {
            var cq = new ConcurrentQueue<Message>();
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "registered as '" + s + "'", "");
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