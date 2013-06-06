using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class NetworkServer
    {
        public static Hashtable ClientList = new Hashtable();
        private TcpListener _serverSocket;
        private TcpClient _clientSocket;
        private IPAddress _ipAddress;
        private int _port;
        private bool _serverRunning = true;
        private Thread _threadListener;
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
        }

        private void ListenForNewClients()
        {
            while (_serverRunning)
            {
                _clientSocket = _serverSocket.AcceptTcpClient();
                var newConnection = new ClientConnection(_clientSocket);
                ClientList.Add(_clientSocket, newConnection);
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

        public void SetOutQueue(ref ConcurrentQueue<Message> outQueue)
        {
            throw new System.NotImplementedException();
        }

        public void SetInQueue(ref ConcurrentQueue<Message> inQueue)
        {
            throw new System.NotImplementedException();
        }
    }
}
