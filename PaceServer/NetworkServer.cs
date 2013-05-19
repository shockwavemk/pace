﻿using System;
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
                _clientSocket = _serverSocket.AcceptTcpClient();
                var newConnection = new ClientConnection(_clientSocket);
            }
        }

        public static void OnClientChange(ClientChangeEventArgs e)
        {
            ClientChangeEventHandler statusHandler = ClientChange;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }
    }
}
