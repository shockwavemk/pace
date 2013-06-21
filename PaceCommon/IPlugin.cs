using System.Windows.Forms;

namespace PaceCommon
{
    public abstract class IPlugin
    {
        public Control Control;
        public View View;
    }

    public abstract class View
    {
        public abstract ToolStripMenuItem CreateMainMenu();
        public abstract ToolStripMenuItem CreateClientsTableMenu();
        public abstract ToolStripMenuItem CreateMainMenuEntryFile();
        public abstract ToolStripMenuItem CreateMainMenuEntryEdit();
        public abstract ToolStripMenuItem CreateMainMenuEntryRun();
        public abstract ToolStripMenuItem CreateMainMenuEntryView();
        public abstract ToolStripMenuItem CreateMainMenuEntryHelp();
    }

    public abstract class Control
    {
    }
}