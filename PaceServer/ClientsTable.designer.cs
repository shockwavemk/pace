namespace PaceServer
{
    partial class ClientsTable
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
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Local clients", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Client 1",
            "SubClient 1"}, 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsTable));
            this.clientListView = new System.Windows.Forms.ListView();
            this.clientListImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startZLeafToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopZLeafToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.externalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientListView
            // 
            this.clientListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            listViewGroup2.Header = "Local clients";
            listViewGroup2.Name = "standardListViewGroup";
            this.clientListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            listViewItem2.Group = listViewGroup2;
            this.clientListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.clientListView.LargeImageList = this.clientListImageList;
            this.clientListView.Location = new System.Drawing.Point(0, 27);
            this.clientListView.Name = "clientListView";
            this.clientListView.Size = new System.Drawing.Size(312, 377);
            this.clientListView.SmallImageList = this.clientListImageList;
            this.clientListView.TabIndex = 0;
            this.clientListView.UseCompatibleStateImageBehavior = false;
            this.clientListView.SelectedIndexChanged += new System.EventHandler(this.clientListView_SelectedIndexChanged);
            // 
            // clientListImageList
            // 
            this.clientListImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("clientListImageList.ImageStream")));
            this.clientListImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.clientListImageList.Images.SetKeyName(0, "iconmonstr-computer-4-icon.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem,
            this.externalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(312, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startZLeafToolStripMenuItem,
            this.stopZLeafToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.actionsToolStripMenuItem.Text = "zTree";
            // 
            // startZLeafToolStripMenuItem
            // 
            this.startZLeafToolStripMenuItem.Name = "startZLeafToolStripMenuItem";
            this.startZLeafToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startZLeafToolStripMenuItem.Text = "Start z-Leaf";
            // 
            // stopZLeafToolStripMenuItem
            // 
            this.stopZLeafToolStripMenuItem.Name = "stopZLeafToolStripMenuItem";
            this.stopZLeafToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopZLeafToolStripMenuItem.Text = "Stop z-Leaf";
            // 
            // externalToolStripMenuItem
            // 
            this.externalToolStripMenuItem.Name = "externalToolStripMenuItem";
            this.externalToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.externalToolStripMenuItem.Text = "External";
            // 
            // ClientsTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(312, 404);
            this.Controls.Add(this.clientListView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClientsTable";
            this.Text = "Client\'s Table";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ClientsTable_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView clientListView;
        private System.Windows.Forms.ImageList clientListImageList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startZLeafToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopZLeafToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem externalToolStripMenuItem;


    }
}