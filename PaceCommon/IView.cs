using System.Windows.Forms;

namespace PaceCommon
{
    public interface IView
    {
        string Test();
        ToolStripMenuItem CreateMainMenu();
        ToolStripMenuItem CreateClientsTableMenu();
        ToolStripMenuItem CreateMainMenuEntryFile();
        ToolStripMenuItem CreateMainMenuEntryEdit();
        ToolStripMenuItem CreateMainMenuEntryRun();
        ToolStripMenuItem CreateMainMenuEntryView();
        ToolStripMenuItem CreateMainMenuEntryHelp();
    }
}