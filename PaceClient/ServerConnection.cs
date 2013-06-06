using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PaceClient;
using PaceCommon;

namespace PaceClient
{
    class ServerConnection
    {
        public TcpClient TcpClient;
        private Thread _threadConnection;
        private StreamReader _connectionReceiver;
        private StreamWriter _connectionSender;

        private bool _connectionEstablished = true;
        private string _buffer;

        public ServerConnection(TcpClient tcpConnection)
        {
            TraceOps.Out("New ServerConnection created");
            TcpClient = tcpConnection;
            _threadConnection = new Thread(Communication);
            _threadConnection.Start();
        }

        public void Communication()
        {
            try
            {
                _connectionReceiver = new StreamReader(TcpClient.GetStream());
                _connectionSender = new StreamWriter(TcpClient.GetStream());

                while (_connectionEstablished)
                {
                    HandleResponse(_connectionReceiver.ReadLine());
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        private void HandleResponse(string s)
        {
            if (s == "</SOAP-ENV:Envelope>")
            { 
                var m = new Message(_buffer);
                _buffer = "";
            }
            _buffer += s;
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            _threadConnection.Abort();
        }

        public void SendMessage(string message)
        {
            TraceOps.Out("Client send Message: " + message);
            _connectionSender.WriteLine(message);
            _connectionSender.Flush();
        }

        public string CalculateConnectionHash()
        {
            // try to use static information of system to (re-)identify a client - for example if ip changed because of reconnect or restart of system
            // TODO: make it more secure
            return HashOps.CreateStringHash(GetFqdn());
        }

        public static string GetFqdn()
        {
            string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = Dns.GetHostName();
            string fqdn;
            if (!hostName.Contains(domainName))
                fqdn = hostName + "." + domainName;
            else
                fqdn = hostName;

            return fqdn;
        }
    }
}
