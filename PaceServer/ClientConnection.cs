using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using PaceCommon;

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
        public delegate void ClientChangeEventHandler(object sender, ClientChangeEventArgs e);
        public static event ClientChangeEventHandler ClientChange;

        public ClientConnection(TcpClient tcpConnection)
        {
            TraceOps.Out("New ClientConnection created");
            TcpClient = tcpConnection;
            _threadConnection = new Thread(Communication);
            _threadConnection.Start();
        }

        private void Communication()
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
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            #endregion

            #region Responses
            try
            {
                TraceOps.Out("Server waiting for Responses to Act");
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
            var random = new Random();
            int clientId = random.Next(0, 10000);
            _connectionEstablished = true;
            ClientInformation clientInformation = ClientInformation.GetClientInformation(clientId); // first line for identification of client
            // Tell the Client: everything is fine
            SendMessage("Hello client! You are now named client["+ clientId+"]");

            ClientChange.Invoke(null, new ClientChangeEventArgs("ClientAdded"));
        }

        private void HandleResponse(string rawResponse)
        {
            TraceOps.Out("Client answers:" + rawResponse);
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            _threadConnection.Abort();
        }

        public void SendMessage(string message)
        {
            TraceOps.Out("Server send Message:" + message);
            _connectionSender.WriteLine(message);
            _connectionSender.Flush();
        }
    }
}
