using System.Windows.Forms;

namespace PaceCommon
{
    public interface IPlugin
    {
        IView GetView();
        IControl GetControl();
        IModel GetModel();
        string Test();
        void Start(string name);
        string Name();
        void SetQueue(ref MessageQueue messageQueue);
        void SetForm(Form mainPanel);
    }
}