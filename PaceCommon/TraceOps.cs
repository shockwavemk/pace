using System;
using System.Diagnostics;

namespace PaceCommon
{
    public class TraceOps
    {
        public static void Out(string output)
        {
            Trace.WriteLine(output);
            Console.WriteLine(output);
        }
   }
}
