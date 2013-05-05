using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private ContextMenu tray_menu;

        public MainClientForm()
        {
            InitializeComponent();
            tray_menu = new ContextMenu();
            tray_menu.MenuItems.Add(0, new MenuItem("Show", new EventHandler(notifyIcon_DoubleClick)));
            tray_menu.MenuItems.Add(0, new MenuItem("Exit", new EventHandler(MainClientForm_Exit)));
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            Resize += new EventHandler(MainClientForm_Resize);
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            notifyIcon.ContextMenu = tray_menu;
            notifyIcon.MouseClick += new MouseEventHandler(notifyIcon_MouseUp);
        }

        private void MainClientForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
                
        }

        protected void MainClientForm_Exit(Object sender, EventArgs e)
        {
            Close();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }
    }
}
