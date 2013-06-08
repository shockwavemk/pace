using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private string _buffer;
        public ConcurrentQueue<Message> OutQueue;
        private ConcurrentQueue<Message> _inQueue;

        public delegate void ConnectionRegistrationEventHandler(ClientConnection sender, ConnectionRegistrationEventArgs e);
        public event ConnectionRegistrationEventHandler ConnectionRegistration;

        public ClientConnection(TcpClient tcpConnection, ref ConcurrentQueue<Message> inQueue)
        {
            TraceOps.Out("New ClientConnection created");
            TcpClient = tcpConnection;
            OutQueue = new ConcurrentQueue<Message>();
            _inQueue = inQueue;

            _connectionReceiver = new StreamReader(TcpClient.GetStream());
            _connectionSender = new StreamWriter(TcpClient.GetStream());

            _threadConnection = new Thread(Communication);
            _threadConnection.Start();
        }

        private void Communication()
        {
            try
            {
                HandleAdd();
            
                TraceOps.Out("Server waiting for Responses to Act");
                while (_connectionEstablished)
                {
                    Thread.Sleep(500);
                    TraceOps.Out("ClientConnectionThread");
                    _connectionReceiver.ReadLine();
                    _connectionSender.WriteLine("");
                    _connectionSender.Flush();
                    //HandleResponse(_connectionReceiver.ReadLine());
                    //HandleMessage();
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
            var rlist = new List<string> {HashOps.GetFqdn()};
            var m = new Message(rlist, true, "register", "");
            m.Send(_connectionSender);
        }

        private void HandleMessage()
        {
            try
            {
                TraceOps.Out("HandleMessage");
                Message m;
                var message = OutQueue.TryDequeue(out m);

                if (message && m != null)
                {
                    var destination = m.GetDestination();
                    var command = m.GetCommand();
                    TraceOps.Out("Inside ClientConnection - Message: " + command + " Destination: " + destination);
                    //m.Send(_connectionSender);
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        private void HandleResponse(string s)
        {
            TraceOps.Out("HandleResponse");
            _buffer += s;
            if (s == "</SOAP-ENV:Envelope>")
            {
                try
                {
                    var m = new Message(_buffer);
                    
                    // Directly catch registration - all other messages are added to queue for delegation
                    if (m.GetCommand() == "register")
                    {
                        //ConnectionRegistration.Invoke(this, new ConnectionRegistrationEventArgs((string) m.Parameter.GetValue(0)));
                    }
                    else
                    {
                        _inQueue.Enqueue(m);
                    }
                }
                catch (Exception exception)
                {
                    // TODO: Secure Transmission of Objects
                    TraceOps.Out(exception.ToString());
                }
                _buffer = "";
            }
        }

        public void CloseConnection()
        {
            _connectionEstablished = false;
            TcpClient.Close();
            _threadConnection.Abort();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }
}
