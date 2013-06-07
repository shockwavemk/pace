using System;

namespace PaceCommon
{
    class ConnectionRegistration
    {
    }

    public class ConnectionRegistrationEventArgs : EventArgs
    {
        public string ConnectionHash { get; set; }

        public ConnectionRegistrationEventArgs(string eventMsg)
        {
            ConnectionHash = eventMsg;
        }
    }
}
