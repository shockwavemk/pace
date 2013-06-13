using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private NetworkClient _networkClient;
        private bool _running = true;
        private string _name;
        private Thread _threadWorker;

        public MainClientForm()
        {
            InitializeComponent();
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            Services.PrepareGetService();
            Services.GetService("localhost", 9090, typeof(ConnectionTable));
            Services.GetService("localhost", 9090, typeof(MessageQueue));
            _connectionTable = new ConnectionTable();
            _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");
            _name = HashOps.GetFqdn();

            try
            {
                //_networkClient = new NetworkClient(ref _messageQueue, _name);
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
                while (_running)
                {
                    Thread.Sleep(1000);
                    /*
                    var m = _messageQueue.ServerToClientTryDequeue(_messageQueue.Get(_name));

                    if (m != null)
                    {
                        MessageBox.Show("Command: " + m.GetCommand() + " Destination: " + m.GetDestination(), "Message from Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    */
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "ping", "");

            var gm = _messageQueue.GetMessage("server");

            MessageBox.Show(gm.GetCommand());
            //_messageQueue.ClientToServerEnqueue(m, _messageQueue.getServer());
        }
    }
}
