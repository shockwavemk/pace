using System;
using System.Collections;
using System.Collections.Generic;

namespace PaceCommon
{
    [Serializable]
    public class MessageQueue : MarshalByRefObject
    {
        private Hashtable _hashTable;
        public InOutQueue Server;
        public int test;

        public MessageQueue()
        {
            _hashTable = new Hashtable();
            Server = new InOutQueue();
            test = 5;
        }

        public string Test()
        {
            return "Test: "+ test;
        }

        public Message GetMessage(string destination)
        {
            var rlist = new List<string> { "" };
            Message m;
            if (destination == "server")
            {
                var b = Server.ServerToClientQueue.TryDequeue(out m);
                //m = new Message(rlist, true, "ping:"+test, "");
            }
            else
            {
                m = new Message(rlist, true, "ping2", "");
            }
            return m;
        }

        public void ServerEnqueue(Message message)
        {
            Server.ServerToClientQueue.Enqueue(message);
            Server.ClientToServerQueue.Enqueue(message);
        }
    }
}