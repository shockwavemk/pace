using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    class ConnectionChange
    {
    }

    public class ConnectionChangeEventArgs : EventArgs
    {
        public string EventMessage { get; set; }

        public ConnectionChangeEventArgs(string eventMsg)
        {
            EventMessage = eventMsg;
        }
    }
}
