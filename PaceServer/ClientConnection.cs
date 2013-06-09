using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class ClientConnection
    {
        private const int Threshold = 1;

        private Thread _threadInConnection, _threadOutConnection;
        
        private bool _connectionEstablished;
        private string _buffer;
        public ConcurrentQueue<Message> OutQueue;
        private ConcurrentQueue<Message> _inQueue;
        public MessageQueue MessageQueue;

        public ClientConnection(ConnectionTable.ClientInformation clientInformation, ref ConcurrentQueue<Message> inQueue)
        {
            var soap = "http://" + clientInformation.Url + ":" + clientInformation.Port + "/MessageQueue.soap";
            MessageQueue = (MessageQueue)Activator.GetObject(typeof(MessageQueue), soap);

            TraceOps.Out("New ClientConnection created");
            
            OutQueue = new ConcurrentQueue<Message>();
            _inQueue = inQueue;

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
                    _inQueue.Enqueue(m);
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
                    OutQueue.TryDequeue(out m);
                    MessageQueue.ClientToServerEnqueue(m);

                    var destination = m.GetDestination();
                    var command = m.GetCommand();
                    TraceOps.Out("Inside ClientConnection - Message: " + command + " Destination: " + destination);
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
