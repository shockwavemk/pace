using System;
using PaceCommon;

namespace PaceCommon
{
    [Serializable]
    public class ConnectionTable : MarshalByRefObject
    {
        private string ServerUrl { get; set; }
        public delegate void ConnectionRegistrationEventHandler(ClientConnection sender, ConnectionRegistrationEventArgs e);
        public event ConnectionRegistrationEventHandler ConnectionRegistration;

        public ConnectionTable()
        {
            ServerUrl = "127.0.0.1";
        }

        public string GetServerUrl()
        {
            return ServerUrl;
        }

        public ClientInformation GetClient()
        {
            return ClientInformation.GetClientInformation(0);
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

            public static ClientInformation GetClientInformation(int clientId)
            {
                return new ClientInformation("TestClient" + clientId, "1234", "localhost");
            }
        }
    }
}
