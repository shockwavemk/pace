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

    public class ClientChangeEventArgs : EventArgs
    {
        public string EventMessage { get; set; }

        public ClientChangeEventArgs(string eventMsg)
        {
            EventMessage = eventMsg;
        }
    }
}
