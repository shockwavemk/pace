using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace PaceCommon
{
    public class Services
    {
        public static void PrepareSetService(int port)
        {
            var serverChannel = new HttpServerChannel(port);
            ChannelServices.RegisterChannel(serverChannel, false);
        }

        public static void SetService(Type type)
        {
            RemotingConfiguration.RegisterWellKnownServiceType(type, type.Name + ".rem", WellKnownObjectMode.Singleton);
        }

        public static void PrepareGetService()
        {
            var clientChannel = new HttpClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);

            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
        }

        public static void GetService(string server, int port, Type type)
        {
            try
            {
                var url = "http://" + server + ":" + port + "/" + type.Name + ".rem";
                TraceOps.Out("Try to get service : " + url);
                var remoteType = new WellKnownClientTypeEntry(type, url);
                remoteType.ApplicationUrl = url;
                TraceOps.Out(remoteType.ObjectUrl);
                RemotingConfiguration.RegisterWellKnownClientType(remoteType);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            
        }
    }
}
