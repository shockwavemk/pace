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

        [Serializable]
        public class InOutQueue
        {
            public ConcurrentQueue<Message> ServerToClientQueue, ClientToServerQueue;
            
            public InOutQueue()
            {
                ServerToClientQueue = new ConcurrentQueue<Message>();
                ClientToServerQueue = new ConcurrentQueue<Message>();
            }
        }


        [Serializable]
        public class Message
        {
            private string Destination { get; set; }
            private string Command { get; set; }
            private Array _parameter;
            private bool Acknowledgement { get; set; }

            public Array Parameter
            {
                get { return _parameter; }
                set { _parameter = value; }
            }

            public Message(List<string> parameter, bool acknowledgement, string command, string destination)
            {
                Parameter = parameter.ToArray();
                Acknowledgement = acknowledgement;
                Command = command;
                Destination = destination;
            }

            public Message()
            {
                _parameter = new string[1];
                _parameter.SetValue("testparameter",0);
                Acknowledgement = false;
                Command = "ping";
                Destination = "all";
            }

            public string GetDestination()
            {
                return Destination;
            }

            public string GetCommand()
            {
                return Command;
            }
        }
    }
}