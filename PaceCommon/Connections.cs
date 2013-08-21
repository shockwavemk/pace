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

		[XmlArray ("ClientInformations"), XmlArrayItem("ClientInformation", typeof(ConnectionTable.ClientInformation))]
		public System.Collections.ArrayList EmailAddresses = new System.Collections.ArrayList();
	}
}
