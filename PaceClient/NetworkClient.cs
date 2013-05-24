using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PaceClient
{
    class NetworkClient
    {
        private TcpClient _clientSocket;
        private IPAddress _ipAddress;
        private int _port;
        private bool _clientConnected = false;
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
                _clientSocket = new TcpClient();
                _clientSocket.Connect(GetIpAddress(), GetPort());

                FlushStream();
                ServerChange.Invoke(null, new ServerChangeEventArgs("Online"));
            }
            catch
            {
                ServerChange.Invoke(null, new ServerChangeEventArgs("Offline"));
            }
        }

        public void Stop()
        {
            
        }

        //TODO: refactor this functions

        private void FlushStream()
        {
            var stream = _clientSocket.GetStream();
            var sw = new StreamWriter(stream);
            sw.Flush();
        }

        public static void OnServerChange(ServerChangeEventArgs e)
        {
            ServerChangeEventHandler statusHandler = ServerChange;
            if (statusHandler != null)
            {
                statusHandler(null, e);
            }
        }
    }
}
