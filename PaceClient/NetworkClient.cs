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
        private string _name;

        public NetworkClient(ref MessageQueue messageQueue, string name)
        {
            _clientRunning = true;
            _name = name;
            _messageQueue = messageQueue;

            _threadMessages = new Thread(MessageWorker);
            _threadMessages.Start();
        }

        public void MessageWorker()
        {
            try
            {
                while (_clientRunning)
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
            _clientRunning = false;
        }
    }
}
