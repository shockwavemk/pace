using System;
using System.Xml.Serialization;

namespace PaceCommon
{
    [XmlRootAttribute("Connections", Namespace="", IsNullable=false)]
	public class Connections
	{
		public Connections()
		{
		}

		[XmlAttributeAttribute(DataType="date")]
		public System.DateTime DateTimeValue;

		[XmlArray ("ConnectionList"), XmlArrayItem("Connection", typeof(Connection))]
        public System.Collections.ArrayList ConnectionList = new System.Collections.ArrayList();
	}
}
