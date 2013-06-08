using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Threading;
using PaceCommon;

namespace PaceClient
{
    class ServerConnection
    {
        private const int Threshold = 1;
        

        private Thread _threadInConnection, _threadOutConnection;

        private int _port = 1234;
        private bool _connectionEstablished = true;
        private ConcurrentQueue<Message> _inQueue;
        public ConcurrentQueue<Message> OutQueue;
        public MessageQueue MessageQueue;

        public delegate void ConnectionRegistrationEventHandler(ServerConnection sender, ConnectionRegistrationEventArgs e);
        public event ConnectionRegistrationEventHandler ConnectionRegistration;

        public ServerConnection(ref ConcurrentQueue<Message> inQueue, int port)
        {
            _port = port;
            var chnl = new HttpChannel(_port);
            ChannelServices.RegisterChannel(chnl, false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(MessageQueue),
                "MessageQueue.soap",
                WellKnownObjectMode.Singleton);

            var soap = "http://localhost:" + _port + "/MessageQueue.soap";
            MessageQueue = (MessageQueue)Activator.GetObject(typeof(MessageQueue), soap);

            _inQueue = inQueue;
            OutQueue = new ConcurrentQueue<Message>();

            _threadInConnection = new Thread(InCommunication);
            _threadInConnection.Start();

            _threadOutConnection = new Thread(OutCommunication);
            _threadOutConnection.Start(); 
        }

        public void InCommunication()
        {
            try
            {
                while (_connectionEstablished)
                {
                    Thread.Sleep(Threshold);
                    var m = MessageQueue.ServerToClientTryDequeue();
                    _inQueue.Enqueue(m);

                    if (m.GetCommand() == "register")
                    {
                        ConnectionRegistration.Invoke(this, new ConnectionRegistrationEventArgs((string)m.Parameter.GetValue(0)));
                    }
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void OutCommunication()
        {
            try
            {
                var rlist = new List<string> { HashOps.GetFqdn() };
                var registerMessage = new Message(rlist, true, "register", "");
                OutQueue.Enqueue(registerMessage);

                while (_connectionEstablished)
                {
                    Thread.Sleep(Threshold);
                    Message m;
                    OutQueue.TryDequeue(out m);
                    MessageQueue.ClientToServerEnqueue(m);

                    var destination = m.GetDestination();
                    var command = m.GetCommand();
                    TraceOps.Out("Inside ServerConnection - Message: " + command + " Destination: " + destination);
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void Stop()
        {
            _connectionEstablished = false;
        }
    }
}
