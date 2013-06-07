using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class NetworkServer
    {
        public static Hashtable ClientList = new Hashtable();
        public static Hashtable RecipientList = new Hashtable();
        private TcpListener _serverSocket;
        private TcpClient _clientSocket;
        private IPAddress _ipAddress;
        private int _port;
        private bool _serverRunning = true;
        private Thread _threadListener;
        private Thread _threadMessages;
        
        private ConcurrentQueue<Message> _inQueue;
        private ConcurrentQueue<Message> _outQueue;

        public delegate void ClientChangeEventHandler(object sender, ClientChangeEventArgs e);
        public static event ClientChangeEventHandler ClientChange;

        public int GetPort()
        {
            return _port;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public IPAddress GetIpAddress()
        {
            return _ipAddress;
        }

        public void SetIpAddress(string address)
        {
            _ipAddress = IPAddress.Parse(address);
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
                _serverSocket = new TcpListener(this.GetIpAddress(), this.GetPort());
                _clientSocket = default(TcpClient);
                _serverRunning = true;

                _serverSocket.Start();

                _threadListener = new Thread(ListenForNewClients);
                _threadListener.Start();

                _threadMessages = new Thread(MessageWorker);
                _threadMessages.Start();

                ClientChange.Invoke(null, new ClientChangeEventArgs("Server Online"));
            }
            catch
            {
                ClientChange.Invoke(null, new ClientChangeEventArgs("Server Offline"));
            }
        }

        public void Stop()
        {
            _serverRunning = false;
            foreach (var cc in from DictionaryEntry item in ClientList select (ClientConnection)item.Value)
            {
                cc.Stop();
            }
        }

        private void ListenForNewClients()
        {
            while (_serverRunning)
            {
                Thread.Sleep(500);
                _clientSocket = _serverSocket.AcceptTcpClient();
                var newConnection = new ClientConnection(_clientSocket, ref _inQueue);
                newConnection.ConnectionRegistration += OnConnectionRegistration;
                ClientList.Add(_clientSocket, newConnection);
            }
        }

        private void MessageWorker()
        {
            while (_serverRunning)
            {
                Thread.Sleep(500);
                Message m; 
                var message = _outQueue.TryDequeue(out m) ? m : null;

                if (message != null)
                {
                    var destination = message.GetDestination();
                    var cq = (ConcurrentQueue<Message>) RecipientList[destination];
                    if (cq != null)
                    {
                        cq.Enqueue(message);
                    }
                    else
                    {
                        _outQueue.Enqueue(message);
                    }
                }
            }
        }

        public static void OnClientChange(ClientChangeEventArgs e)
        {
            var statusHandler = ClientChange;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }

        public static void OnConnectionRegistration(ClientConnection sender, ConnectionRegistrationEventArgs connectionRegistrationEventArgs)
        {
            var destination = connectionRegistrationEventArgs.ConnectionHash;
            RecipientList.Add(destination, sender.OutQueue);
        }
    }
}
