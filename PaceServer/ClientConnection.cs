using System;
using System.Collections.Concurrent;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class ClientConnection
    {
        private const int Threshold = 1;

        private Thread _threadInConnection, _threadOutConnection;
        
        private bool _connectionEstablished;
        public ConcurrentQueue<Message> OutQueue;
        public ConcurrentQueue<Message> InQueue;
        public MessageQueue MessageQueue;

        public ClientConnection(ConnectionTable.ClientInformation clientInformation)
        {
            var soap = "http://" + clientInformation.Url + ":" + clientInformation.Port + "/MessageQueue.soap";
            MessageQueue = (MessageQueue)Activator.GetObject(typeof(MessageQueue), soap);

            TraceOps.Out("New ClientConnection created");
            
            OutQueue = new ConcurrentQueue<Message>();
            InQueue = new ConcurrentQueue<Message>();

            _threadInConnection = new Thread(InCommunication);
            _threadInConnection.Start();

            _threadOutConnection = new Thread(OutCommunication);
            _threadOutConnection.Start(); 
        }

        private void InCommunication()
        {
            try
            {
                while (_connectionEstablished)
                {
                    Thread.Sleep(Threshold);
                    var m = MessageQueue.ClientToServerTryDequeue();
                    if (m != null) InQueue.Enqueue(m);
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void OutCommunication()
        {
            try
            {
               while (_connectionEstablished)
                {
                    Thread.Sleep(Threshold);
                    Message m;
                    var message = OutQueue.TryDequeue(out m);
                    if (message && m != null)
                    {
                        MessageQueue.ClientToServerEnqueue(m);

                        var destination = m.GetDestination();
                        var command = m.GetCommand();
                        TraceOps.Out("Inside ClientConnection - Message: " + command + " Destination: " + destination);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void Stop()
        {
            _connectionEstablished = false;
        }
    }
}
