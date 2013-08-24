﻿using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PaceCommon
{
    public class TraceOps
    {
        private static ArrayList _log = new ArrayList();
        private static Log _logForm;

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

        public static void LoadLog()
        {
            var mainForm = Application.OpenForms[0];
            var location = mainForm.Location;
            location.X += mainForm.Width;

            _logForm = new Log();
            _logForm.TopLevel = true;
            _logForm.Location = location;
            _logForm.Show();
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
            return _dateTime+ "("+ _type +"): "+ _log +"; "+ Environment.NewLine;
        }
    }
}
