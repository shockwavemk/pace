using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private void MainClientForm_Resize(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, C# World!", "Howdy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
    }
}
