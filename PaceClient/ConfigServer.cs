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
            Byte[] data;
            String responseData = String.Empty;
            var server = new TcpListener(IPAddress.Any, _port);
            server.Start();
            
            while (_running)
            {
                

                Thread.Sleep(Threshold);
                while (!server.Pending()) { Thread.Sleep(Threshold); }

                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                IPEndPoint localIpEndPoint = client.Client.LocalEndPoint as IPEndPoint;

                if (remoteIpEndPoint != null) { TraceOps.Out(remoteIpEndPoint.Address + " : " + remoteIpEndPoint.Port); }
                if (localIpEndPoint != null) { TraceOps.Out(localIpEndPoint.Address + " : " + localIpEndPoint.Port);
                }


                while (client.Connected)
                {
                    Thread.Sleep(Threshold);
                    data = new Byte[1024];
                    var connectIp = "";
                    var connectPort = 0;

                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    if (responseData != "")
                    {
                        
                        if (responseData.IndexOf("<PORT>", StringComparison.Ordinal) > 0)
                        {
                            connectPort = Convert.ToInt32(NetworkOps.GetValue("PORT", responseData)); 
                            TraceOps.Out("Recived PORT: "+connectPort); 
                        }
                        if (responseData.IndexOf("<IP>", StringComparison.Ordinal) > 0)
                        {
                            connectIp = NetworkOps.GetValue("IP", responseData);
                            connectIp = NetworkOps.GetIpString(connectIp);
                            TraceOps.Out("Recived IP: "+connectIp);
                        }

                        if (responseData.IndexOf("</XML>", StringComparison.Ordinal) > 0)
                        {
                            stream.Close();
                            client.Close(); 

                            if (connectIp != "" && connectPort != 0 && remoteIpEndPoint != null)
                            {
                                var e = new ChangedEventArgs(remoteIpEndPoint.Address.ToString(), connectPort);
                                OnChanged(e);
                            }
                            TraceOps.Out("Recived End");
                        }
                    }
                }

                TraceOps.Out("Close Stream and TCP Connection");

                if (client.Connected)
                {
                    stream.Close();
                    client.Close(); 
                }
            }
            server.Stop();
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
