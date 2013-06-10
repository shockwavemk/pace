using System;
using System.Windows.Forms;
using PaceCommon;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private RemoteObject _service;
        private ConnectionTable _connectionTable;

        public MainClientForm()
        {
            InitializeComponent();
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            Services.PrepareGetService();
            Services.GetService("localhost", 9090, typeof(ConnectionTable));
            _service = new RemoteObject();
            _connectionTable = new ConnectionTable();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           Console.WriteLine("Port {0} .", _connectionTable.GetServerPort());
        }
    }
}
