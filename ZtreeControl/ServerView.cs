﻿using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using PaceCommon;

namespace ZtreeControl
{
    class ServerView : IServerView
    {
        private ToolStripMenuItem zTreeControlToolStripMenuItem;
        private ToolStripMenuItem startZTreeToolStripMenuItem;
        private ToolStripMenuItem openZLeafToolStripMenuItem;
        private ToolStripMenuItem closeZLeafToolStripMenuItem;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        
        public ArrayList MainServerFormMenuAddIns()
        {
            return null;
        }

        public ArrayList ClientsTableMenuAddIns()
        {
            return null;
        }
        
        public ToolStripMenuItem CreateMainMenu()
        {
            return null;
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            zTreeControlToolStripMenuItem = new ToolStripMenuItem();
            openZLeafToolStripMenuItem = new ToolStripMenuItem();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            closeZLeafToolStripMenuItem = new ToolStripMenuItem();
            
            zTreeControlToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            preferencesToolStripMenuItem,
            openZLeafToolStripMenuItem,
            closeZLeafToolStripMenuItem});
            zTreeControlToolStripMenuItem.Name = "zTreeControlToolStripMenuItem";
            zTreeControlToolStripMenuItem.Size = new Size(92, 20);
            zTreeControlToolStripMenuItem.Text = "z-TreeControl";

            openZLeafToolStripMenuItem.Name = "openZLeafToolStripMenuItem";
            openZLeafToolStripMenuItem.Size = new Size(152, 22);
            openZLeafToolStripMenuItem.Text = "Open z-Leaf";
            openZLeafToolStripMenuItem.Click += ServerControl.RemoteStartZLeaf;
            
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(152, 22);
            preferencesToolStripMenuItem.Text = "Preferences";
            preferencesToolStripMenuItem.Click += ServerControl.OpenPreferences;
            
            closeZLeafToolStripMenuItem.Name = "closeZLeafToolStripMenuItem";
            closeZLeafToolStripMenuItem.Size = new Size(152, 22);
            closeZLeafToolStripMenuItem.Text = "Close z-Leaf";
            closeZLeafToolStripMenuItem.Click += ServerControl.RemoteStopLeaf;

            return zTreeControlToolStripMenuItem;
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
            this.startZTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startZTreeToolStripMenuItem.Name = "startZTreeToolStripMenuItem";
            startZTreeToolStripMenuItem.Size = new Size(152, 22);
            startZTreeToolStripMenuItem.Text = "Start z-Tree";
            startZTreeToolStripMenuItem.Click += ServerControl.StartZTree;

            return startZTreeToolStripMenuItem;
        }

        public ToolStripMenuItem CreateMainMenuEntryView()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryHelp()
        {
            return null;
        }

        public string Test()
        {
            return "Test";
        }
    }
}