using System;
using System.Windows.Forms;
using PaceCommon;

namespace ExternalControl
{
    class ServerView : IServerView
    {
        private ToolStripMenuItem startProcessToolStripMenuItem;
        private ToolStripMenuItem stopProcessToolStripMenuItem;
        private ToolStripMenuItem showClientDetailsToolStripMenuItem;
        private ToolStripMenuItem externalProcessToolStripMenuItem;
        
        public ToolStripMenuItem CreateMainMenu()
        {
            return null;
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            externalProcessToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            startProcessToolStripMenuItem,
            stopProcessToolStripMenuItem,
            showClientDetailsToolStripMenuItem});
            externalProcessToolStripMenuItem.Name = "externalProcessToolStripMenuItem";
            externalProcessToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            externalProcessToolStripMenuItem.Text = "ExternalControl";

            startProcessToolStripMenuItem.Name = "startProcessToolStripMenuItem";
            startProcessToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            startProcessToolStripMenuItem.Text = "Start Process";
            
            stopProcessToolStripMenuItem.Name = "stopProcessToolStripMenuItem";
            stopProcessToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            stopProcessToolStripMenuItem.Text = "Stop Process";
             
            showClientDetailsToolStripMenuItem.Name = "showClientDetailsToolStripMenuItem";
            showClientDetailsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            showClientDetailsToolStripMenuItem.Text = "Show Client Details";
            
            throw new NotImplementedException();
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
