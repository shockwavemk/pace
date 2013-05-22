using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PaceClient
{
    class NetworkClient
    {
        private TcpClient _clientSocket;
        private IPAddress _ipAddress;
        private int _port;
        private bool _clientConnected = false;

        public int GetPort()
        {
            return _port;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public IPAddress GetIpAddress()
        {
            return _ipAddress;
        }

        public void SetIpAddress(string address)
        {
            _ipAddress = IPAddress.Parse(address);
        }

        public void Start()
        {
            try
            {
                _clientSocket = new TcpClient();
                _clientSocket.Connect(GetIpAddress(), GetPort());

                FlushStream();
                UpdateGuiOnline(); // TODO remove Gui related from class
            }
            catch
            {
                UpdateGuiOffline(); // TODO remove Gui related from class
            }
        }

        public void Stop()
        {
            
        }

        //TODO: refactor this functions

        private void FlushStream()
        {
            var stream = _clientSocket.GetStream();
            var sw = new StreamWriter(stream);
            sw.Flush();
        }


        private static void UpdateGuiOnline()
        {
            throw new NotImplementedException();
            /*
             *  status.Text = "Online"; 
             *  status.ForeColor = Color.DarkGreen;
             *  button1.Image = button1.ImageList.Images[0];
             *  button1.ImageAlign = ContentAlignment.MiddleRight;
             */
        }

        private static void UpdateGuiOffline()
        {
            throw new NotImplementedException();
            /*
             *  status.Text = "Offline"; 
             *  status.ForeColor = Color.DarkRed;
             *  button1.Image = button1.ImageList.Images[1];
             *  button1.ImageAlign = ContentAlignment.MiddleRight;
             */
        }
    }
}
