using System;
using System.Collections;
using System.Threading;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceServer
{
    public class TaskManager
    {
        private const int Threshold = 100;
        private bool _running = true;
        private Thread _threadTasks;
        private MessageQueue _messageQueue;
        private Hashtable _hashTable;
        private string _name;

        public delegate void TaskEventHandler(Message message);
        public event TaskEventHandler Task;

        public TaskManager(ref MessageQueue messageQueue, ref string name)
        {
            _running = true;
            _messageQueue = messageQueue;
            _hashTable = new Hashtable();
            _threadTasks = new Thread(Tasks);
            _threadTasks.Start();
            _name = name;
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

                    var m = _messageQueue.GetMessage(_name);
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
