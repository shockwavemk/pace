using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaceServer
{
    class ClientConnection
    {
        public TcpClient TcpClient;
        private Thread ThreadConnection;
        private StreamReader ConnectionReceiver;
        private StreamWriter ConnectionSender;

        private bool _connectionEstablished;
        private string _clientResponse;

        public ClientConnection(TcpClient tcpConnection)
        {
            TcpClient = tcpConnection;
            ThreadConnection = new Thread(AcceptClient);
            ThreadConnection.Start();
        }

        private void AcceptClient()
        {
            #region Registration
            try
            {
                ConnectionReceiver = new StreamReader(TcpClient.GetStream());
                ConnectionSender = new StreamWriter(TcpClient.GetStream());

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
                while ((_clientResponse = ConnectionReceiver.ReadLine()) != "")
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
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            #endregion
        }


        private void HandleAdd()
        {
            int ClientId = 0; // TODO remove this
            _connectionEstablished = true;
            ClientInformation clientInformation = ClientInformation.GetClientInformation(ClientId); // first line for identification of client
            // Tell the Client: everything is fine
            ConnectionSender.WriteLine("1"); // TODO Add conversation in external function
            ConnectionSender.WriteLine("Welcome");
            ConnectionSender.Flush();
        }

        private void HandleResponse(string rawResponse)
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            ThreadConnection.Abort();
        }
    }
}
