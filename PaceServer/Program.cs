using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;

namespace PaceServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the server channel.
            HttpServerChannel serverChannel = new HttpServerChannel(9090);

            // Register the server channel.
            ChannelServices.RegisterChannel(serverChannel);

            // Expose an object for remote calls.
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteObject), "RemoteObject.rem",
                WellKnownObjectMode.Singleton);

            // Wait for the user prompt.
            Console.WriteLine("Press ENTER to exit the server.");
            Console.ReadLine();
            Console.WriteLine("The server is exiting.");
        }
    }
}
