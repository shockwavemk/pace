using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;

namespace ExternalControl
{
    public partial class ServerClientDetailForm : Form
    {
        private ConnectionTable.ClientInformation _information;

        public ServerClientDetailForm(ConnectionTable.ClientInformation information)
        {
            InitializeComponent();
            _information = information;
        }

        private void ServerClientDetailForm_Load(object sender, EventArgs e)
        {
            labelName.Text = _information.GetName();
        }
    }
}
