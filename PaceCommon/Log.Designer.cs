namespace PaceCommon
{
    partial class Log
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
            this.LogFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LogFile
            // 
            this.LogFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogFile.Location = new System.Drawing.Point(0, 0);
            this.LogFile.Multiline = true;
            this.LogFile.Name = "LogFile";
            this.LogFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogFile.Size = new System.Drawing.Size(425, 435);
            this.LogFile.TabIndex = 0;
            this.LogFile.Text = "Log-File";
            this.LogFile.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 435);
            this.Controls.Add(this.LogFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Log";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Log";
            this.Load += new System.EventHandler(this.Log_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogFile;
    }
}