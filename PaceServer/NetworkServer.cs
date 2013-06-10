using System;
using System.Collections;
using System.Linq;
using System.Threading;
using PaceCommon;

namespace PaceServer
{
    class NetworkServer
    {
        private const int Threshold = 1;
        private const int Waiting = 1000;
        public static Hashtable ClientList = new Hashtable();
        private string _url;
        private int _port;
        private bool _serverRunning = true;
        private Thread _threadMessages;

        public int GetPort()
        {
            return _port;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public string GetUrl()
        {
            return _url;
        }

        public void SetUrl(string url)
        {
            _url = url;
        }

        public NetworkServer()
        {
            _serverRunning = true;
            _threadMessages = new Thread(MessageWorker);
            _threadMessages.Start();
        }

        public void Stop()
        {
            _serverRunning = false;
            foreach (var cc in from DictionaryEntry item in ClientList select (ClientConnection)item.Value)
            {
                cc.Stop();
            }
        }

        private void MessageWorker()
        {
            
            try
            {
                TraceOps.Out("Try Service: http://" + _url + ":" + _port + "/ConnectionTable.xml");
                while (_serverRunning)
                {
                    /*
                    if (_serviceReady)
                    {
                        Thread.Sleep(Threshold);
                        Message m;
                        var message = _outQueue.TryDequeue(out m);

                        if (message && m != null)
                        {
                            var destination = m.GetDestination();
                            var cc = (ClientConnection) ClientList[destination];
                            if (cc != null)
                            {
                                cc.OutQueue.Enqueue(m);
                            }
                            else
                            {
                                _outQueue.Enqueue(m);
                            }
                        }
                    }
                    else
                    {
                        
                        
                        /*
                        if (_connectionTable != null)
                        {
                            _serviceReady = true;
                            _connectionTable.ConnectionRegistration += OnConnectionRegistration;
                            TraceOps.Out("Service Ready");
                        }
                        else
                        {
                            var soap = "http://" + _url + ":" + _port + "/ConnectionTable.soap";
                            _connectionTable = (ConnectionTable)Activator.GetObject(typeof(ConnectionTable), soap);
                        }
                    }
                     * */
                    Thread.Sleep(Waiting);
                }
    
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            
        }

        public static void OnConnectionRegistration(ConnectionTable.ClientInformation sender)
        {
            var newConnection = new ClientConnection(sender);
            ClientList.Add(sender.Name, newConnection);
        }
    }
}
