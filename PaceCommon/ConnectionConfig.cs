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
        public delegate void ChangedEventHandler(object sender, EventArgs e);

        public event ChangedEventHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public ConnectionConfig()
        {
            _remoteConfigurationUsedForServerConnectionLater = new RemoteConfiguration();
        }

        public static ConnectionConfig GetRemote()
        {
            _remoteConfigurationPublished = new RemoteConfiguration();

            Services.PrepareSetService(_remoteConfigurationPublished.GetPort());
            Services.SetService(typeof(ConnectionConfig));
            
            return (ConnectionConfig)Activator.GetObject(typeof(ConnectionConfig), _remoteConfigurationPublished.GetServiceUrl());
        }

        public void SetServer(string uri, int port)
        {
            _remoteConfigurationUsedForServerConnectionLater.SetUri(uri);
            _remoteConfigurationUsedForServerConnectionLater.SetPort(port);
            OnChanged(EventArgs.Empty);
        }

        public void SetServerUri(string s)
        {
            _remoteConfigurationUsedForServerConnectionLater.SetUri(s);
        }

        public void SetServerPort(int p)
        {
            _remoteConfigurationUsedForServerConnectionLater.SetPort(p);
        }

        public RemoteConfiguration GetRemoteConfig()
        {
            return _remoteConfigurationUsedForServerConnectionLater;
        }

        public string GetTest()
        {
            return "Hallo!";
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
