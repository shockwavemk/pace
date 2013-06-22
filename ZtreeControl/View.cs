using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using PaceCommon;

namespace ZtreeControl
{
    class View : IView
    {
        private ToolStripMenuItem _toolStripMenuItem;
        private ToolStripMenuItem _startZLeafToolStripMenuItem;
        private ToolStripMenuItem _stopZLeafToolStripMenuItem;
        
        public View()
        {}

        public ArrayList MainServerFormMenuAddIns()
        {
            return new ArrayList();
        }

        public ArrayList ClientsTableMenuAddIns()
        {
            return new ArrayList();
        }
        
        public ToolStripMenuItem CreateMainMenu()
        {
            return new ToolStripMenuItem("Ztree New");
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            _toolStripMenuItem = new ToolStripMenuItem();
            _startZLeafToolStripMenuItem = new ToolStripMenuItem();
            _stopZLeafToolStripMenuItem = new ToolStripMenuItem();

            _toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _startZLeafToolStripMenuItem, _stopZLeafToolStripMenuItem });
            _toolStripMenuItem.Name = "actionsToolStripMenuItem";
            _toolStripMenuItem.Size = new Size(47, 20);
            _toolStripMenuItem.Text = "zTree";

            _startZLeafToolStripMenuItem.Name = "startZLeafToolStripMenuItem";
            _startZLeafToolStripMenuItem.Size = new Size(152, 22);
            _startZLeafToolStripMenuItem.Text = "Start z-Leaf";
            _startZLeafToolStripMenuItem.Click += Control.StartZLeafToolStripMenuItemOnClick;
             
            _stopZLeafToolStripMenuItem.Name = "stopZLeafToolStripMenuItem";
            _stopZLeafToolStripMenuItem.Size = new Size(152, 22);
            _stopZLeafToolStripMenuItem.Text = "Stop z-Leaf";

            return _toolStripMenuItem;
        }

        public ToolStripMenuItem CreateMainMenuEntryFile()
        {
            var mi = new ToolStripMenuItem("Test0");
            return mi;
        }

        public ToolStripMenuItem CreateMainMenuEntryEdit()
        {
            return new ToolStripMenuItem("Test1");
        }

        public ToolStripMenuItem CreateMainMenuEntryRun()
        {
            var mi = new ToolStripMenuItem("Start z-Tree");
            mi.Click += Control.StartZTree;
            return mi;
        }

        public ToolStripMenuItem CreateMainMenuEntryView()
        {
            return new ToolStripMenuItem("Test3");
        }

        public ToolStripMenuItem CreateMainMenuEntryHelp()
        {
            return new ToolStripMenuItem("Test4");
        }

        public string Test()
        {
            return "View Test";
        }
    }
}