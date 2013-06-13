using System;
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

        public NetworkServer(ref MessageQueue messageQueue)
        {
            _serverRunning = true;
            _messageQueue = messageQueue;
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
                    /*
                    var m = _messageQueue.ServerToClientTryDequeue(_messageQueue.Server);
                        
                    if (m != null)
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
                     */
                }
    
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }
    }
}
