using System;
using PaceCommon;

namespace PaceCommon
{
    [Serializable]
    public class ConnectionTable : MarshalByRefObject
    {
        private string _serverUrl;
        private string _serverPort;

        public delegate void ConnectionRegistrationEventHandler(ClientInformation clientInformation);
        public event ConnectionRegistrationEventHandler ConnectionRegistration;

        public ConnectionTable()
        {
            _serverUrl = "localhost";
            _serverPort = "1234";
        }

        public string GetServerUrl()
        {
            return _serverUrl;
        }

        public void SetServerUrl(string serverurl)
        {
            _serverUrl = serverurl;
        }

        public string GetServerPort()
        {
            return _serverPort;
        }

        public void SetServerPort(string serverport)
        {
            _serverPort = serverport;
        }

        public ClientInformation GetNew(string name, string port, string url)
        {
            var ci = new ClientInformation(name, port, url);
            ConnectionRegistration.Invoke(ci);
            return ci;
        }

        public class ClientInformation
        {
            public string Name { get; set; }
            public string Port { get; set; }
            public string Url { get; set; }

            public ClientInformation(string name, string port, string url)
            {
                Name = name;
                Port = port;
                Url = url;
            }
        }
    }
}
