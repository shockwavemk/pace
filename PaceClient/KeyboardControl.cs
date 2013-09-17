using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PaceCommon;
using Keys = System.Windows.Forms.Keys;
using Message = System.Windows.Forms.Message;

namespace PaceClient
{
    class KeyboardControl : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                try
                {
                    this.CreateHandle(new CreateParams());
                }
                catch (Exception ex)
                {
                    TraceOps.Out(System.Environment.NewLine + "Target  :  " + ex.TargetSite.ToString() + System.Environment.NewLine + "Message :  " + ex.Message.ToString() + System.Environment.NewLine + "Stack   :  " + ex.StackTrace.ToString());
                }
            }

            protected override void WndProc(ref Message m)
            {
                try
                {
                    base.WndProc(ref m);
                    if (m.Msg == WM_HOTKEY)
                    {
                        var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                        var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                        if (KeyPressed != null)
                            KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                    }
                }
                catch (Exception ex)
                {
                    TraceOps.Out(System.Environment.NewLine + "Target  :  " + ex.TargetSite.ToString() + System.Environment.NewLine + "Message :  " + ex.Message.ToString() + System.Environment.NewLine + "Stack   :  " + ex.StackTrace.ToString());
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            #region IDisposable Members

            public void Dispose()
            {
                this.DestroyHandle();
            }

            #endregion
        }

        private Window _window = new Window();
        private int _currentId;
        public bool Running;

        public KeyboardControl()
        {
            Running = true;
            try
            {
                _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
                {
                    if (KeyPressed != null)
                        KeyPressed(this, args);
                };
            }
            catch (Exception ex)
            {
                TraceOps.Out(System.Environment.NewLine + "Target  :  " + ex.TargetSite.ToString() + System.Environment.NewLine + "Message :  " + ex.Message.ToString() + System.Environment.NewLine + "Stack   :  " + ex.StackTrace.ToString());
            }
        }

        public void RegisterGlobalHotKey(ModifierKeys modifier, Keys key, Form mainForm)
        {
            try
            {
                _currentId = _currentId + 1;

                if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                    TraceOps.Out("Couldn’t register the hot key.");
            }
            catch (Exception ex)
            {
                TraceOps.Out(System.Environment.NewLine + "Target  :  " + ex.TargetSite.ToString() + System.Environment.NewLine + "Message :  " + ex.Message.ToString() + System.Environment.NewLine + "Stack   :  " + ex.StackTrace.ToString());
            }
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        public void Dispose()
        {
            Running = false;
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            _window.Dispose();
        }

        #endregion
    }

    public class KeyPressedEventArgs : EventArgs
    {
        private ModifierKeys _modifier;
        private Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        public Keys Key
        {
            get { return _key; }
        }
    }

    [Flags]
    public enum ModifierKeys : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}