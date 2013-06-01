using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    class ServerChange
    {

    }

    public class ServerChangeEventArgs : EventArgs
    {
        public string EventMessage { get; set; }

        public ServerChangeEventArgs(string eventMsg)
        {
            EventMessage = eventMsg;
        }
    }
}
