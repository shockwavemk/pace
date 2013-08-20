namespace PaceServer
{
    partial class NewConnection
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
            this.textBoxUri = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelUri = new System.Windows.Forms.Label();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxUri
            // 
            this.textBoxUri.Location = new System.Drawing.Point(84, 12);
            this.textBoxUri.Name = "textBoxUri";
            this.textBoxUri.Size = new System.Drawing.Size(177, 20);
            this.textBoxUri.TabIndex = 0;
            this.textBoxUri.Text = "localhost";
            // 
            // buttonConnect
            // 
            this.buttonConnect.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonConnect.Location = new System.Drawing.Point(12, 83);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(116, 24);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelUri
            // 
            this.labelUri.AutoSize = true;
            this.labelUri.Location = new System.Drawing.Point(12, 15);
            this.labelUri.Name = "labelUri";
            this.labelUri.Size = new System.Drawing.Size(54, 13);
            this.labelUri.TabIndex = 2;
            this.labelUri.Text = "URI or IP:";
            // 
            // buttonAbort
            // 
            this.buttonAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAbort.Location = new System.Drawing.Point(138, 83);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(123, 24);
            this.buttonAbort.TabIndex = 3;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(12, 52);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "Port:";
            this.labelPort.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(84, 49);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(177, 20);
            this.textBoxPort.TabIndex = 5;
            this.textBoxPort.Text = "9091";
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // NewConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonAbort;
            this.ClientSize = new System.Drawing.Size(273, 119);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.labelUri);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxUri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewConnection";
            this.ShowInTaskbar = false;
            this.Text = "Establish a new connection";
            this.Load += new System.EventHandler(this.NewConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUri;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelUri;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
    }
}