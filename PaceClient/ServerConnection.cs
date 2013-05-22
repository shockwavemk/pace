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
                while ((_serverResponse = _connectionReceiver.ReadLine()) != "")
                {
                    if (_serverResponse == null)
                    {
                        CloseConnection();
                    }
                    else
                    {
                        HandleResponse(_serverResponse);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            #endregion
        }

        private void HandleResponse(string rawResponse)
        {
            TraceOps.Out(rawResponse);
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            _threadConnection.Abort();
        }
    }
}
