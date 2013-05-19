using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceServer
{
    class ClientChange
    {
    }

    public class ClientChangedEventArgs : EventArgs
    {
        public string EventMessage { get; set; }

        public ClientChangedEventArgs(string eventMsg)
        {
            EventMessage = eventMsg;
        }
    }
}
