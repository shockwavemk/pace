using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    [Serializable]
    public class Parameter
    {
        public string Name;
        public string Value;

        public Parameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
