﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebControl
{
    public partial class ServerChangeUrlForm : Form
    {
        public ServerChangeUrlForm()
        {
            InitializeComponent();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ServerChangeUrlForm_Load(object sender, EventArgs e)
        {

        }
    }
}
