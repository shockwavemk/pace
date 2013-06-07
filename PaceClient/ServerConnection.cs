using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
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
        public ConcurrentQueue<Message> OutQueue;
        private ConcurrentQueue<Message> _inQueue;

        public delegate void ConnectionRegistrationEventHandler(ServerConnection sender, ConnectionRegistrationEventArgs e);
        public event ConnectionRegistrationEventHandler ConnectionRegistration;

        public ServerConnection(TcpClient tcpConnection, ref ConcurrentQueue<Message> inQueue)
        {
            TraceOps.Out("New ServerConnection created");
            TcpClient = tcpConnection;
            OutQueue = new ConcurrentQueue<Message>();
            _inQueue = inQueue;

            _connectionReceiver = new StreamReader(TcpClient.GetStream());
            _connectionSender = new StreamWriter(TcpClient.GetStream());

            _threadConnection = new Thread(Communication);
            _threadConnection.Start(); 
        }

        public void Communication()
        {
            try
            {
                HandleAdd();

                while (_connectionEstablished)
                {
                    Thread.Sleep(500);
                    HandleResponse(_connectionReceiver.ReadLine());
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        private void HandleAdd()
        {
            _connectionEstablished = true;
            var rlist = new List<string> { HashOps.GetFqdn() };
            var m = new Message(rlist, true, "register", "");
            m.Send(_connectionSender);
        }

        private void HandleResponse(string s)
        {
            _buffer += s;
            if (s == "</SOAP-ENV:Envelope>")
            {
                try
                {
                    var m = new Message(_buffer);

                    // Directly catch registration - all other messages are added to queue for delegation
                    if (m.GetCommand() == "register")
                    {
                        ConnectionRegistration.Invoke(this, new ConnectionRegistrationEventArgs((string)m.Parameter.GetValue(0)));
                    }
                    else
                    {
                        _inQueue.Enqueue(m);
                    }

                    _buffer = "";
                }
                catch (Exception exception)
                {
                    // TODO: Secure Transmission of Objects
                    TraceOps.Out(exception.ToString());
                }
            }
        }

        public void CloseConnection()
        {
            TcpClient.Close();
            _threadConnection.Abort();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }
}
