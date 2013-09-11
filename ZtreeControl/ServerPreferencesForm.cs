using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZtreeControl
{
    public partial class ServerPreferencesForm : Form
    {
        public ServerPreferencesForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ServerPreferencesForm_Load(object sender, EventArgs e)
        {
            XPos.Value = ServerModel.X;
            YPos.Value = ServerModel.Y;
            Width.Value = ServerModel.W;
            Height.Value = ServerModel.H;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
