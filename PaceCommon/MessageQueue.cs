using System;
using System.Collections.Concurrent;

namespace PaceCommon
{
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
            ServerToClientQueue.TryDequeue(out m);
            return m;
        }

        public void ServerToClientEnqueue(Message m)
        {
            ServerToClientQueue.Enqueue(m);
        }

        public Message ClientToServerTryDequeue()
        {
            Message m;
            ClientToServerQueue.TryDequeue(out m);
            return m;
        }

        public void ClientToServerEnqueue(Message m)
        {
            ClientToServerQueue.Enqueue(m);
        }
    }
}