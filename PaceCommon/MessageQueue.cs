using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PaceCommon
{
    [Serializable]
    public class MessageQueue : MarshalByRefObject
    {
        private Hashtable _hashTable;
        public InOutQueue Server;

        public MessageQueue()
        {
            _hashTable = new Hashtable();
            Server = new InOutQueue();
        }

        public InOutQueue Get(string destination)
        {
            var inOutQueue = (InOutQueue)_hashTable[destination];
            if (inOutQueue == null)
            {
                inOutQueue = new InOutQueue();
                _hashTable.Add(destination, inOutQueue);
            }
            return inOutQueue;
        }

        public Message ServerToClientTryDequeue(InOutQueue inOutQueue)
        {
            Message m;
            var message = inOutQueue.ServerToClientQueue.TryDequeue(out m);
            if (message && m != null)
            {
                return m;
            }
            return null;
        }

        public void ServerToClientEnqueue(Message m, InOutQueue inOutQueue)
        {
            inOutQueue.ServerToClientQueue.Enqueue(m);
        }

        public Message ClientToServerTryDequeue(InOutQueue inOutQueue)
        {
            Message m;
            var message = inOutQueue.ClientToServerQueue.TryDequeue(out m);
            if (message && m != null)
            {
                return m;
            }
            return null;
        }

        public void ClientToServerEnqueue(Message m, InOutQueue inOutQueue)
        {
            inOutQueue.ClientToServerQueue.Enqueue(m);
        }

        public string Test()
        {
            return "Test!";
        }

        public InOutQueue getServer()
        {
            return Server;
        }
    }
}