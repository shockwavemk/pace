using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
                var tempServer = new NetworkServer();
                NetworkServer.StatusChanged += new StatusChangedEventHandler(TempServer_StatusChanged);
                tempServer.StartListening();
            }
            catch (Exception ex)
            {
                txtLog.AppendText(ex.Message + "\r\n");
            }
        }
    }
}
