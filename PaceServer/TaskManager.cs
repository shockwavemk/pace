using System;
using System.Collections;
using System.Threading;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceServer
{
    class TaskManager
    {
        private const int Threshold = 1;
        private bool _running = true;
        private Thread _threadTasks;
        private MessageQueue _messageQueue;
        private Hashtable _hashTable;

        public delegate void TaskEventHandler(Message message);
        public event TaskEventHandler Task;

        public TaskManager(ref MessageQueue messageQueue)
        {
            _running = true;
            _messageQueue = messageQueue;
            _hashTable = new Hashtable();
            _threadTasks = new Thread(Tasks);
            _threadTasks.Start();
        }

        public void Stop()
        {
            _running = false;
        }

        private void Tasks()
        {
            try
            {
                while (_running)
                {
                    Thread.Sleep(Threshold);

                    var m = _messageQueue.GetMessage("server");
                    if (m != null)
                    {
                        Task.Invoke(m);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }
    }
}
