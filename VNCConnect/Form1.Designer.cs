namespace VNCConnect
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.cbStation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPartNumber = new System.Windows.Forms.ComboBox();
            this.cbSelBU = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labPN_Search = new System.Windows.Forms.Label();
            this.dvTableView = new System.Windows.Forms.DataGridView();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tbStation = new System.Windows.Forms.TextBox();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_LoadFromGroupModel = new System.Windows.Forms.Button();
            this.cbbListModel = new System.Windows.Forms.ComboBox();
            this.tb_ListModel = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_DeleteModel = new System.Windows.Forms.Button();
            this.bt_addModel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bt_smbConnect = new System.Windows.Forms.Button();
            this.cb_listIP = new System.Windows.Forms.ComboBox();
            this.bt_connect = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtGridViewFail = new System.Windows.Forms.DataGridView();
            this.gb_PCControl = new System.Windows.Forms.GroupBox();
            this.bt_EN_Telnet = new System.Windows.Forms.Button();
            this.bt_Dis_MSTSC = new System.Windows.Forms.Button();
            this.bt_En_MSTSC = new System.Windows.Forms.Button();
            this.bt_UsbOff = new System.Windows.Forms.Button();
            this.bt_UsbOn = new System.Windows.Forms.Button();
            this.bt_Reset_VNC_PW = new System.Windows.Forms.Button();
            this.bt_disable_Defender = new System.Windows.Forms.Button();
            this.bt_Restart = new System.Windows.Forms.Button();
            this.tb_Ip_Restart = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dvTableView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewFail)).BeginInit();
            this.gb_PCControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 60);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load from list file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbStation
            // 
            this.cbStation.FormattingEnabled = true;
            this.cbStation.Location = new System.Drawing.Point(41, 49);
            this.cbStation.Name = "cbStation";
            this.cbStation.Size = new System.Drawing.Size(142, 21);
            this.cbStation.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Station :";
            // 
            // cbPartNumber
            // 
            this.cbPartNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbPartNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPartNumber.FormattingEnabled = true;
            this.cbPartNumber.Location = new System.Drawing.Point(152, 18);
            this.cbPartNumber.Name = "cbPartNumber";
            this.cbPartNumber.Size = new System.Drawing.Size(139, 21);
            this.cbPartNumber.TabIndex = 61;
            this.cbPartNumber.SelectedIndexChanged += new System.EventHandler(this.cbPartNumber_SelectedIndexChanged);
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
            "VBU",
            "NS"});
            this.cbSelBU.Location = new System.Drawing.Point(7, 18);
            this.cbSelBU.Name = "cbSelBU";
            this.cbSelBU.Size = new System.Drawing.Size(68, 21);
            this.cbSelBU.TabIndex = 60;
            this.cbSelBU.SelectedIndexChanged += new System.EventHandler(this.cbSelBU_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(206, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 34);
            this.btnSearch.TabIndex = 56;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labPN_Search
            // 
            this.labPN_Search.AutoSize = true;
            this.labPN_Search.Location = new System.Drawing.Point(80, 22);
            this.labPN_Search.Name = "labPN_Search";
            this.labPN_Search.Size = new System.Drawing.Size(72, 13);
            this.labPN_Search.TabIndex = 57;
            this.labPN_Search.Text = "Part Number :";
            // 
            // dvTableView
            // 
            this.dvTableView.AllowDrop = true;
            this.dvTableView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Cyan;
            this.dvTableView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dvTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvTableView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dvTableView.Location = new System.Drawing.Point(308, 2);
            this.dvTableView.Name = "dvTableView";
            this.dvTableView.RowHeadersWidth = 51;
            this.dvTableView.RowTemplate.Height = 18;
            this.dvTableView.ShowCellToolTips = false;
            this.dvTableView.Size = new System.Drawing.Size(695, 401);
            this.dvTableView.TabIndex = 64;
            this.dvTableView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dvTableView_CellMouseDoubleClick);
            this.dvTableView.Sorted += new System.EventHandler(this.dvTableView_Sorted);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(4, 2);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(40, 13);
            this.lbStatus.TabIndex = 65;
            this.lbStatus.Text = "Status:";
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(82, 41);
            this.tbStation.Margin = new System.Windows.Forms.Padding(2);
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(108, 20);
            this.tbStation.TabIndex = 66;
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(83, 17);
            this.tbIP.Margin = new System.Windows.Forms.Padding(2);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(107, 20);
            this.tbIP.TabIndex = 67;
            // 
            // tbModel
            // 
            this.tbModel.Location = new System.Drawing.Point(82, 66);
            this.tbModel.Margin = new System.Windows.Forms.Padding(2);
            this.tbModel.Name = "tbModel";
            this.tbModel.Size = new System.Drawing.Size(108, 20);
            this.tbModel.TabIndex = 68;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(206, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 28);
            this.button2.TabIndex = 69;
            this.button2.Text = "add new";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Station_IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Station_Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "Model:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbStation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbModel);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(2, 455);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(296, 92);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "add new IP";
            // 
            // bt_LoadFromGroupModel
            // 
            this.bt_LoadFromGroupModel.Enabled = false;
            this.bt_LoadFromGroupModel.Location = new System.Drawing.Point(190, 61);
            this.bt_LoadFromGroupModel.Margin = new System.Windows.Forms.Padding(2);
            this.bt_LoadFromGroupModel.Name = "bt_LoadFromGroupModel";
            this.bt_LoadFromGroupModel.Size = new System.Drawing.Size(90, 27);
            this.bt_LoadFromGroupModel.TabIndex = 74;
            this.bt_LoadFromGroupModel.Text = "Load by model";
            this.bt_LoadFromGroupModel.UseVisualStyleBackColor = true;
            this.bt_LoadFromGroupModel.Click += new System.EventHandler(this.bt_LoadFromGroupModel_Click);
            // 
            // cbbListModel
            // 
            this.cbbListModel.FormattingEnabled = true;
            this.cbbListModel.Location = new System.Drawing.Point(112, 10);
            this.cbbListModel.Name = "cbbListModel";
            this.cbbListModel.Size = new System.Drawing.Size(153, 21);
            this.cbbListModel.TabIndex = 76;
            this.cbbListModel.SelectedIndexChanged += new System.EventHandler(this.cbbListModel_SelectedIndexChanged);
            // 
            // tb_ListModel
            // 
            this.tb_ListModel.Location = new System.Drawing.Point(5, 34);
            this.tb_ListModel.Name = "tb_ListModel";
            this.tb_ListModel.Size = new System.Drawing.Size(277, 20);
            this.tb_ListModel.TabIndex = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_DeleteModel);
            this.groupBox2.Controls.Add(this.bt_addModel);
            this.groupBox2.Controls.Add(this.cbbListModel);
            this.groupBox2.Controls.Add(this.bt_LoadFromGroupModel);
            this.groupBox2.Controls.Add(this.tb_ListModel);
            this.groupBox2.Location = new System.Drawing.Point(2, 216);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(296, 100);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Config by model";
            // 
            // bt_DeleteModel
            // 
            this.bt_DeleteModel.Location = new System.Drawing.Point(84, 61);
            this.bt_DeleteModel.Margin = new System.Windows.Forms.Padding(2);
            this.bt_DeleteModel.Name = "bt_DeleteModel";
            this.bt_DeleteModel.Size = new System.Drawing.Size(76, 27);
            this.bt_DeleteModel.TabIndex = 78;
            this.bt_DeleteModel.Text = "Del Model";
            this.bt_DeleteModel.UseVisualStyleBackColor = true;
            this.bt_DeleteModel.Click += new System.EventHandler(this.bt_Delete_Click);
            // 
            // bt_addModel
            // 
            this.bt_addModel.Location = new System.Drawing.Point(4, 61);
            this.bt_addModel.Margin = new System.Windows.Forms.Padding(2);
            this.bt_addModel.Name = "bt_addModel";
            this.bt_addModel.Size = new System.Drawing.Size(76, 27);
            this.bt_addModel.TabIndex = 77;
            this.bt_addModel.Text = "add model";
            this.bt_addModel.UseVisualStyleBackColor = true;
            this.bt_addModel.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_smbConnect);
            this.groupBox3.Controls.Add(this.cb_listIP);
            this.groupBox3.Controls.Add(this.bt_connect);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(10, 17);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(288, 98);
            this.groupBox3.TabIndex = 74;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connect VNC by IP";
            // 
            // bt_smbConnect
            // 
            this.bt_smbConnect.Location = new System.Drawing.Point(238, 60);
            this.bt_smbConnect.Margin = new System.Windows.Forms.Padding(2);
            this.bt_smbConnect.Name = "bt_smbConnect";
            this.bt_smbConnect.Size = new System.Drawing.Size(42, 34);
            this.bt_smbConnect.TabIndex = 72;
            this.bt_smbConnect.Text = "SMB";
            this.bt_smbConnect.UseVisualStyleBackColor = true;
            this.bt_smbConnect.Click += new System.EventHandler(this.bt_smbConnect_Click);
            // 
            // cb_listIP
            // 
            this.cb_listIP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_listIP.FormattingEnabled = true;
            this.cb_listIP.Location = new System.Drawing.Point(62, 18);
            this.cb_listIP.Name = "cb_listIP";
            this.cb_listIP.Size = new System.Drawing.Size(173, 21);
            this.cb_listIP.TabIndex = 71;
            // 
            // bt_connect
            // 
            this.bt_connect.Location = new System.Drawing.Point(63, 60);
            this.bt_connect.Margin = new System.Windows.Forms.Padding(2);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(172, 34);
            this.bt_connect.TabIndex = 69;
            this.bt_connect.Text = "Connect";
            this.bt_connect.UseVisualStyleBackColor = true;
            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-2, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Station_IP:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labPN_Search);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.cbSelBU);
            this.groupBox4.Controls.Add(this.cbPartNumber);
            this.groupBox4.Controls.Add(this.cbStation);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(2, 128);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(296, 84);
            this.groupBox4.TabIndex = 75;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Connect VNC by model";
            // 
            // dtGridViewFail
            // 
            this.dtGridViewFail.AllowDrop = true;
            this.dtGridViewFail.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Cyan;
            this.dtGridViewFail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dtGridViewFail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewFail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtGridViewFail.Location = new System.Drawing.Point(308, 404);
            this.dtGridViewFail.Name = "dtGridViewFail";
            this.dtGridViewFail.RowHeadersWidth = 51;
            this.dtGridViewFail.RowTemplate.Height = 18;
            this.dtGridViewFail.ShowCellToolTips = false;
            this.dtGridViewFail.Size = new System.Drawing.Size(695, 273);
            this.dtGridViewFail.TabIndex = 78;
            // 
            // gb_PCControl
            // 
            this.gb_PCControl.Controls.Add(this.bt_EN_Telnet);
            this.gb_PCControl.Controls.Add(this.bt_Dis_MSTSC);
            this.gb_PCControl.Controls.Add(this.bt_En_MSTSC);
            this.gb_PCControl.Controls.Add(this.bt_UsbOff);
            this.gb_PCControl.Controls.Add(this.bt_UsbOn);
            this.gb_PCControl.Controls.Add(this.bt_Reset_VNC_PW);
            this.gb_PCControl.Controls.Add(this.bt_disable_Defender);
            this.gb_PCControl.Controls.Add(this.bt_Restart);
            this.gb_PCControl.Controls.Add(this.tb_Ip_Restart);
            this.gb_PCControl.Controls.Add(this.label8);
            this.gb_PCControl.Location = new System.Drawing.Point(2, 320);
            this.gb_PCControl.Margin = new System.Windows.Forms.Padding(2);
            this.gb_PCControl.Name = "gb_PCControl";
            this.gb_PCControl.Padding = new System.Windows.Forms.Padding(2);
            this.gb_PCControl.Size = new System.Drawing.Size(296, 122);
            this.gb_PCControl.TabIndex = 74;
            this.gb_PCControl.TabStop = false;
            this.gb_PCControl.Text = "PC Control by IP";
            // 
            // bt_EN_Telnet
            // 
            this.bt_EN_Telnet.Enabled = false;
            this.bt_EN_Telnet.Location = new System.Drawing.Point(199, 69);
            this.bt_EN_Telnet.Margin = new System.Windows.Forms.Padding(2);
            this.bt_EN_Telnet.Name = "bt_EN_Telnet";
            this.bt_EN_Telnet.Size = new System.Drawing.Size(89, 23);
            this.bt_EN_Telnet.TabIndex = 77;
            this.bt_EN_Telnet.Text = "EN Telnet";
            this.bt_EN_Telnet.UseVisualStyleBackColor = true;
            this.bt_EN_Telnet.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_Dis_MSTSC
            // 
            this.bt_Dis_MSTSC.Enabled = false;
            this.bt_Dis_MSTSC.Location = new System.Drawing.Point(103, 69);
            this.bt_Dis_MSTSC.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Dis_MSTSC.Name = "bt_Dis_MSTSC";
            this.bt_Dis_MSTSC.Size = new System.Drawing.Size(89, 23);
            this.bt_Dis_MSTSC.TabIndex = 76;
            this.bt_Dis_MSTSC.Text = "DIS MSTSC";
            this.bt_Dis_MSTSC.UseVisualStyleBackColor = true;
            this.bt_Dis_MSTSC.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_En_MSTSC
            // 
            this.bt_En_MSTSC.Enabled = false;
            this.bt_En_MSTSC.Location = new System.Drawing.Point(103, 96);
            this.bt_En_MSTSC.Margin = new System.Windows.Forms.Padding(2);
            this.bt_En_MSTSC.Name = "bt_En_MSTSC";
            this.bt_En_MSTSC.Size = new System.Drawing.Size(89, 23);
            this.bt_En_MSTSC.TabIndex = 75;
            this.bt_En_MSTSC.Text = "EN MSTSC";
            this.bt_En_MSTSC.UseVisualStyleBackColor = true;
            this.bt_En_MSTSC.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_UsbOff
            // 
            this.bt_UsbOff.Enabled = false;
            this.bt_UsbOff.Location = new System.Drawing.Point(7, 68);
            this.bt_UsbOff.Margin = new System.Windows.Forms.Padding(2);
            this.bt_UsbOff.Name = "bt_UsbOff";
            this.bt_UsbOff.Size = new System.Drawing.Size(89, 23);
            this.bt_UsbOff.TabIndex = 74;
            this.bt_UsbOff.Text = "USB OFF";
            this.bt_UsbOff.UseVisualStyleBackColor = true;
            this.bt_UsbOff.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_UsbOn
            // 
            this.bt_UsbOn.Enabled = false;
            this.bt_UsbOn.Location = new System.Drawing.Point(7, 41);
            this.bt_UsbOn.Margin = new System.Windows.Forms.Padding(2);
            this.bt_UsbOn.Name = "bt_UsbOn";
            this.bt_UsbOn.Size = new System.Drawing.Size(89, 23);
            this.bt_UsbOn.TabIndex = 73;
            this.bt_UsbOn.Text = "USB ON";
            this.bt_UsbOn.UseVisualStyleBackColor = true;
            this.bt_UsbOn.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_Reset_VNC_PW
            // 
            this.bt_Reset_VNC_PW.Enabled = false;
            this.bt_Reset_VNC_PW.Location = new System.Drawing.Point(199, 42);
            this.bt_Reset_VNC_PW.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Reset_VNC_PW.Name = "bt_Reset_VNC_PW";
            this.bt_Reset_VNC_PW.Size = new System.Drawing.Size(89, 23);
            this.bt_Reset_VNC_PW.TabIndex = 72;
            this.bt_Reset_VNC_PW.Text = "Reset VNC PW";
            this.bt_Reset_VNC_PW.UseVisualStyleBackColor = true;
            this.bt_Reset_VNC_PW.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_disable_Defender
            // 
            this.bt_disable_Defender.Enabled = false;
            this.bt_disable_Defender.Location = new System.Drawing.Point(103, 42);
            this.bt_disable_Defender.Margin = new System.Windows.Forms.Padding(2);
            this.bt_disable_Defender.Name = "bt_disable_Defender";
            this.bt_disable_Defender.Size = new System.Drawing.Size(89, 23);
            this.bt_disable_Defender.TabIndex = 71;
            this.bt_disable_Defender.Text = "DIS Defender";
            this.bt_disable_Defender.UseVisualStyleBackColor = true;
            this.bt_disable_Defender.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // bt_Restart
            // 
            this.bt_Restart.Enabled = false;
            this.bt_Restart.Location = new System.Drawing.Point(199, 15);
            this.bt_Restart.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Restart.Name = "bt_Restart";
            this.bt_Restart.Size = new System.Drawing.Size(89, 23);
            this.bt_Restart.TabIndex = 69;
            this.bt_Restart.Text = "Restart PC";
            this.bt_Restart.UseVisualStyleBackColor = true;
            this.bt_Restart.Click += new System.EventHandler(this.bt_PCControl_Click);
            // 
            // tb_Ip_Restart
            // 
            this.tb_Ip_Restart.Location = new System.Drawing.Point(27, 17);
            this.tb_Ip_Restart.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Ip_Restart.Name = "tb_Ip_Restart";
            this.tb_Ip_Restart.Size = new System.Drawing.Size(163, 20);
            this.tb_Ip_Restart.TabIndex = 67;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "IP:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 442);
            this.Controls.Add(this.gb_PCControl);
            this.Controls.Add(this.dtGridViewFail);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.dvTableView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VNC Connect";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.dvTableView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewFail)).EndInit();
            this.gb_PCControl.ResumeLayout(false);
            this.gb_PCControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbStation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPartNumber;
        private System.Windows.Forms.ComboBox cbSelBU;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labPN_Search;
        private System.Windows.Forms.DataGridView dvTableView;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox tbStation;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.TextBox tbModel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_LoadFromGroupModel;
        private System.Windows.Forms.ComboBox cbbListModel;
        private System.Windows.Forms.TextBox tb_ListModel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_addModel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bt_connect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_listIP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dtGridViewFail;
        private System.Windows.Forms.Button bt_smbConnect;
        private System.Windows.Forms.Button bt_DeleteModel;
        private System.Windows.Forms.GroupBox gb_PCControl;
        private System.Windows.Forms.Button bt_Restart;
        private System.Windows.Forms.TextBox tb_Ip_Restart;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bt_disable_Defender;
        private System.Windows.Forms.Button bt_Reset_VNC_PW;
        private System.Windows.Forms.Button bt_UsbOff;
        private System.Windows.Forms.Button bt_UsbOn;
        private System.Windows.Forms.Button bt_En_MSTSC;
        private System.Windows.Forms.Button bt_Dis_MSTSC;
        private System.Windows.Forms.Button bt_EN_Telnet;
    }
}

