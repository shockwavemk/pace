using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace PaceCommon
{
    [Serializable]
    public class Message : ISerializable
    {
        public Message(List<string> parameter, bool acknowledgement, string command, string destination)
        {
            Parameter = parameter;
            Acknowledgement = acknowledgement;
            Command = command;
            Destination = destination;
        }

        public Message()
        {
            Parameter = new List<string>();
            Acknowledgement = false;
            Command = "test command";
            Destination = "server";
        }

        private string Destination { get; set; }
        private string Command { get; set; }
        private List<string> Parameter { get; set; }
        private bool Acknowledgement { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
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
                strObject =
                   Encoding.ASCII.GetString(memStream.GetBuffer());
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
    }
}
