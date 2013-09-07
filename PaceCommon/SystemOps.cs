using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    public class SystemOps
    {
        public static void SetAutoStart()
        {
            try
            {
                var clickOnceHelper = new ClickOnceHelper(Globals.PublisherName, Globals.ProductName);
                clickOnceHelper.UpdateUninstallParameters();
                clickOnceHelper.AddShortcutToStartup();
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.ToString());
            }
        }
    }
}
