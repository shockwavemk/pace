using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

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

        public Message(string soap)
        {
            var m = (Message) SoapTo(soap);
            _parameter = m.Parameter;
            Acknowledgement = m.Acknowledgement;
            Command = m.Command;
            Destination = m.Destination;
        }

        public Message()
        {
            _parameter = new string[1];
            Acknowledgement = false;
            Command = "test command";
            Destination = "server";
        }

        public string ToSoap()
        {
            return ToSoap(this);
        }
        
        public static string ToSoap(Object objToSoap)
        {
            MemoryStream memStream = null;
            string strObject;
            try
            {
                memStream = new MemoryStream();
                IFormatter formatter = new SoapFormatter();
                formatter.Serialize(memStream, objToSoap);
                strObject = Encoding.ASCII.GetString(memStream.GetBuffer());
                var index = strObject.IndexOf("\0", StringComparison.Ordinal);
                if (index > 0)
                {
                    strObject = strObject.Substring(0, index);
                }
            }
            finally
            {
                if (memStream != null) memStream.Close();
            }
            return strObject;
        }

        public static object SoapTo(string soapString)
        {
            MemoryStream memStream = null;
            Object objectFromSoap;
            try
            {
                var bytes = new byte[soapString.Length];

                Encoding.ASCII.GetBytes(soapString, 0, soapString.Length, bytes, 0);
                memStream = new MemoryStream(bytes);
                IFormatter formatter = new SoapFormatter();
                objectFromSoap =
                     formatter.Deserialize(memStream);
            }
            finally
            {
                if (memStream != null) memStream.Close();
            }
            return objectFromSoap;
        }

        public void Send(StreamWriter streamWriter)
        {
            IFormatter formatter = new SoapFormatter();
            formatter.Serialize(streamWriter.BaseStream, this);
            streamWriter.Flush();
        }
    }
}
