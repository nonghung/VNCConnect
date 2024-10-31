namespace VNCConnect
{
    partial class Form2
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtStationIP_Search = new System.Windows.Forms.TextBox();
            this.labPN_Search = new System.Windows.Forms.Label();
            this.labStationIP_Search = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbSelBU = new System.Windows.Forms.ComboBox();
            this.cbPartNumber = new System.Windows.Forms.ComboBox();
            this.cbStation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_path_server = new System.Windows.Forms.TextBox();
            this.tb_path_local = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.bt_upload = new System.Windows.Forms.Button();
            this.bt_SelectFolder = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtStationIP_Search);
            this.groupBox4.Controls.Add(this.labPN_Search);
            this.groupBox4.Controls.Add(this.labStationIP_Search);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.cbSelBU);
            this.groupBox4.Controls.Add(this.cbPartNumber);
            this.groupBox4.Controls.Add(this.cbStation);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(11, 11);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(878, 74);
            this.groupBox4.TabIndex = 76;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Connect VNC by model";
            // 
            // txtStationIP_Search
            // 
            this.txtStationIP_Search.Location = new System.Drawing.Point(571, 28);
            this.txtStationIP_Search.Name = "txtStationIP_Search";
            this.txtStationIP_Search.Size = new System.Drawing.Size(142, 20);
            this.txtStationIP_Search.TabIndex = 59;
            // 
            // labPN_Search
            // 
            this.labPN_Search.AutoSize = true;
            this.labPN_Search.Location = new System.Drawing.Point(80, 32);
            this.labPN_Search.Name = "labPN_Search";
            this.labPN_Search.Size = new System.Drawing.Size(72, 13);
            this.labPN_Search.TabIndex = 57;
            this.labPN_Search.Text = "Part Number :";
            // 
            // labStationIP_Search
            // 
            this.labStationIP_Search.AutoSize = true;
            this.labStationIP_Search.Location = new System.Drawing.Point(499, 33);
            this.labStationIP_Search.Name = "labStationIP_Search";
            this.labStationIP_Search.Size = new System.Drawing.Size(56, 13);
            this.labStationIP_Search.TabIndex = 58;
            this.labStationIP_Search.Text = "Station IP:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(731, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 34);
            this.btnSearch.TabIndex = 56;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbSelBU
            // 
            this.cbSelBU.FormattingEnabled = true;
            this.cbSelBU.Items.AddRange(new object[] {
            "NA",
            "NC",
            "NB",
            "NH",
            "NE",
            "NW",
            "ND",
            "NP",
            "BA",
            "VBU"});
            this.cbSelBU.Location = new System.Drawing.Point(7, 28);
            this.cbSelBU.Name = "cbSelBU";
            this.cbSelBU.Size = new System.Drawing.Size(68, 21);
            this.cbSelBU.TabIndex = 60;
            this.cbSelBU.SelectedIndexChanged += new System.EventHandler(this.cbSelBU_SelectedIndexChanged);
            // 
            // cbPartNumber
            // 
            this.cbPartNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbPartNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPartNumber.FormattingEnabled = true;
            this.cbPartNumber.Location = new System.Drawing.Point(152, 28);
            this.cbPartNumber.Name = "cbPartNumber";
            this.cbPartNumber.Size = new System.Drawing.Size(132, 21);
            this.cbPartNumber.TabIndex = 61;
            this.cbPartNumber.SelectedIndexChanged += new System.EventHandler(this.cbPartNumber_SelectedIndexChanged);
            // 
            // cbStation
            // 
            this.cbStation.FormattingEnabled = true;
            this.cbStation.Location = new System.Drawing.Point(342, 25);
            this.cbStation.Name = "cbStation";
            this.cbStation.Size = new System.Drawing.Size(132, 21);
            this.cbStation.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Station :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 80;
            this.label2.Text = "server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "local";
            // 
            // tb_path_server
            // 
            this.tb_path_server.Enabled = false;
            this.tb_path_server.Location = new System.Drawing.Point(66, 140);
            this.tb_path_server.Name = "tb_path_server";
            this.tb_path_server.Size = new System.Drawing.Size(675, 20);
            this.tb_path_server.TabIndex = 78;
            // 
            // tb_path_local
            // 
            this.tb_path_local.Location = new System.Drawing.Point(66, 103);
            this.tb_path_local.Name = "tb_path_local";
            this.tb_path_local.Size = new System.Drawing.Size(675, 20);
            this.tb_path_local.TabIndex = 77;
            this.tb_path_local.Text = "D:\\Upserver";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(116, 296);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(594, 148);
            this.richTextBox1.TabIndex = 82;
            this.richTextBox1.Text = "";
            // 
            // bt_upload
            // 
            this.bt_upload.Location = new System.Drawing.Point(253, 182);
            this.bt_upload.Name = "bt_upload";
            this.bt_upload.Size = new System.Drawing.Size(131, 52);
            this.bt_upload.TabIndex = 81;
            this.bt_upload.Text = "upload";
            this.bt_upload.UseVisualStyleBackColor = true;
            this.bt_upload.Click += new System.EventHandler(this.bt_upload_Click);
            // 
            // bt_SelectFolder
            // 
            this.bt_SelectFolder.Location = new System.Drawing.Point(747, 100);
            this.bt_SelectFolder.Name = "bt_SelectFolder";
            this.bt_SelectFolder.Size = new System.Drawing.Size(91, 23);
            this.bt_SelectFolder.TabIndex = 83;
            this.bt_SelectFolder.Text = "Select Folder";
            this.bt_SelectFolder.UseVisualStyleBackColor = true;
            this.bt_SelectFolder.Click += new System.EventHandler(this.bt_SelectFolder_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 542);
            this.Controls.Add(this.bt_SelectFolder);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.bt_upload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_path_server);
            this.Controls.Add(this.tb_path_local);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtStationIP_Search;
        private System.Windows.Forms.Label labPN_Search;
        private System.Windows.Forms.Label labStationIP_Search;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbSelBU;
        private System.Windows.Forms.ComboBox cbPartNumber;
        private System.Windows.Forms.ComboBox cbStation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_path_server;
        private System.Windows.Forms.TextBox tb_path_local;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button bt_upload;
        private System.Windows.Forms.Button bt_SelectFolder;
    }
}