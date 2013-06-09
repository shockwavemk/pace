using System;
using System.Collections.Concurrent;

namespace PaceCommon
{
    [Serializable]
    public class MessageQueue : MarshalByRefObject
    {
        public ConcurrentQueue<Message> ServerToClientQueue, ClientToServerQueue;

        public MessageQueue()
        {
            ServerToClientQueue = new ConcurrentQueue<Message>();
            ClientToServerQueue = new ConcurrentQueue<Message>();
        }

        public Message ServerToClientTryDequeue()
        {
            Message m;
            var message = ServerToClientQueue.TryDequeue(out m);
            if (message && m != null)
            {
                return m;
            }
            return null;
        }

        public void ServerToClientEnqueue(Message m)
        {
            ServerToClientQueue.Enqueue(m);
        }

        public Message ClientToServerTryDequeue()
        {
            Message m;
            var message = ClientToServerQueue.TryDequeue(out m);
            if (message && m != null)
            {
                return m;
            }
            return null;
        }

        public void ClientToServerEnqueue(Message m)
        {
            ClientToServerQueue.Enqueue(m);
        }
    }
}