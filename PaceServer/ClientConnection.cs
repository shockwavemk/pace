using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace PaceServer
{
    class ClientConnection
    {
        public TcpClient TcpClient;
        private Thread _threadConnection;
        private StreamReader _connectionReceiver;
        private StreamWriter _connectionSender;

        private bool _connectionEstablished;
        private string _clientResponse;

        public ClientConnection(TcpClient tcpConnection)
        {
            TcpClient = tcpConnection;
            _threadConnection = new Thread(AcceptClient);
            _threadConnection.Start();
        }

        private void AcceptClient()
        {
            #region Registration
            try
            {
                _connectionReceiver = new StreamReader(TcpClient.GetStream());
                _connectionSender = new StreamWriter(TcpClient.GetStream());

                // TODO Registration / Identification
                bool Register = false; // TODO remove this
                
                if (Register)
                {
                    CloseConnection();
                    return;
                }
                else
                {
                    HandleAdd();
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            #endregion

            #region Responses
            try
            {
                while ((_clientResponse = _connectionReceiver.ReadLine()) != "")
                {
                    if (_clientResponse == null)
                    {
                        CloseConnection();
                    }
                    else
                    {
                        HandleResponse(_clientResponse);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            #endregion
        }


        private void HandleAdd()
        {
            int ClientId = 0; // TODO remove this
            _connectionEstablished = true;
            ClientInformation clientInformation = ClientInformation.GetClientInformation(ClientId); // first line for identification of client
            // Tell the Client: everything is fine
            _connectionSender.WriteLine("1"); // TODO Add conversation in external function
            _connectionSender.WriteLine("Welcome");
            _connectionSender.Flush();
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
