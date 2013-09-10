using System;
using System.Windows.Forms;
using PaceCommon;

namespace WebControl
{
    class ServerView : IServerView
    {
        private ToolStripMenuItem webControlToolStripMenuItem;
        private ToolStripMenuItem webbrowserPreferencesToolStripMenuItem;
        private ToolStripMenuItem openWebbrowserToolStripMenuItem;
        private ToolStripMenuItem openCustomWebbrowserToolStripMenuItem;
        private ToolStripMenuItem closeWebBrowserToolStripMenuItem;
        private ToolStripMenuItem changeUrlToolStripMenuItem;

        public ToolStripMenuItem CreateMainMenu()
        {
            return null;
        }

        public ToolStripMenuItem CreateClientsTableMenu()
        {
            

            webControlToolStripMenuItem = new ToolStripMenuItem();
            openWebbrowserToolStripMenuItem = new ToolStripMenuItem();
            openCustomWebbrowserToolStripMenuItem = new ToolStripMenuItem();
            closeWebBrowserToolStripMenuItem = new ToolStripMenuItem();
            changeUrlToolStripMenuItem = new ToolStripMenuItem();
            webbrowserPreferencesToolStripMenuItem = new ToolStripMenuItem();

            webControlToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            webbrowserPreferencesToolStripMenuItem,
            openWebbrowserToolStripMenuItem,
            openCustomWebbrowserToolStripMenuItem,
            closeWebBrowserToolStripMenuItem,
            changeUrlToolStripMenuItem});
            webControlToolStripMenuItem.Name = "webControlToolStripMenuItem";
            webControlToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            webControlToolStripMenuItem.Text = "WebControl";
            
             
            openWebbrowserToolStripMenuItem.Name = "openWebbrowserToolStripMenuItem";
            openWebbrowserToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            openWebbrowserToolStripMenuItem.Text = "Open Webbrowser";
            openWebbrowserToolStripMenuItem.Click += ServerControl.OpenBrowser;

            openCustomWebbrowserToolStripMenuItem.Name = "openCustomWebbrowserToolStripMenuItem";
            openCustomWebbrowserToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            openCustomWebbrowserToolStripMenuItem.Text = "Open Custom Webbrowser";
            openCustomWebbrowserToolStripMenuItem.Click += ServerControl.OpenCustomBrowser;
             
            closeWebBrowserToolStripMenuItem.Name = "closeWebBrowserToolStripMenuItem";
            closeWebBrowserToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            closeWebBrowserToolStripMenuItem.Text = "Close Webbrowser";
            closeWebBrowserToolStripMenuItem.Click += ServerControl.CloseBrowser;
            
            changeUrlToolStripMenuItem.Name = "changeUrlToolStripMenuItem";
            changeUrlToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            changeUrlToolStripMenuItem.Text = "Change Url";
            changeUrlToolStripMenuItem.Click += ServerControl.ChangeUrl;
             
            webbrowserPreferencesToolStripMenuItem.Name = "webbrowserPreferencesToolStripMenuItem";
            webbrowserPreferencesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            webbrowserPreferencesToolStripMenuItem.Text = "Preferences";
            webbrowserPreferencesToolStripMenuItem.Click += ServerControl.OpenPreferences;
            
            return webControlToolStripMenuItem;
        }

        public ToolStripMenuItem CreateMainMenuEntryFile()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryEdit()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryRun()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryView()
        {
            return null;
        }

        public ToolStripMenuItem CreateMainMenuEntryHelp()
        {
            return null;
        }
    }
}
