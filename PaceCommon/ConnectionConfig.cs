using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    [Serializable]
    public class ConnectionConfig : MarshalByRefObject
    {
        private static RemoteConfiguration _remoteConfigurationPublished;
        private static RemoteConfiguration _remoteConfigurationUsedForServerConnectionLater;

        public ConnectionConfig()
        {

        }

        public static ConnectionConfig GetRemote()
        {
            _remoteConfigurationPublished = new RemoteConfiguration();
            SetPublicService();
            return (ConnectionConfig)Activator.GetObject(typeof(ConnectionConfig), _remoteConfigurationPublished.GetServiceUrl());
        }

        public static void SetPublicService()
        {
            Services.PrepareSetService(_remoteConfigurationPublished.GetPort());
            Services.SetService(typeof(ConnectionConfig));
        }
    }

    [Serializable]
    public class RemoteConfiguration : MarshalByRefObject
    {
        private string _uri;
        private int _port;

        public RemoteConfiguration()
        {
            _uri = "localhost";
            _port = 9091;
        }

        public string GetServiceUrl()
        {
            var serviceUrl = "http://"+ GetUri() +":"+ GetPort() +"/ConnectionConfig.rem";
            return serviceUrl;
        }

        public int GetPort()
        {
            return _port;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public string GetUri()
        {
            return _uri;
        }

        public void SetUri(string uri)
        {
            _uri = uri;
        }
    }
}
