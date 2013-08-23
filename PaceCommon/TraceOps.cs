using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace PaceCommon
{
    public class TraceOps
    {
        private static ArrayList _log = new ArrayList();

        public static void Out(string output)
        {
            var e = new LogEvent(1, output);
            _log.Add(e);
            Console.WriteLine(output);
        }

        public static string GetLog()
        {
            return _log.Cast<LogEvent>().Aggregate("", (current, logEvent) => current + logEvent.ToString());
        }
    }

    public class LogEvent
    {
        private DateTime _dateTime;
        private int _type;
        private string _log;

        public LogEvent(int type, string log)
        {
            _dateTime = DateTime.Now;
            _type = type;
            _log = log;
        }

        public override string ToString()
        {
            return _dateTime+ "("+ _type +"): "+ _log +"; "+ System.Environment.NewLine;
        }
    }
}
