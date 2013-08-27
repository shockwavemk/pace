using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    [Serializable]
    public class Message
    {
        private string Destination { get; set; }
        private string Command { get; set; }
        private Array _parameter;
        private bool Acknowledgement { get; set; }

        public Array Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        public Message(List<string> parameter, bool acknowledgement, string command, string destination)
        {
            Parameter = parameter.ToArray();
            Acknowledgement = acknowledgement;
            Command = command;
            Destination = destination;
        }

        public Message()
        {
            _parameter = new string[1];
            _parameter.SetValue("testparameter", 0);
            Acknowledgement = false;
            Command = "ping";
            Destination = "Server";
        }

        public string GetDestination()
        {
            return Destination;
        }

        public string GetCommand()
        {
            return Command;
        }
    }
}
