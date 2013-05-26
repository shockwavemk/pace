using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PaceClient
{
    class NetworkClient
    {
        private TcpClient _clientSocket;
        private IPAddress _ipAddress;
        private int _port;
        private bool _clientRunning = true;
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
                ConnectionWithServer();
                ServerChange.Invoke(null, new ServerChangeEventArgs("Client Online"));
            }
            catch
            {
                ServerChange.Invoke(null, new ServerChangeEventArgs("Client Offline"));
            }
        }

        private void ConnectionWithServer()
        {
           _clientSocket = new TcpClient();
           _clientSocket.Connect(GetIpAddress(), GetPort());
           var newConnection = new ServerConnection(_clientSocket);
        }

        public void Stop()
        {
            _clientRunning = false;
        }
        
        public static void OnServerChange(ServerChangeEventArgs e)
        {
            var statusHandler = ServerChange;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }
    }
}
