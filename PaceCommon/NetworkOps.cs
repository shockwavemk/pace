using System;
using System.Collections.Generic;
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
            foreach (IPAddress ipAddress in Dns.GetHostEntry(uri).AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    dns = ipAddress.ToString();
                }
            }

            TraceOps.Out("Parsed DNS/IP 4: " + dns);
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
    }
}
