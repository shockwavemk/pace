using System;
using System.Windows.Forms;
using PaceCommon;

namespace WebControl
{
    class ServerView : IServerView
    {
        private ToolStripMenuItem WebControlMenuItem;

        public ToolStripMenuItem CreateMainMenu()
        {
            return null;
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            var startWebControlMenuItem = new ToolStripMenuItem("Start WebControl");
            startWebControlMenuItem.Click += ServerControl.StartWebControlToolStripMenuItemOnClick;

            WebControlMenuItem = new ToolStripMenuItem("WebControl");
            
            WebControlMenuItem.DropDownItems.AddRange(new ToolStripItem[] 
            {
                startWebControlMenuItem
            });
            
            return WebControlMenuItem;
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
