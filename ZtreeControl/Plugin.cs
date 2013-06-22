using System.Windows.Forms;
using PaceCommon;

namespace ZtreeControl
{
    class Plugin
    {
        private Control _control;
        private View _view;
        private Model _model;
        
        public Plugin()
        {
            _control = new Control();
            _view = new View();
            _model = new Model();
        }

        public View GetView()
        {
            return _view;
        }

        public Control GetControl()
        {
            return _control;
        }

        public Model GetModel()
        {
            return _model;
        }

        public string Test()
        {
            return "Plugin Test";
        }
    }
}
