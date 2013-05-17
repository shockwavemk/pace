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
        private bool _connectionEstablished;

        public ClientConnection(TcpClient tcpConnection)
        {
            TcpClient = tcpConnection;
            ThreadConnection = new Thread(AcceptClient);
            ThreadConnection.Start();
        }

        public void AcceptClient()
        {
            try
            {
                ClientReceiver = new StreamReader(TcpClient.GetStream());
                ClientSender = new StreamWriter(TcpClient.GetStream());

                // TODO Registration / Identification
                bool Register = false; // TODO
                int ClientId = 0; // TODO

                if (Register)
                {
                    CloseConnection();
                    return;
                }
                else
                {
                    _connectionEstablished = true;
                    ClientInformation clientInformation = ClientInformation.GetClientInformation(ClientId);

                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            ThreadConnection.Abort();
        }
    }
}
