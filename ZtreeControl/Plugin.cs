namespace ZtreeControl
{
    class Plugin : PaceCommon.Plugin
    {
        public new Control Control;
        public new View View;
        
        public Plugin()
        {
            Control = new Control();
            View = new View();
        }

        public string Test2()
        {
            return "innerTest";
        }
    }
}
