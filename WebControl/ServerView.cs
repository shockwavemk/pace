using System.Windows.Forms;
using PaceCommon;

namespace WebControl
{
    class ServerView : IServerView
    {
        public ToolStripMenuItem CreateMainMenu()
        {
            return new ToolStripMenuItem();
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryFile()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryEdit()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryRun()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryView()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryHelp()
        {
            return null;
        }
    }
}
