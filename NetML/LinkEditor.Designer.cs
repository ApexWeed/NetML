namespace NetML
{
    partial class LinkEditor
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtLinkDelay = new System.Windows.Forms.TextBox();
            this.lblLinkDelay = new System.Windows.Forms.Label();
            this.txtLinkBandwidth = new System.Windows.Forms.TextBox();
            this.lblLinkBandwidth = new System.Windows.Forms.Label();
            this.numQW = new System.Windows.Forms.NumericUpDown();
            this.lblQW = new System.Windows.Forms.Label();
            this.chkWait = new System.Windows.Forms.CheckBox();
            this.chkGentle = new System.Windows.Forms.CheckBox();
            this.numQueueLimit = new System.Windows.Forms.NumericUpDown();
            this.lblQueueLimit = new System.Windows.Forms.Label();
            this.numMaxTh = new System.Windows.Forms.NumericUpDown();
            this.lblMaxTh = new System.Windows.Forms.Label();
            this.numMinTh = new System.Windows.Forms.NumericUpDown();
            this.lblMinTh = new System.Windows.Forms.Label();
            this.numLinterm = new System.Windows.Forms.NumericUpDown();
            this.lblLinterm = new System.Windows.Forms.Label();
            this.numIdlePacketSize = new System.Windows.Forms.NumericUpDown();
            this.lblIdlePacketSize = new System.Windows.Forms.Label();
            this.numMeanPacketSize = new System.Windows.Forms.NumericUpDown();
            this.lblMeanPacketSize = new System.Windows.Forms.Label();
            this.numMaxPackets = new System.Windows.Forms.NumericUpDown();
            this.lblMaxPackets = new System.Windows.Forms.Label();
            this.numMaxBytes = new System.Windows.Forms.NumericUpDown();
            this.lblMaxBytes = new System.Windows.Forms.Label();
            this.cmbStartNode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEndNode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkDuplex = new System.Windows.Forms.CheckBox();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.txtDataRate = new System.Windows.Forms.TextBox();
            this.lblDataRate = new System.Windows.Forms.Label();
            this.txtBaseAddress = new System.Windows.Forms.TextBox();
            this.lblBaseAddress = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPacketMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbQueueType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numMtu = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinTh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLinterm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdlePacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeanPacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPackets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBytes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMtu)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtLinkDelay);
            this.pnlMain.Controls.Add(this.lblLinkDelay);
            this.pnlMain.Controls.Add(this.txtLinkBandwidth);
            this.pnlMain.Controls.Add(this.lblLinkBandwidth);
            this.pnlMain.Controls.Add(this.numQW);
            this.pnlMain.Controls.Add(this.lblQW);
            this.pnlMain.Controls.Add(this.chkWait);
            this.pnlMain.Controls.Add(this.chkGentle);
            this.pnlMain.Controls.Add(this.numQueueLimit);
            this.pnlMain.Controls.Add(this.lblQueueLimit);
            this.pnlMain.Controls.Add(this.numMaxTh);
            this.pnlMain.Controls.Add(this.lblMaxTh);
            this.pnlMain.Controls.Add(this.numMinTh);
            this.pnlMain.Controls.Add(this.lblMinTh);
            this.pnlMain.Controls.Add(this.numLinterm);
            this.pnlMain.Controls.Add(this.lblLinterm);
            this.pnlMain.Controls.Add(this.numIdlePacketSize);
            this.pnlMain.Controls.Add(this.lblIdlePacketSize);
            this.pnlMain.Controls.Add(this.numMeanPacketSize);
            this.pnlMain.Controls.Add(this.lblMeanPacketSize);
            this.pnlMain.Controls.Add(this.numMaxPackets);
            this.pnlMain.Controls.Add(this.lblMaxPackets);
            this.pnlMain.Controls.Add(this.numMaxBytes);
            this.pnlMain.Controls.Add(this.lblMaxBytes);
            this.pnlMain.Location = new System.Drawing.Point(12, 285);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(321, 304);
            this.pnlMain.TabIndex = 0;
            // 
            // txtLinkDelay
            // 
            this.txtLinkDelay.Location = new System.Drawing.Point(83, 275);
            this.txtLinkDelay.Name = "txtLinkDelay";
            this.txtLinkDelay.Size = new System.Drawing.Size(235, 19);
            this.txtLinkDelay.TabIndex = 55;
            // 
            // lblLinkDelay
            // 
            this.lblLinkDelay.AutoSize = true;
            this.lblLinkDelay.Location = new System.Drawing.Point(6, 278);
            this.lblLinkDelay.Name = "lblLinkDelay";
            this.lblLinkDelay.Size = new System.Drawing.Size(59, 12);
            this.lblLinkDelay.TabIndex = 54;
            this.lblLinkDelay.Text = "Link Delay";
            // 
            // txtLinkBandwidth
            // 
            this.txtLinkBandwidth.Location = new System.Drawing.Point(83, 250);
            this.txtLinkBandwidth.Name = "txtLinkBandwidth";
            this.txtLinkBandwidth.Size = new System.Drawing.Size(235, 19);
            this.txtLinkBandwidth.TabIndex = 53;
            // 
            // lblLinkBandwidth
            // 
            this.lblLinkBandwidth.AutoSize = true;
            this.lblLinkBandwidth.Location = new System.Drawing.Point(6, 253);
            this.lblLinkBandwidth.Name = "lblLinkBandwidth";
            this.lblLinkBandwidth.Size = new System.Drawing.Size(83, 12);
            this.lblLinkBandwidth.TabIndex = 52;
            this.lblLinkBandwidth.Text = "Link Bandwidth";
            // 
            // numQW
            // 
            this.numQW.DecimalPlaces = 10;
            this.numQW.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numQW.Location = new System.Drawing.Point(83, 225);
            this.numQW.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numQW.Name = "numQW";
            this.numQW.Size = new System.Drawing.Size(235, 19);
            this.numQW.TabIndex = 51;
            // 
            // lblQW
            // 
            this.lblQW.AutoSize = true;
            this.lblQW.Location = new System.Drawing.Point(9, 227);
            this.lblQW.Name = "lblQW";
            this.lblQW.Size = new System.Drawing.Size(22, 12);
            this.lblQW.TabIndex = 50;
            this.lblQW.Text = "QW";
            // 
            // chkWait
            // 
            this.chkWait.AutoSize = true;
            this.chkWait.Location = new System.Drawing.Point(160, 203);
            this.chkWait.Name = "chkWait";
            this.chkWait.Size = new System.Drawing.Size(46, 16);
            this.chkWait.TabIndex = 49;
            this.chkWait.Text = "Wait";
            this.chkWait.UseVisualStyleBackColor = true;
            // 
            // chkGentle
            // 
            this.chkGentle.AutoSize = true;
            this.chkGentle.Location = new System.Drawing.Point(4, 203);
            this.chkGentle.Name = "chkGentle";
            this.chkGentle.Size = new System.Drawing.Size(57, 16);
            this.chkGentle.TabIndex = 33;
            this.chkGentle.Text = "Gentle";
            this.chkGentle.UseVisualStyleBackColor = true;
            // 
            // numQueueLimit
            // 
            this.numQueueLimit.Location = new System.Drawing.Point(83, 178);
            this.numQueueLimit.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQueueLimit.Name = "numQueueLimit";
            this.numQueueLimit.Size = new System.Drawing.Size(235, 19);
            this.numQueueLimit.TabIndex = 48;
            // 
            // lblQueueLimit
            // 
            this.lblQueueLimit.AutoSize = true;
            this.lblQueueLimit.Location = new System.Drawing.Point(6, 180);
            this.lblQueueLimit.Name = "lblQueueLimit";
            this.lblQueueLimit.Size = new System.Drawing.Size(66, 12);
            this.lblQueueLimit.TabIndex = 47;
            this.lblQueueLimit.Text = "Queue Limit";
            // 
            // numMaxTh
            // 
            this.numMaxTh.Location = new System.Drawing.Point(83, 153);
            this.numMaxTh.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMaxTh.Name = "numMaxTh";
            this.numMaxTh.Size = new System.Drawing.Size(235, 19);
            this.numMaxTh.TabIndex = 46;
            // 
            // lblMaxTh
            // 
            this.lblMaxTh.AutoSize = true;
            this.lblMaxTh.Location = new System.Drawing.Point(6, 155);
            this.lblMaxTh.Name = "lblMaxTh";
            this.lblMaxTh.Size = new System.Drawing.Size(43, 12);
            this.lblMaxTh.TabIndex = 45;
            this.lblMaxTh.Text = "Max Th";
            // 
            // numMinTh
            // 
            this.numMinTh.Location = new System.Drawing.Point(83, 128);
            this.numMinTh.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMinTh.Name = "numMinTh";
            this.numMinTh.Size = new System.Drawing.Size(235, 19);
            this.numMinTh.TabIndex = 44;
            // 
            // lblMinTh
            // 
            this.lblMinTh.AutoSize = true;
            this.lblMinTh.Location = new System.Drawing.Point(6, 130);
            this.lblMinTh.Name = "lblMinTh";
            this.lblMinTh.Size = new System.Drawing.Size(40, 12);
            this.lblMinTh.TabIndex = 43;
            this.lblMinTh.Text = "Min Th";
            // 
            // numLinterm
            // 
            this.numLinterm.Location = new System.Drawing.Point(83, 103);
            this.numLinterm.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numLinterm.Name = "numLinterm";
            this.numLinterm.Size = new System.Drawing.Size(235, 19);
            this.numLinterm.TabIndex = 42;
            // 
            // lblLinterm
            // 
            this.lblLinterm.AutoSize = true;
            this.lblLinterm.Location = new System.Drawing.Point(6, 105);
            this.lblLinterm.Name = "lblLinterm";
            this.lblLinterm.Size = new System.Drawing.Size(43, 12);
            this.lblLinterm.TabIndex = 41;
            this.lblLinterm.Text = "Linterm";
            // 
            // numIdlePacketSize
            // 
            this.numIdlePacketSize.Location = new System.Drawing.Point(83, 78);
            this.numIdlePacketSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numIdlePacketSize.Name = "numIdlePacketSize";
            this.numIdlePacketSize.Size = new System.Drawing.Size(235, 19);
            this.numIdlePacketSize.TabIndex = 40;
            // 
            // lblIdlePacketSize
            // 
            this.lblIdlePacketSize.AutoSize = true;
            this.lblIdlePacketSize.Location = new System.Drawing.Point(6, 80);
            this.lblIdlePacketSize.Name = "lblIdlePacketSize";
            this.lblIdlePacketSize.Size = new System.Drawing.Size(87, 12);
            this.lblIdlePacketSize.TabIndex = 39;
            this.lblIdlePacketSize.Text = "Idle Packet Size";
            // 
            // numMeanPacketSize
            // 
            this.numMeanPacketSize.Location = new System.Drawing.Point(83, 53);
            this.numMeanPacketSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMeanPacketSize.Name = "numMeanPacketSize";
            this.numMeanPacketSize.Size = new System.Drawing.Size(235, 19);
            this.numMeanPacketSize.TabIndex = 38;
            // 
            // lblMeanPacketSize
            // 
            this.lblMeanPacketSize.AutoSize = true;
            this.lblMeanPacketSize.Location = new System.Drawing.Point(6, 55);
            this.lblMeanPacketSize.Name = "lblMeanPacketSize";
            this.lblMeanPacketSize.Size = new System.Drawing.Size(96, 12);
            this.lblMeanPacketSize.TabIndex = 37;
            this.lblMeanPacketSize.Text = "Mean Packet Size";
            // 
            // numMaxPackets
            // 
            this.numMaxPackets.Location = new System.Drawing.Point(83, 28);
            this.numMaxPackets.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMaxPackets.Name = "numMaxPackets";
            this.numMaxPackets.Size = new System.Drawing.Size(235, 19);
            this.numMaxPackets.TabIndex = 36;
            // 
            // lblMaxPackets
            // 
            this.lblMaxPackets.AutoSize = true;
            this.lblMaxPackets.Location = new System.Drawing.Point(6, 30);
            this.lblMaxPackets.Name = "lblMaxPackets";
            this.lblMaxPackets.Size = new System.Drawing.Size(71, 12);
            this.lblMaxPackets.TabIndex = 35;
            this.lblMaxPackets.Text = "Max Packets";
            // 
            // numMaxBytes
            // 
            this.numMaxBytes.Location = new System.Drawing.Point(83, 3);
            this.numMaxBytes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMaxBytes.Name = "numMaxBytes";
            this.numMaxBytes.Size = new System.Drawing.Size(235, 19);
            this.numMaxBytes.TabIndex = 34;
            // 
            // lblMaxBytes
            // 
            this.lblMaxBytes.AutoSize = true;
            this.lblMaxBytes.Location = new System.Drawing.Point(6, 5);
            this.lblMaxBytes.Name = "lblMaxBytes";
            this.lblMaxBytes.Size = new System.Drawing.Size(60, 12);
            this.lblMaxBytes.TabIndex = 33;
            this.lblMaxBytes.Text = "Max Bytes";
            // 
            // cmbStartNode
            // 
            this.cmbStartNode.FormattingEnabled = true;
            this.cmbStartNode.Location = new System.Drawing.Point(95, 62);
            this.cmbStartNode.Name = "cmbStartNode";
            this.cmbStartNode.Size = new System.Drawing.Size(238, 20);
            this.cmbStartNode.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "X";
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(205, 37);
            this.numY.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(128, 19);
            this.numY.TabIndex = 11;
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(30, 37);
            this.numX.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(128, 19);
            this.numX.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(238, 19);
            this.txtName.TabIndex = 9;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // cmbEndNode
            // 
            this.cmbEndNode.FormattingEnabled = true;
            this.cmbEndNode.Location = new System.Drawing.Point(95, 88);
            this.cmbEndNode.Name = "cmbEndNode";
            this.cmbEndNode.Size = new System.Drawing.Size(238, 20);
            this.cmbEndNode.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "End";
            // 
            // chkDuplex
            // 
            this.chkDuplex.AutoSize = true;
            this.chkDuplex.Location = new System.Drawing.Point(14, 240);
            this.chkDuplex.Name = "chkDuplex";
            this.chkDuplex.Size = new System.Drawing.Size(59, 16);
            this.chkDuplex.TabIndex = 19;
            this.chkDuplex.Text = "Duplex";
            this.chkDuplex.UseVisualStyleBackColor = true;
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(95, 164);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(238, 19);
            this.txtDelay.TabIndex = 25;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(12, 167);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(34, 12);
            this.lblDelay.TabIndex = 24;
            this.lblDelay.Text = "Delay";
            // 
            // txtDataRate
            // 
            this.txtDataRate.Location = new System.Drawing.Point(95, 139);
            this.txtDataRate.Name = "txtDataRate";
            this.txtDataRate.Size = new System.Drawing.Size(238, 19);
            this.txtDataRate.TabIndex = 23;
            // 
            // lblDataRate
            // 
            this.lblDataRate.AutoSize = true;
            this.lblDataRate.Location = new System.Drawing.Point(12, 142);
            this.lblDataRate.Name = "lblDataRate";
            this.lblDataRate.Size = new System.Drawing.Size(57, 12);
            this.lblDataRate.TabIndex = 22;
            this.lblDataRate.Text = "Data Rate";
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Location = new System.Drawing.Point(95, 114);
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Size = new System.Drawing.Size(238, 19);
            this.txtBaseAddress.TabIndex = 21;
            // 
            // lblBaseAddress
            // 
            this.lblBaseAddress.AutoSize = true;
            this.lblBaseAddress.Location = new System.Drawing.Point(12, 117);
            this.lblBaseAddress.Name = "lblBaseAddress";
            this.lblBaseAddress.Size = new System.Drawing.Size(77, 12);
            this.lblBaseAddress.TabIndex = 20;
            this.lblBaseAddress.Text = "Base Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "Mtu";
            // 
            // cmbPacketMode
            // 
            this.cmbPacketMode.FormattingEnabled = true;
            this.cmbPacketMode.Location = new System.Drawing.Point(95, 214);
            this.cmbPacketMode.Name = "cmbPacketMode";
            this.cmbPacketMode.Size = new System.Drawing.Size(238, 20);
            this.cmbPacketMode.TabIndex = 29;
            this.cmbPacketMode.SelectedIndexChanged += new System.EventHandler(this.cmbPacketMode_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "Packet Mode";
            // 
            // cmbQueueType
            // 
            this.cmbQueueType.FormattingEnabled = true;
            this.cmbQueueType.Location = new System.Drawing.Point(95, 262);
            this.cmbQueueType.Name = "cmbQueueType";
            this.cmbQueueType.Size = new System.Drawing.Size(238, 20);
            this.cmbQueueType.TabIndex = 31;
            this.cmbQueueType.SelectedIndexChanged += new System.EventHandler(this.cmbQueueType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "Queue Type";
            // 
            // numMtu
            // 
            this.numMtu.Location = new System.Drawing.Point(95, 189);
            this.numMtu.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMtu.Name = "numMtu";
            this.numMtu.Size = new System.Drawing.Size(238, 19);
            this.numMtu.TabIndex = 32;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 595);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(258, 595);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 34;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // LinkEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 629);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numMtu);
            this.Controls.Add(this.cmbQueueType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPacketMode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDelay);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.txtDataRate);
            this.Controls.Add(this.lblDataRate);
            this.Controls.Add(this.txtBaseAddress);
            this.Controls.Add(this.lblBaseAddress);
            this.Controls.Add(this.chkDuplex);
            this.Controls.Add(this.cmbEndNode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbStartNode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(361, 2000);
            this.MinimumSize = new System.Drawing.Size(361, 39);
            this.Name = "LinkEditor";
            this.Text = "StreamEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinkEditor_FormClosing);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinTh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLinterm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdlePacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeanPacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPackets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBytes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMtu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ComboBox cmbStartNode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEndNode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkDuplex;
        private System.Windows.Forms.NumericUpDown numMaxPackets;
        private System.Windows.Forms.Label lblMaxPackets;
        private System.Windows.Forms.NumericUpDown numMaxBytes;
        private System.Windows.Forms.Label lblMaxBytes;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.TextBox txtDataRate;
        private System.Windows.Forms.Label lblDataRate;
        private System.Windows.Forms.TextBox txtBaseAddress;
        private System.Windows.Forms.Label lblBaseAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPacketMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbQueueType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numMtu;
        private System.Windows.Forms.CheckBox chkWait;
        private System.Windows.Forms.CheckBox chkGentle;
        private System.Windows.Forms.NumericUpDown numQueueLimit;
        private System.Windows.Forms.Label lblQueueLimit;
        private System.Windows.Forms.NumericUpDown numMaxTh;
        private System.Windows.Forms.Label lblMaxTh;
        private System.Windows.Forms.NumericUpDown numMinTh;
        private System.Windows.Forms.Label lblMinTh;
        private System.Windows.Forms.NumericUpDown numLinterm;
        private System.Windows.Forms.Label lblLinterm;
        private System.Windows.Forms.NumericUpDown numIdlePacketSize;
        private System.Windows.Forms.Label lblIdlePacketSize;
        private System.Windows.Forms.NumericUpDown numMeanPacketSize;
        private System.Windows.Forms.Label lblMeanPacketSize;
        private System.Windows.Forms.TextBox txtLinkDelay;
        private System.Windows.Forms.Label lblLinkDelay;
        private System.Windows.Forms.TextBox txtLinkBandwidth;
        private System.Windows.Forms.Label lblLinkBandwidth;
        private System.Windows.Forms.NumericUpDown numQW;
        private System.Windows.Forms.Label lblQW;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}