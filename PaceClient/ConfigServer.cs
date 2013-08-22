using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;

namespace PaceClient
{
    public delegate void ChangedEventHandler(object sender, ChangedEventArgs e);

    class ConfigServer
    {
        private const int Threshold = 100;
        private bool _running = true;
        private Thread _threadNetworkServer;
        private int _port;
        public IPAddress IpAddress = IPAddress.Any;

        
        public event ChangedEventHandler Changed;
        protected virtual void OnChanged(ChangedEventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public ConfigServer()
        {
            _running = true;
            _port = 9091;
            _threadNetworkServer = new Thread(ConfigTasks);
            _threadNetworkServer.Start();
        }

        public void Stop()
        {
            _running = false;
        }

        private void ConfigTasks()
        {
            var listener = new TcpListener(IpAddress, _port);
            listener.Start();
            while (_running)
            {
                Thread.Sleep(Threshold);
                while (!listener.Pending()) { Thread.Sleep(Threshold); }

                var newSocket = listener.AcceptSocket();

                var remoteIpEndPoint = newSocket.RemoteEndPoint as IPEndPoint;
                if (remoteIpEndPoint != null)
                {
                    var ip = remoteIpEndPoint.Address.ToString();
                    var port = ((IPEndPoint)newSocket.LocalEndPoint).Port;

                    var e = new ChangedEventArgs(ip, port);
                    OnChanged(e);
                }
            }
        }
    }

    public class ChangedEventArgs : EventArgs
    {
        private readonly string _ip;
        private readonly int _port;

        public ChangedEventArgs(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public string Ip
        {
            get { return _ip; }
        }

        public int Port
        {
            get { return _port; }
        }
    }
}
