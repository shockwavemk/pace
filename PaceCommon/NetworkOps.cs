using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PaceCommon
{
    public class NetworkOps
    {
        public static string GetValue(string tag, string data)
        {
            var value = "";
            var elem = XElement.Parse(data);
            var xElement = elem.Element(tag);
            if (xElement != null)
            {
                value = xElement.Value;
            }
            return value;
        }

        public static string GetIpString(string uri)
        {
            string dns = "0.0.0.0";
            try
            {
                foreach (IPAddress ipAddress in Dns.GetHostEntry(uri).AddressList)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        dns = ipAddress.ToString();
                    }
                }

                TraceOps.Out("Parsed DNS/IP 4: " + dns);
            }
            catch (Exception)
            {
                TraceOps.Out("Parsed DNS/IP 4: not parsed");
            }
            
            return dns;
        }

        public static int GetPort(string text)
        {
            var port = 0;
            try { port = Convert.ToInt32(text); }
            catch (FormatException) { MessageBox.Show("Input string is not a sequence of digits."); }
            catch (OverflowException) { MessageBox.Show("The number cannot fit in an Int32."); }
            return port;
        }

        public static bool SetUpClientConnectionConfig(string toIp, int toPort, string fromIp, int fromPort)
        {
            try
            {
                TraceOps.Out("try to connect to " + toIp + " : " + toPort);
                var tcpClient = new TcpClient();
                try
                {
                    tcpClient.Connect(toIp, toPort);
                    TraceOps.Out("Connected with " + tcpClient.Client.RemoteEndPoint);
                }
                catch (Exception e)
                {
                    TraceOps.Out(e.ToString());
                }
                if (tcpClient.Connected)
                {
                    var networkStream = tcpClient.GetStream();
                    var streamWriter = new StreamWriter(networkStream);
                    TraceOps.Out("Send data to " + tcpClient.Client.RemoteEndPoint);
                    streamWriter.WriteLine("<XML>");
                    streamWriter.WriteLine("<IP>" + fromIp + "</IP>");
                    streamWriter.WriteLine("<PORT>" + fromPort + "</PORT>");
                    streamWriter.WriteLine("</XML>");
                    streamWriter.Flush();
                    tcpClient.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.ToString());
            }
            return false;
        }
    }
}
