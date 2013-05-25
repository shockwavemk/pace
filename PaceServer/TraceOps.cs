using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceServer
{
    class TraceOps
    {
        public static void Out(string output)
        {
            Trace.WriteLine(output);
            Console.WriteLine(output);
        }
   }
}
