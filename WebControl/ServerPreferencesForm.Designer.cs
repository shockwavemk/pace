namespace WebControl
{
    partial class ServerPreferencesForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.NumericUpDown();
            this.Width = new System.Windows.Forms.NumericUpDown();
            this.YPos = new System.Windows.Forms.NumericUpDown();
            this.XPos = new System.Windows.Forms.NumericUpDown();
            this.buttonAccept = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPos)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Y Postion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "X Postion";
            // 
            // Height
            // 
            this.Height.Location = new System.Drawing.Point(89, 96);
            this.Height.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(120, 20);
            this.Height.TabIndex = 11;
            this.Height.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // Width
            // 
            this.Width.Location = new System.Drawing.Point(89, 70);
            this.Width.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(120, 20);
            this.Width.TabIndex = 10;
            this.Width.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // YPos
            // 
            this.YPos.Location = new System.Drawing.Point(89, 38);
            this.YPos.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.YPos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.YPos.Name = "YPos";
            this.YPos.Size = new System.Drawing.Size(120, 20);
            this.YPos.TabIndex = 9;
            this.YPos.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // XPos
            // 
            this.XPos.Location = new System.Drawing.Point(89, 12);
            this.XPos.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.XPos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.XPos.Name = "XPos";
            this.XPos.Size = new System.Drawing.Size(120, 20);
            this.XPos.TabIndex = 8;
            this.XPos.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(18, 127);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(88, 23);
            this.buttonAccept.TabIndex = 16;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // ServerPreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 162);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.YPos);
            this.Controls.Add(this.XPos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ServerPreferencesForm";
            this.Text = "ServerPreferencesForm";
            ((System.ComponentModel.ISupportInitialize)(this.Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown Height;
        public System.Windows.Forms.NumericUpDown Width;
        public System.Windows.Forms.NumericUpDown YPos;
        public System.Windows.Forms.NumericUpDown XPos;
        private System.Windows.Forms.Button buttonAccept;
    }
}