using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace PaceClient
{
    class ServerConnection
    {
        public TcpClient TcpClient;
        private Thread _threadConnection;
        private StreamReader _connectionReceiver;
        private StreamWriter _connectionSender;

        private bool _connectionEstablished;
        private string _serverResponse;

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
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }

            #region Responses
            try
            {
                TraceOps.Out("Client waiting for Responses to Act");
                while ((_serverResponse = _connectionReceiver.ReadLine()) != "")
                {
                    if (_serverResponse == null)
                    {
                        TraceOps.Out("Verbindung geschlossen");
                        CloseConnection();
                    }
                    else
                    {
                        HandleResponse(_serverResponse);
                    }
                }
                TraceOps.Out("Client: End of server responses");
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            #endregion
        }

        private void HandleResponse(string rawResponse)
        {
            TraceOps.Out("Server answers:"+rawResponse);
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
    }
}
