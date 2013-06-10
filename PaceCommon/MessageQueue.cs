using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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

        public string Test()
        {
            return "Test!";
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