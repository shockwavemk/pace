using System.Windows.Forms;

namespace PaceCommon
{
    public interface IPlugin
    {
        IView GetView();
        IControl GetControl();
        IModel GetModel();
        string Test();
    }

    public interface IControl
    {
        string Test();
    }

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

    public interface IModel
    {
        string Test();
    }
}