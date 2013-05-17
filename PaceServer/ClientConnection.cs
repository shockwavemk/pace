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
        private StreamReader ClientReceiver;
        private StreamWriter ClientSender;

        private bool _connectionEstablished;

        public ClientConnection(TcpClient tcpConnection)
        {
            TcpClient = tcpConnection;
            ThreadConnection = new Thread(AcceptClient);
            ThreadConnection.Start();
        }

        public void AcceptClient()
        {
            #region Registration
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
                    // Tell the Client: everything is fine
                    ClientSender.WriteLine("1"); // TODO Add conversation in external function
                    ClientSender.WriteLine("Welcome");
                    ClientSender.Flush();
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

            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
            #endregion
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            ThreadConnection.Abort();
        }
    }
}
