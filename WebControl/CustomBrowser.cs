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
    public partial class CustomBrowser : Form
    {
        public CustomBrowser()
        {
            InitializeComponent();
        }

        public void BrowseTo(string url)
        {
            webBrowser.Navigate(url);
        }
    }
}
