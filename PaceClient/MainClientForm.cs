using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceServer;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        public MainClientForm()
        {
            InitializeComponent();
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            // Create the channel.
            HttpClientChannel clientChannel = new HttpClientChannel();

            // Register the channel.
            ChannelServices.RegisterChannel(clientChannel);

            // Register as client for remote object.
            WellKnownClientTypeEntry remoteType =
                new WellKnownClientTypeEntry(typeof(RemoteObject),
                "http://localhost:9090/RemoteObject.rem");
            RemotingConfiguration.RegisterWellKnownClientType(remoteType);

            // Create a message sink.
            string objectUri;
            System.Runtime.Remoting.Messaging.IMessageSink messageSink =
                clientChannel.CreateMessageSink(
                "http://localhost:9090/RemoteObject.rem",
                null, out objectUri);
            Console.WriteLine(
                "The URI of the message sink is {0}.",
                objectUri);
            if (messageSink != null)
            {
                Console.WriteLine("The type of the message sink is {0}.",
                    messageSink.GetType().ToString());
            }

            // Display the channel's properties using Keys and Item.
            foreach (string key in clientChannel.Keys)
            {
                Console.WriteLine(
                    "clientChannel[{0}] = <{1}>",
                    key, clientChannel[key]);
            }

            // Parse the channel's URI.
            string objectUrl = "http://localhost:9090/RemoteObject.rem";
            string channelUri = clientChannel.Parse(objectUrl, out objectUri);
            Console.WriteLine("The object URL is {0}.", objectUrl);
            Console.WriteLine("The object URI is {0}.", objectUri);
            Console.WriteLine("The channel URI is {0}.", channelUri);

            // Create an instance of the remote object.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoteObject service = new RemoteObject();

            // Invoke a method on the remote object.
            Console.WriteLine("The client is invoking the remote object.");
            Console.WriteLine("The remote object has been called {0} times.",
                service.GetCount());

            Console.WriteLine("Press ENTER to exit the server.");
            Console.ReadLine();
        }
    }
}
