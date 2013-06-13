using System;
using System.Threading;
using PaceCommon;

namespace PaceClient
{
    class NetworkClient
    {
        private const int Threshold = 1;
        private bool _clientRunning = true;
        private Thread _threadMessages;
        private MessageQueue _messageQueue;
        private ConnectionTable _connectionTable;
        private string _name;

        public NetworkClient(ref MessageQueue messageQueue, ref ConnectionTable connectionTable, string name)
        {
            _clientRunning = true;
            _name = name;
            _messageQueue = messageQueue;
            _connectionTable = connectionTable;

            _threadMessages = new Thread(MessageWorker);
            _threadMessages.Start();
        }

        private void Register()
        {
            _connectionTable.Get(_name);
        }

        private void MessageWorker()
        {
            try
            {
                Register();

                while (_clientRunning)
                {
                    Thread.Sleep(Threshold);
                    
                    /*
                    var m = _messageQueue.ServerToClientTryDequeue(_messageQueue.Server);
                    if (m != null)
                    {
                        _messageQueue.ClientToServerEnqueue(m, _messageQueue.Get(_name));
                    }
                     */
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void Stop()
        {
            _clientRunning = false;
        }
    }
}
