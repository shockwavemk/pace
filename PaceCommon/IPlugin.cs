using System.Windows.Forms;

namespace PaceCommon
{
    public interface IPlugin
    {
        IView GetView();
        IControl GetControl();
        IModel GetModel();
        void Start(string name);
        void Test();
        string Name();
        void SetQueue(ref MessageQueue messageQueue);
        void SetForm(Form mainPanel);
        void SetTask(Message message);
    }
}
