using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    public class RemoteObject : MarshalByRefObject
    {
        private int callCount = 0;

        public int GetCount()
        {
            Console.WriteLine("GetCount was called.");
            callCount++;
            return (callCount);
        }

    }
}
