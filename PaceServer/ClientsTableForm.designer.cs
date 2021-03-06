﻿namespace PaceServer
{
    partial class ClientsTableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Local clients", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Client 1, test", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsTableForm));
            this.clientListImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.handle = new System.Windows.Forms.PictureBox();
            this.clientListView = new System.Windows.Forms.ListView();
            this.CName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CCpu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CQueue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CApplications = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.handle)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientListImageList
            // 
            this.clientListImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.clientListImageList.ImageSize = new System.Drawing.Size(64, 64);
            this.clientListImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.handle);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.clientListView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(519, 239);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(519, 263);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // handle
            // 
            this.handle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.handle.Image = global::PaceServer.Properties.Resources.handle;
            this.handle.Location = new System.Drawing.Point(504, 224);
            this.handle.Name = "handle";
            this.handle.Size = new System.Drawing.Size(15, 15);
            this.handle.TabIndex = 2;
            this.handle.TabStop = false;
            // 
            // clientListView
            // 
            this.clientListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientListView.CheckBoxes = true;
            this.clientListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CName,
            this.CCpu,
            this.CQueue,
            this.CApplications});
            this.clientListView.FullRowSelect = true;
            this.clientListView.GridLines = true;
            listViewGroup1.Header = "Local clients";
            listViewGroup1.Name = "standardListViewGroup";
            this.clientListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.clientListView.HoverSelection = true;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.Tag = "test";
            listViewItem1.ToolTipText = "ToolTipText";
            this.clientListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.clientListView.LargeImageList = this.clientListImageList;
            this.clientListView.Location = new System.Drawing.Point(0, 0);
            this.clientListView.Name = "clientListView";
            this.clientListView.Size = new System.Drawing.Size(519, 218);
            this.clientListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.clientListView.TabIndex = 1;
            this.clientListView.UseCompatibleStateImageBehavior = false;
            this.clientListView.View = System.Windows.Forms.View.Details;
            this.clientListView.SelectedIndexChanged += new System.EventHandler(this.clientListView_SelectedIndexChanged_1);
            // 
            // CName
            // 
            this.CName.Text = "Name";
            this.CName.Width = 84;
            // 
            // CCpu
            // 
            this.CCpu.Text = "Cpu";
            // 
            // CQueue
            // 
            this.CQueue.Text = "Queue";
            // 
            // CApplications
            // 
            this.CApplications.Text = "Applications";
            this.CApplications.Width = 221;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(519, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConnectionToolStripMenuItem,
            this.saveConnectionsToolStripMenuItem,
            this.loadConnectionsToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // newConnectionToolStripMenuItem
            // 
            this.newConnectionToolStripMenuItem.Name = "newConnectionToolStripMenuItem";
            this.newConnectionToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.newConnectionToolStripMenuItem.Text = "New Connection";
            this.newConnectionToolStripMenuItem.Click += new System.EventHandler(this.newConnectionToolStripMenuItem_Click_1);
            // 
            // saveConnectionsToolStripMenuItem
            // 
            this.saveConnectionsToolStripMenuItem.Name = "saveConnectionsToolStripMenuItem";
            this.saveConnectionsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveConnectionsToolStripMenuItem.Text = "Save Connections";
            this.saveConnectionsToolStripMenuItem.Click += new System.EventHandler(this.saveConnectionsToolStripMenuItem_Click_1);
            // 
            // loadConnectionsToolStripMenuItem
            // 
            this.loadConnectionsToolStripMenuItem.Name = "loadConnectionsToolStripMenuItem";
            this.loadConnectionsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.loadConnectionsToolStripMenuItem.Text = "Load Connections";
            this.loadConnectionsToolStripMenuItem.Click += new System.EventHandler(this.loadConnectionsToolStripMenuItem_Click_1);
            // 
            // ClientsTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(519, 263);
            this.Controls.Add(this.toolStripContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1600, 1200);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ClientsTableForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Client\'s Table";
            this.Load += new System.EventHandler(this.ClientsTable_Load);
            this.Resize += new System.EventHandler(this.OnResize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.handle)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList clientListImageList;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ListView clientListView;
        private System.Windows.Forms.ColumnHeader CName;
        private System.Windows.Forms.ColumnHeader CCpu;
        private System.Windows.Forms.ColumnHeader CQueue;
        private System.Windows.Forms.ColumnHeader CApplications;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConnectionsToolStripMenuItem;
        public System.Windows.Forms.PictureBox handle;


    }
}