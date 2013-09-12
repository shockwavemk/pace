using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaceCommon;

namespace ExternalControl
{
    class ClientModel : IClientModel
    {
        /// <summary>
        /// Windows restart
        /// </summary>
        public static string BuildCommandLineOptionsRestart()
        {
            return " -f -r -t 5";
        }

        /// <summary>
        /// Log off.
        /// </summary>
        public static string BuildCommandLineOptionsLogOff()
        {
            return " -l";
        }

        /// <summary>
        ///  Shutting Down Windows 
        /// </summary>
        public static string BuildCommandLineOptionsShutDown()
        {
            return " -f -s -t 5";
        }
    }
}
