using System;
using System.Diagnostics;
using System.Threading;
using PaceCommon;

namespace PaceClient
{
    class NetworkClient
    {
        private const int Threshold = 1000;
        private bool _clientRunning = true;
        private Thread _threadMessages;
        private MessageQueue _messageQueue;
        private ConnectionTable _connectionTable;
        private PerformanceCounter _cpuCounter;
        private string _name;

        public NetworkClient(ref MessageQueue messageQueue, ref ConnectionTable connectionTable, string name)
        {
            _clientRunning = true;
            _name = name;
            _messageQueue = messageQueue;
            _connectionTable = connectionTable;
            _cpuCounter = new PerformanceCounter { CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total" };
            _threadMessages = new Thread(MessageWorker);
            _threadMessages.Start();
        }

        private void Register()
        {
            var ci = _connectionTable.Get(_name);

            //TODO Demo
            ci.SetApplicationNames("Demo Application 1, Demo Application 2");
            ci.SetIp("localhost");
            ci.SetPort(9091);

            _connectionTable.Set(_name, ci);
        }

        private void MessageWorker()
        {
            try
            {
                Register();

                while (_clientRunning)
                {
                    Thread.Sleep(Threshold);

                    var cpu = _cpuCounter.NextValue();
                    
                    var ci = _connectionTable.Get(_name);
                    ci.SetPerformance(cpu);
                    _connectionTable.Set(_name, ci);
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
