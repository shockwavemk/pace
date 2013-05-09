using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaceServer
{
    class NetworkServer
    {
        public static Hashtable ClientList = new Hashtable();
        private TcpListener _serverSocket;
        private TcpClient _clientSocket;
        private IPAddress _ipAddress;
        private bool _serverRunning = true;
        private Thread _threadListener;

        public void SetPort(int port)
        {

        }

        public int GetPort()
        {
            return 0;
        }

        public System.Net.IPAddress GetIpAddress()
        {
            return _ipAddress;
        }

        public void SetIpAddress(string address)
        {
            _ipAddress = IPAddress.Parse(address);
        }

        

        public void Start()
        {
            _serverSocket = new TcpListener(this.GetIpAddress(),this.GetPort());
            _clientSocket = default(TcpClient);
            int counter = 0;
            _serverRunning = true;

            _serverSocket.Start();
            _threadListener = new Thread(ListenForNewClients);
            _threadListener.Start();
        }

        public void Stop()
        {
            _serverRunning = false;
        }

        private void ListenForNewClients()
        {
            while (_serverRunning == true)
            {
                tcpClient = _serverSocket.AcceptTcpClient();
                Connection newConnection = new Connection(tcpClient);
            }
        }
    }
}
