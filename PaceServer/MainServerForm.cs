using System;
using System.Windows.Forms;
using PaceCommon;

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
            Services.PrepareSetService(9090);
            Services.SetService(typeof(ConnectionTable));
            Services.SetService(typeof(MessageQueue));
        }
    }
}
