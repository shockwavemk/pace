﻿using System;
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
        }

        public Message GetMessage(string destination)
        {
            var cq = (ConcurrentQueue<Message>)_hashTable[destination];
            Message m;
            if (cq == null)
            {
                cq = new ConcurrentQueue<Message>();
                _hashTable.Add(destination, cq);

                var rlist = new List<string> { "" };
                m = new Message(rlist, true, "registered", "");
            }
            else
            {
                cq.TryDequeue(out m);
            }

            return m;
        }

        public void SetMessage(Message message)
        {
            var cq = (ConcurrentQueue<Message>)_hashTable[message.GetDestination()];
            if (cq == null)
            {
                cq = new ConcurrentQueue<Message>();
                _hashTable.Add(message.GetDestination(), cq);

                cq.Enqueue(message);
            }
            else
            {
                cq.Enqueue(message);
            }
        }
    }
}