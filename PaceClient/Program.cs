using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;

namespace PaceClient
{
    static class Program
    {
        private static DllLoader _dllLoader;
        private static MainClientForm _mcf;
        private static IClientPlugin[] _plugins;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mcf = new MainClientForm();
            Application.Run(_mcf);
        }
    }
}
