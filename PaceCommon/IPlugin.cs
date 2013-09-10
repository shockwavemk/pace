using System;
using System.Windows.Forms;

namespace PaceCommon
{
    public interface IPlugin
    {
        IView GetView();
        IControl GetControl();
        IModel GetModel();
        void Start();
        void Test();
        string Name();
        void SetName(ref string name);
        void SetQueue(ref MessageQueue messageQueue);
        void SetTable(ref ConnectionTable connectionTable);
        void SetForm(Form mainForm);
        void SetPanel(Panel mainPanel);
        void SetTask(Message message);
        EventHandler SetEventHandler(object sender, EventArgs args);
    }
}
