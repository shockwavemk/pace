using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PaceCommon;

namespace PaceClient
{
    class NetworkClient
    {
        private const int Threshold = 1;
        public static Hashtable ServerList = new Hashtable();
        public static Hashtable RecipientList = new Hashtable();
        private string _ipAddress;
        private int _port;
        private bool _clientRunning = true;
        private Thread _threadMessages;

        private ConcurrentQueue<Message> _outQueue;
        private ConcurrentQueue<Message> _inQueue;

        public delegate void ServerChangeEventHandler(object sender, ServerChangeEventArgs e);
        public static event ServerChangeEventHandler ServerChange;

        public int GetPort()
        {
            return _port;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public string GetIpAddress()
        {
            return _ipAddress;
        }

        public void SetIpAddress(string address)
        {
            _ipAddress = address;
        }

        public void SetOutQueue(ref ConcurrentQueue<Message> outQueue)
        {
            _outQueue = outQueue;
        }

        public void SetInQueue(ref ConcurrentQueue<Message> inQueue)
        {
            _inQueue = inQueue;
        }

        public void Start()
        {
            try
            {
                ConnectionWithServer();

                _threadMessages = new Thread(MessageWorker);
                _threadMessages.Start();

                ServerChange.Invoke(null, new ServerChangeEventArgs("Client Online"));
            }
            catch
            {
                ServerChange.Invoke(null, new ServerChangeEventArgs("Client Offline"));
            }
        }

        private void ConnectionWithServer()
        {
           var newConnection = new ServerConnection(ref _inQueue, _port, _ipAddress);
           newConnection.ConnectionRegistration += OnConnectionRegistration;
           ServerList.Add(newConnection, newConnection);
        }

        private void MessageWorker()
        {
            while (_clientRunning)
            {
                Thread.Sleep(Threshold);
                Message m;
                var message = _outQueue.TryDequeue(out m);

                if (message && m != null)
                {
                    var destination = m.GetDestination();
                    var command = m.GetCommand();
                    
                    if(destination != "")
                    {
                        var cq = (MessageQueue)RecipientList[destination];
                        if (cq != null)
                        {
                            cq.ClientToServerEnqueue(m);
                        }
                        else
                        {
                            _outQueue.Enqueue(m);
                        }
                    }
                    else
                    {
                        foreach (DictionaryEntry item in RecipientList)
                        {
                            var cq = (MessageQueue)item.Value;
                            TraceOps.Out("All - Message: " + command + " Destination: " + destination);
                            cq.ClientToServerEnqueue(m);
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            foreach (DictionaryEntry item in ServerList)
            {
                var sc = (ServerConnection) item.Value;
                sc.Stop();
            }
        }
        
        public static void OnServerChange(ServerChangeEventArgs e)
        {
            var statusHandler = ServerChange;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }

        public static void OnConnectionRegistration(ServerConnection sender, ConnectionRegistrationEventArgs connectionRegistrationEventArgs)
        {
            var destination = connectionRegistrationEventArgs.ConnectionHash;
            TraceOps.Out("Registration: " + destination);
            RecipientList.Add(destination, sender.MessageQueue);
        }
    }
}
