namespace PaceServer
{
    partial class SecurityOptionsForm
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
            this.buttonSetPassword = new System.Windows.Forms.Button();
            this.textBoxClientPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSetPassword
            // 
            this.buttonSetPassword.Location = new System.Drawing.Point(180, 8);
            this.buttonSetPassword.Name = "buttonSetPassword";
            this.buttonSetPassword.Size = new System.Drawing.Size(124, 23);
            this.buttonSetPassword.TabIndex = 0;
            this.buttonSetPassword.Text = "Set Client-Password";
            this.buttonSetPassword.UseVisualStyleBackColor = true;
            // 
            // textBoxClientPassword
            // 
            this.textBoxClientPassword.Location = new System.Drawing.Point(74, 10);
            this.textBoxClientPassword.Name = "textBoxClientPassword";
            this.textBoxClientPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxClientPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password:";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(13, 39);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 3;
            this.buttonAccept.Text = "Save";
            this.buttonAccept.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(99, 39);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // SecurityOptionsForm
            // 
            this.AcceptButton = this.buttonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(315, 74);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxClientPassword);
            this.Controls.Add(this.buttonSetPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SecurityOptionsForm";
            this.Text = "Security Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSetPassword;
        private System.Windows.Forms.TextBox textBoxClientPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
    }
}