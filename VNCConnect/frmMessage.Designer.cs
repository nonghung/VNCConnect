namespace VNCConnect
{
    partial class frmMessage
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
            this.lbMess = new System.Windows.Forms.Label();
            this.btYes = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btNo = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbMess
            // 
            this.lbMess.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMess.Location = new System.Drawing.Point(1, 9);
            this.lbMess.Name = "lbMess";
            this.lbMess.Size = new System.Drawing.Size(792, 319);
            this.lbMess.TabIndex = 0;
            this.lbMess.Text = "lbMess";
            this.lbMess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbMess.UseWaitCursor = true;
            // 
            // btYes
            // 
            this.btYes.BackColor = System.Drawing.Color.Lime;
            this.btYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btYes.Location = new System.Drawing.Point(182, 339);
            this.btYes.Name = "btYes";
            this.btYes.Size = new System.Drawing.Size(105, 57);
            this.btYes.TabIndex = 1;
            this.btYes.Text = "Yes";
            this.btYes.UseVisualStyleBackColor = false;
            this.btYes.UseWaitCursor = true;
            this.btYes.Visible = false;
            this.btYes.Click += new System.EventHandler(this.btYes_Click);
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOK.Location = new System.Drawing.Point(344, 339);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(105, 57);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.UseWaitCursor = true;
            this.btOK.Visible = false;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btNo
            // 
            this.btNo.BackColor = System.Drawing.Color.Red;
            this.btNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNo.Location = new System.Drawing.Point(508, 339);
            this.btNo.Name = "btNo";
            this.btNo.Size = new System.Drawing.Size(105, 57);
            this.btNo.TabIndex = 3;
            this.btNo.Text = "No";
            this.btNo.UseVisualStyleBackColor = false;
            this.btNo.UseWaitCursor = true;
            this.btNo.Visible = false;
            this.btNo.Click += new System.EventHandler(this.btNo_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(41, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(728, 276);
            this.textBox1.TabIndex = 4;
            this.textBox1.Visible = false;
            // 
            // frmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 425);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btNo);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btYes);
            this.Controls.Add(this.lbMess);
            this.Name = "frmMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsBox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmMessage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMess;
        private System.Windows.Forms.Button btYes;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btNo;
        private System.Windows.Forms.TextBox textBox1;
    }
}