using System;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class NetworkServer
    {
        private const int Threshold = 1;
        private bool _serverRunning = true;
        private Thread _threadPing;
        private MessageQueue _messageQueue;

        public NetworkServer(ref MessageQueue messageQueue, ref ConnectionTable connectionTable)
        {
            _serverRunning = true;
            _messageQueue = messageQueue;
            _threadPing = new Thread(MessageWorker);
            _threadPing.Start();
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
                     * Task task = new Task(() => { try { obj.Ping(); } catch {} });
                        task.Start();
                        if(!task.Wait(1000)) throw new TimeoutException();
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
