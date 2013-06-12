using System;
using System.Collections;
using System.Linq;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class NetworkServer
    {
        private const int Threshold = 1;
        private bool _serverRunning = true;
        private Thread _threadMessages;
        private MessageQueue _messageQueue;
        private ConnectionTable _connectionTable;

        public NetworkServer(ref MessageQueue messageQueue, ref ConnectionTable connectionTable)
        {
            _serverRunning = true;
            _messageQueue = messageQueue;
            _connectionTable = connectionTable;
            _threadMessages = new Thread(MessageWorker);
            _threadMessages.Start();
        }

        public void Stop()
        {
            _serverRunning = false;
        }

        private void MessageWorker()
        {
            try
            {
                while (_serverRunning)
                {
                    Thread.Sleep(Threshold);
                    MessageQueue.Message m;
                    var message = _messageQueue.Server.ServerToClientQueue.TryDequeue(out m);

                    if (message && m != null)
                    {
                        var destination = m.GetDestination();
                        var cc = _messageQueue.Get(destination);
                        if (cc != null)
                        {
                            cc.ServerToClientQueue.Enqueue(m);
                        }
                        else
                        {
                            _messageQueue.Server.ServerToClientQueue.Enqueue(m);
                        }
                    }
                }
    
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            
        }

        public void OnConnectionRegistration(ConnectionTable.ClientInformation sender)
        {
            var newConnection = new ClientConnection(ref _messageQueue, sender.Name);
        }
    }
}
