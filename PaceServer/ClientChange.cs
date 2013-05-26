using System;

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
