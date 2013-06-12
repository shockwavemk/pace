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
        private MessageQueue _messageQueue;
        private string _name;

        public ClientConnection(ref MessageQueue messageQueue, string name)
        {
            TraceOps.Out("New ClientConnection created");
            _messageQueue = messageQueue;
            _name = name;

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
                    var m = _messageQueue.ClientToServerTryDequeue(_messageQueue.Get(_name));
                    if (m != null) _messageQueue.ClientToServerEnqueue(m, _messageQueue.Server);
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
                    
                    var m = _messageQueue.ServerToClientTryDequeue(_messageQueue.Server);
                    if (m != null)
                    {
                        _messageQueue.ClientToServerEnqueue(m, _messageQueue.Get(_name));
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
