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

namespace PaceServer
{
    public partial class MainServerForm : Form
    {
        public MainServerForm()
        {
            InitializeComponent();
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            // Create the server channel.
            HttpServerChannel serverChannel = new HttpServerChannel(9090);

            // Register the server channel.
            ChannelServices.RegisterChannel(serverChannel);

            // Expose an object for remote calls.
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteObject), "RemoteObject.rem",
                WellKnownObjectMode.Singleton);
        }
    }
}
