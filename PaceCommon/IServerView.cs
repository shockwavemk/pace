using System.Windows.Forms;

namespace PaceCommon
{
    public interface IServerView : IView
    {
        ToolStripMenuItem CreateMainMenu();
        ToolStripMenuItem CreateClientsTableMenu();
        ToolStripMenuItem CreateMainMenuEntryFile();
        ToolStripMenuItem CreateMainMenuEntryEdit();
        ToolStripMenuItem CreateMainMenuEntryRun();
        ToolStripMenuItem CreateMainMenuEntryView();
        ToolStripMenuItem CreateMainMenuEntryHelp();
    }
}
