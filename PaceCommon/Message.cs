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
        private string Source { get; set; }
        private string Destination { get; set; }
        private string Command { get; set; }
        private Array _parameter;
        private bool Acknowledgement { get; set; }

        public Array Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        public Message(List<Parameter> parameter, bool acknowledgement, string command, string destination)
        {
            Parameter = parameter.ToArray();
            Acknowledgement = acknowledgement;
            Command = command;
            Destination = destination;
        }

        public Message(bool acknowledgement, string command, string destination)
        {
            var p = new List<Parameter> { };
            Parameter = p.ToArray();
            Acknowledgement = acknowledgement;
            Command = command;
            Destination = destination;
        }

        public Message()
        {
            var p = new List<Parameter> { new Parameter ("parameter", "value") };
            _parameter = p.ToArray();
            Acknowledgement = false;
            Command = "ping";
            Destination = "Server";
            Source = "Server";
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
