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
        private string[,] Parameter { get; set; }
        private bool Acknowledgement { get; set; }
        

        public Message(string[,] parameter, bool acknowledgement, string command, string destination)
        {
            Acknowledgement = acknowledgement;
            Command = command;
            Destination = destination;
            Parameter = parameter;
        }

        public Message()
        {
            Parameter = null;
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

        public string[,] GetParameter()
        {
            return Parameter;
        }

        public static string GetAttribute(string[,] strings, string needle)
        {
            try
            {
                for (int i = 0; i < strings.GetLength(0); i++)
                {
                    var key = strings[i, 0];
                    var value = strings[i, 1];
                    if (key.Equals(needle))
                    {
                        return value;
                    }
                }
            }
            catch (Exception e)
            {
                TraceOps.Out(e.ToString());
            }
            
            return "";
        }
    }
}
