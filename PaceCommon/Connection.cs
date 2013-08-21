using System;
using System.Xml.Serialization;

namespace PaceCommon
{
    [Serializable]
	public class Connection
	{
        public Connection()
		{
		}

        public string name = "";
        public string ip = "";
        public int port = 0;
	}
}


