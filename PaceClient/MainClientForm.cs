using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private ContextMenu tray_menu;
        private Thread _threadWorker;
        private bool _running = true;
        private ConcurrentQueue<PaceCommon.Message> _inQueue;
        private ConcurrentQueue<PaceCommon.Message> _outQueue;

        private delegate void UpdateStatusCallback(string strMessage);
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

            try
            {
                _inQueue = new ConcurrentQueue<PaceCommon.Message>();
                _outQueue = new ConcurrentQueue<PaceCommon.Message>();
                
                var tempClient = new NetworkClient();
                tempClient.SetIpAddress("127.0.0.1");
                tempClient.SetPort(1987);
                tempClient.SetInQueue(ref _inQueue);
                tempClient.SetOutQueue(ref _outQueue);
                NetworkClient.ServerChange += tempClient_ServerChange;
                tempClient.Start();

                _threadWorker = new Thread(Tasks);
                _threadWorker.Start();
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.Message);
            }
        }

        private void Tasks()
        {
            try
            {
                TraceOps.Out("Client: Start to work on Messages");
                while (_running)
                {
                    Thread.Sleep(500);
                    Message m;
                    var message = _inQueue.TryDequeue(out m);

                    if (message && m != null)
                    {
                        MessageBox.Show("Command: " + m.GetCommand() + " Destination: " + m.GetDestination(), "Message from Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        private void tempClient_ServerChange(object sender, ServerChangeEventArgs e)
        {
            Invoke(new UpdateStatusCallback(UpdateStatus), new object[] { e.EventMessage });
        }

        private void UpdateStatus(string strMessage)
        {
            TraceOps.Out(strMessage);
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

        private void UpdateGuiOnline()
        {
            status.Text = "Online"; 
            status.ForeColor = Color.DarkGreen;
        }

        private void UpdateGuiOffline()
        {
            status.Text = "Offline"; 
            status.ForeColor = Color.DarkRed;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "ping", "");
            _outQueue.Enqueue(m);
        }
    }
}
