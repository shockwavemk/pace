using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace ZtreeControl
{
    class View
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

        public ToolStripMenuItem CreateMenu()
        {
            _toolStripMenuItem = new ToolStripMenuItem();
            _startZLeafToolStripMenuItem = new ToolStripMenuItem();
            _stopZLeafToolStripMenuItem = new ToolStripMenuItem();
            
            _toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {_startZLeafToolStripMenuItem,_stopZLeafToolStripMenuItem});
            _toolStripMenuItem.Name = "actionsToolStripMenuItem";
            _toolStripMenuItem.Size = new Size(47, 20);
            _toolStripMenuItem.Text = "zTree";
            
            _startZLeafToolStripMenuItem.Name = "startZLeafToolStripMenuItem";
            _startZLeafToolStripMenuItem.Size = new Size(152, 22);
            _startZLeafToolStripMenuItem.Text = "Start z-Leaf";
           
            _stopZLeafToolStripMenuItem.Name = "stopZLeafToolStripMenuItem";
            _stopZLeafToolStripMenuItem.Size = new Size(152, 22);
            _stopZLeafToolStripMenuItem.Text = "Stop z-Leaf";

            return _toolStripMenuItem;
        }
    }
}