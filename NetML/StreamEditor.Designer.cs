namespace NetML
{
    partial class StreamEditor
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
            this.cmbEndNode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStartNode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numPacketSize = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numMaxPackets = new System.Windows.Forms.NumericUpDown();
            this.lblMaxPackets = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.numEndMaxWindowSize = new System.Windows.Forms.NumericUpDown();
            this.lblEndMaxWindowSize = new System.Windows.Forms.Label();
            this.numEndSendBufferSize = new System.Windows.Forms.NumericUpDown();
            this.lblEndSendBufferSize = new System.Windows.Forms.Label();
            this.numEndReceiveBufferSize = new System.Windows.Forms.NumericUpDown();
            this.lblEndReceiveBufferSize = new System.Windows.Forms.Label();
            this.numStartMaxWindowSize = new System.Windows.Forms.NumericUpDown();
            this.lblStartMaxWindowSize = new System.Windows.Forms.Label();
            this.numStartSendBufferSize = new System.Windows.Forms.NumericUpDown();
            this.lblStartSendBufferSize = new System.Windows.Forms.Label();
            this.numStartReceiveBufferSize = new System.Windows.Forms.NumericUpDown();
            this.lblStartReceiveBufferSize = new System.Windows.Forms.Label();
            this.numMaxBytes = new System.Windows.Forms.NumericUpDown();
            this.lblMaxBytes = new System.Windows.Forms.Label();
            this.cmbStreamType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtCBRRate = new System.Windows.Forms.TextBox();
            this.lblCBRRate = new System.Windows.Forms.Label();
            this.cmbOnDistribution = new System.Windows.Forms.ComboBox();
            this.lblOnDistribution = new System.Windows.Forms.Label();
            this.cmbOffDistribution = new System.Windows.Forms.ComboBox();
            this.lblOffDistribution = new System.Windows.Forms.Label();
            this.cmbTransportProtocol = new System.Windows.Forms.ComboBox();
            this.lblTransportProtocol = new System.Windows.Forms.Label();
            this.txtOnInterval = new System.Windows.Forms.TextBox();
            this.lblOnInterval = new System.Windows.Forms.Label();
            this.txtOffInterval = new System.Windows.Forms.TextBox();
            this.lblOffInterval = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPackets)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEndMaxWindowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndSendBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndReceiveBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartMaxWindowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartSendBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartReceiveBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBytes)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbEndNode
            // 
            this.cmbEndNode.FormattingEnabled = true;
            this.cmbEndNode.Location = new System.Drawing.Point(95, 88);
            this.cmbEndNode.Name = "cmbEndNode";
            this.cmbEndNode.Size = new System.Drawing.Size(238, 20);
            this.cmbEndNode.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "End";
            // 
            // cmbStartNode
            // 
            this.cmbStartNode.FormattingEnabled = true;
            this.cmbStartNode.Location = new System.Drawing.Point(95, 62);
            this.cmbStartNode.Name = "cmbStartNode";
            this.cmbStartNode.Size = new System.Drawing.Size(238, 20);
            this.cmbStartNode.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 22;
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
            this.numY.TabIndex = 21;
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
            this.numX.TabIndex = 20;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(238, 19);
            this.txtName.TabIndex = 19;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "Name";
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(95, 114);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(238, 19);
            this.txtStartTime.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "Start Time";
            // 
            // txtEndTime
            // 
            this.txtEndTime.Location = new System.Drawing.Point(95, 139);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(238, 19);
            this.txtEndTime.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "End Time";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(95, 164);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(238, 19);
            this.numPort.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 12);
            this.label9.TabIndex = 39;
            this.label9.Text = "Port";
            // 
            // numPacketSize
            // 
            this.numPacketSize.Location = new System.Drawing.Point(95, 189);
            this.numPacketSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numPacketSize.Name = "numPacketSize";
            this.numPacketSize.Size = new System.Drawing.Size(238, 19);
            this.numPacketSize.TabIndex = 42;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "Packet Size";
            // 
            // numMaxPackets
            // 
            this.numMaxPackets.Location = new System.Drawing.Point(81, 28);
            this.numMaxPackets.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numMaxPackets.Name = "numMaxPackets";
            this.numMaxPackets.Size = new System.Drawing.Size(238, 19);
            this.numMaxPackets.TabIndex = 46;
            // 
            // lblMaxPackets
            // 
            this.lblMaxPackets.AutoSize = true;
            this.lblMaxPackets.Location = new System.Drawing.Point(-2, 30);
            this.lblMaxPackets.Name = "lblMaxPackets";
            this.lblMaxPackets.Size = new System.Drawing.Size(71, 12);
            this.lblMaxPackets.TabIndex = 45;
            this.lblMaxPackets.Text = "Max Packets";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(81, 3);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(238, 19);
            this.txtInterval.TabIndex = 44;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(-2, 6);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(43, 12);
            this.lblInterval.TabIndex = 43;
            this.lblInterval.Text = "Interval";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtOffInterval);
            this.pnlMain.Controls.Add(this.lblOffInterval);
            this.pnlMain.Controls.Add(this.txtOnInterval);
            this.pnlMain.Controls.Add(this.lblOnInterval);
            this.pnlMain.Controls.Add(this.cmbTransportProtocol);
            this.pnlMain.Controls.Add(this.lblTransportProtocol);
            this.pnlMain.Controls.Add(this.cmbOffDistribution);
            this.pnlMain.Controls.Add(this.lblOffDistribution);
            this.pnlMain.Controls.Add(this.cmbOnDistribution);
            this.pnlMain.Controls.Add(this.lblOnDistribution);
            this.pnlMain.Controls.Add(this.txtCBRRate);
            this.pnlMain.Controls.Add(this.lblCBRRate);
            this.pnlMain.Controls.Add(this.numEndMaxWindowSize);
            this.pnlMain.Controls.Add(this.lblEndMaxWindowSize);
            this.pnlMain.Controls.Add(this.numEndSendBufferSize);
            this.pnlMain.Controls.Add(this.lblEndSendBufferSize);
            this.pnlMain.Controls.Add(this.numEndReceiveBufferSize);
            this.pnlMain.Controls.Add(this.lblEndReceiveBufferSize);
            this.pnlMain.Controls.Add(this.numStartMaxWindowSize);
            this.pnlMain.Controls.Add(this.lblStartMaxWindowSize);
            this.pnlMain.Controls.Add(this.numStartSendBufferSize);
            this.pnlMain.Controls.Add(this.lblStartSendBufferSize);
            this.pnlMain.Controls.Add(this.numStartReceiveBufferSize);
            this.pnlMain.Controls.Add(this.lblStartReceiveBufferSize);
            this.pnlMain.Controls.Add(this.numMaxBytes);
            this.pnlMain.Controls.Add(this.lblMaxBytes);
            this.pnlMain.Controls.Add(this.txtInterval);
            this.pnlMain.Controls.Add(this.numMaxPackets);
            this.pnlMain.Controls.Add(this.lblInterval);
            this.pnlMain.Controls.Add(this.lblMaxPackets);
            this.pnlMain.Location = new System.Drawing.Point(14, 240);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(319, 380);
            this.pnlMain.TabIndex = 47;
            // 
            // numEndMaxWindowSize
            // 
            this.numEndMaxWindowSize.Location = new System.Drawing.Point(81, 203);
            this.numEndMaxWindowSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numEndMaxWindowSize.Name = "numEndMaxWindowSize";
            this.numEndMaxWindowSize.Size = new System.Drawing.Size(238, 19);
            this.numEndMaxWindowSize.TabIndex = 60;
            // 
            // lblEndMaxWindowSize
            // 
            this.lblEndMaxWindowSize.AutoSize = true;
            this.lblEndMaxWindowSize.Location = new System.Drawing.Point(-2, 205);
            this.lblEndMaxWindowSize.Name = "lblEndMaxWindowSize";
            this.lblEndMaxWindowSize.Size = new System.Drawing.Size(116, 12);
            this.lblEndMaxWindowSize.TabIndex = 59;
            this.lblEndMaxWindowSize.Text = "End Max Window Size";
            // 
            // numEndSendBufferSize
            // 
            this.numEndSendBufferSize.Location = new System.Drawing.Point(81, 178);
            this.numEndSendBufferSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numEndSendBufferSize.Name = "numEndSendBufferSize";
            this.numEndSendBufferSize.Size = new System.Drawing.Size(238, 19);
            this.numEndSendBufferSize.TabIndex = 58;
            // 
            // lblEndSendBufferSize
            // 
            this.lblEndSendBufferSize.AutoSize = true;
            this.lblEndSendBufferSize.Location = new System.Drawing.Point(-2, 180);
            this.lblEndSendBufferSize.Name = "lblEndSendBufferSize";
            this.lblEndSendBufferSize.Size = new System.Drawing.Size(114, 12);
            this.lblEndSendBufferSize.TabIndex = 57;
            this.lblEndSendBufferSize.Text = "End Send Buffer Size";
            // 
            // numEndReceiveBufferSize
            // 
            this.numEndReceiveBufferSize.Location = new System.Drawing.Point(81, 153);
            this.numEndReceiveBufferSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numEndReceiveBufferSize.Name = "numEndReceiveBufferSize";
            this.numEndReceiveBufferSize.Size = new System.Drawing.Size(238, 19);
            this.numEndReceiveBufferSize.TabIndex = 56;
            // 
            // lblEndReceiveBufferSize
            // 
            this.lblEndReceiveBufferSize.AutoSize = true;
            this.lblEndReceiveBufferSize.Location = new System.Drawing.Point(-2, 155);
            this.lblEndReceiveBufferSize.Name = "lblEndReceiveBufferSize";
            this.lblEndReceiveBufferSize.Size = new System.Drawing.Size(130, 12);
            this.lblEndReceiveBufferSize.TabIndex = 55;
            this.lblEndReceiveBufferSize.Text = "End Receive Buffer Size";
            // 
            // numStartMaxWindowSize
            // 
            this.numStartMaxWindowSize.Location = new System.Drawing.Point(81, 128);
            this.numStartMaxWindowSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numStartMaxWindowSize.Name = "numStartMaxWindowSize";
            this.numStartMaxWindowSize.Size = new System.Drawing.Size(238, 19);
            this.numStartMaxWindowSize.TabIndex = 54;
            // 
            // lblStartMaxWindowSize
            // 
            this.lblStartMaxWindowSize.AutoSize = true;
            this.lblStartMaxWindowSize.Location = new System.Drawing.Point(-2, 130);
            this.lblStartMaxWindowSize.Name = "lblStartMaxWindowSize";
            this.lblStartMaxWindowSize.Size = new System.Drawing.Size(122, 12);
            this.lblStartMaxWindowSize.TabIndex = 53;
            this.lblStartMaxWindowSize.Text = "Start Max Window Size";
            // 
            // numStartSendBufferSize
            // 
            this.numStartSendBufferSize.Location = new System.Drawing.Point(81, 103);
            this.numStartSendBufferSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numStartSendBufferSize.Name = "numStartSendBufferSize";
            this.numStartSendBufferSize.Size = new System.Drawing.Size(238, 19);
            this.numStartSendBufferSize.TabIndex = 52;
            // 
            // lblStartSendBufferSize
            // 
            this.lblStartSendBufferSize.AutoSize = true;
            this.lblStartSendBufferSize.Location = new System.Drawing.Point(-2, 105);
            this.lblStartSendBufferSize.Name = "lblStartSendBufferSize";
            this.lblStartSendBufferSize.Size = new System.Drawing.Size(120, 12);
            this.lblStartSendBufferSize.TabIndex = 51;
            this.lblStartSendBufferSize.Text = "Start Send Buffer Size";
            // 
            // numStartReceiveBufferSize
            // 
            this.numStartReceiveBufferSize.Location = new System.Drawing.Point(81, 78);
            this.numStartReceiveBufferSize.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numStartReceiveBufferSize.Name = "numStartReceiveBufferSize";
            this.numStartReceiveBufferSize.Size = new System.Drawing.Size(238, 19);
            this.numStartReceiveBufferSize.TabIndex = 50;
            // 
            // lblStartReceiveBufferSize
            // 
            this.lblStartReceiveBufferSize.AutoSize = true;
            this.lblStartReceiveBufferSize.Location = new System.Drawing.Point(-2, 80);
            this.lblStartReceiveBufferSize.Name = "lblStartReceiveBufferSize";
            this.lblStartReceiveBufferSize.Size = new System.Drawing.Size(136, 12);
            this.lblStartReceiveBufferSize.TabIndex = 49;
            this.lblStartReceiveBufferSize.Text = "Start Receive Buffer Size";
            // 
            // numMaxBytes
            // 
            this.numMaxBytes.Location = new System.Drawing.Point(81, 53);
            this.numMaxBytes.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numMaxBytes.Name = "numMaxBytes";
            this.numMaxBytes.Size = new System.Drawing.Size(238, 19);
            this.numMaxBytes.TabIndex = 48;
            // 
            // lblMaxBytes
            // 
            this.lblMaxBytes.AutoSize = true;
            this.lblMaxBytes.Location = new System.Drawing.Point(-2, 55);
            this.lblMaxBytes.Name = "lblMaxBytes";
            this.lblMaxBytes.Size = new System.Drawing.Size(60, 12);
            this.lblMaxBytes.TabIndex = 47;
            this.lblMaxBytes.Text = "Max Bytes";
            // 
            // cmbStreamType
            // 
            this.cmbStreamType.FormattingEnabled = true;
            this.cmbStreamType.Location = new System.Drawing.Point(95, 214);
            this.cmbStreamType.Name = "cmbStreamType";
            this.cmbStreamType.Size = new System.Drawing.Size(238, 20);
            this.cmbStreamType.TabIndex = 49;
            this.cmbStreamType.SelectedIndexChanged += new System.EventHandler(this.cmbStreamType_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 220);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 12);
            this.label13.TabIndex = 48;
            this.label13.Text = "Stream Type";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 626);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(258, 626);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 51;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtCBRRate
            // 
            this.txtCBRRate.Location = new System.Drawing.Point(81, 228);
            this.txtCBRRate.Name = "txtCBRRate";
            this.txtCBRRate.Size = new System.Drawing.Size(238, 19);
            this.txtCBRRate.TabIndex = 62;
            // 
            // lblCBRRate
            // 
            this.lblCBRRate.AutoSize = true;
            this.lblCBRRate.Location = new System.Drawing.Point(-2, 231);
            this.lblCBRRate.Name = "lblCBRRate";
            this.lblCBRRate.Size = new System.Drawing.Size(75, 12);
            this.lblCBRRate.TabIndex = 61;
            this.lblCBRRate.Text = "On CBR Rate";
            // 
            // cmbOnDistribution
            // 
            this.cmbOnDistribution.FormattingEnabled = true;
            this.cmbOnDistribution.Location = new System.Drawing.Point(81, 253);
            this.cmbOnDistribution.Name = "cmbOnDistribution";
            this.cmbOnDistribution.Size = new System.Drawing.Size(238, 20);
            this.cmbOnDistribution.TabIndex = 64;
            // 
            // lblOnDistribution
            // 
            this.lblOnDistribution.AutoSize = true;
            this.lblOnDistribution.Location = new System.Drawing.Point(-2, 259);
            this.lblOnDistribution.Name = "lblOnDistribution";
            this.lblOnDistribution.Size = new System.Drawing.Size(82, 12);
            this.lblOnDistribution.TabIndex = 63;
            this.lblOnDistribution.Text = "On Distribution";
            // 
            // cmbOffDistribution
            // 
            this.cmbOffDistribution.FormattingEnabled = true;
            this.cmbOffDistribution.Location = new System.Drawing.Point(81, 304);
            this.cmbOffDistribution.Name = "cmbOffDistribution";
            this.cmbOffDistribution.Size = new System.Drawing.Size(238, 20);
            this.cmbOffDistribution.TabIndex = 66;
            // 
            // lblOffDistribution
            // 
            this.lblOffDistribution.AutoSize = true;
            this.lblOffDistribution.Location = new System.Drawing.Point(-2, 310);
            this.lblOffDistribution.Name = "lblOffDistribution";
            this.lblOffDistribution.Size = new System.Drawing.Size(84, 12);
            this.lblOffDistribution.TabIndex = 65;
            this.lblOffDistribution.Text = "Off Distribution";
            // 
            // cmbTransportProtocol
            // 
            this.cmbTransportProtocol.FormattingEnabled = true;
            this.cmbTransportProtocol.Location = new System.Drawing.Point(81, 355);
            this.cmbTransportProtocol.Name = "cmbTransportProtocol";
            this.cmbTransportProtocol.Size = new System.Drawing.Size(238, 20);
            this.cmbTransportProtocol.TabIndex = 68;
            // 
            // lblTransportProtocol
            // 
            this.lblTransportProtocol.AutoSize = true;
            this.lblTransportProtocol.Location = new System.Drawing.Point(-2, 361);
            this.lblTransportProtocol.Name = "lblTransportProtocol";
            this.lblTransportProtocol.Size = new System.Drawing.Size(100, 12);
            this.lblTransportProtocol.TabIndex = 67;
            this.lblTransportProtocol.Text = "Transport Protocol";
            // 
            // txtOnInterval
            // 
            this.txtOnInterval.Location = new System.Drawing.Point(81, 279);
            this.txtOnInterval.Name = "txtOnInterval";
            this.txtOnInterval.Size = new System.Drawing.Size(238, 19);
            this.txtOnInterval.TabIndex = 53;
            this.txtOnInterval.KeyPress += Apex.InputFiltering.FilterPositiveNumeric;
            // 
            // lblOnInterval
            // 
            this.lblOnInterval.AutoSize = true;
            this.lblOnInterval.Location = new System.Drawing.Point(-2, 282);
            this.lblOnInterval.Name = "lblOnInterval";
            this.lblOnInterval.Size = new System.Drawing.Size(61, 12);
            this.lblOnInterval.TabIndex = 52;
            this.lblOnInterval.Text = "On Interval";
            // 
            // txtOffInterval
            // 
            this.txtOffInterval.Location = new System.Drawing.Point(81, 330);
            this.txtOffInterval.Name = "txtOffInterval";
            this.txtOffInterval.Size = new System.Drawing.Size(238, 19);
            this.txtOffInterval.TabIndex = 70;
            this.txtOffInterval.KeyPress += Apex.InputFiltering.FilterPositiveNumeric;
            // 
            // lblOffInterval
            // 
            this.lblOffInterval.AutoSize = true;
            this.lblOffInterval.Location = new System.Drawing.Point(-2, 333);
            this.lblOffInterval.Name = "lblOffInterval";
            this.lblOffInterval.Size = new System.Drawing.Size(63, 12);
            this.lblOffInterval.TabIndex = 69;
            this.lblOffInterval.Text = "Off Interval";
            // 
            // StreamEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 656);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbStreamType);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.numPacketSize);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numPort);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.label7);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StreamEditor";
            this.Text = "LinkEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StreamEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPackets)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEndMaxWindowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndSendBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndReceiveBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartMaxWindowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartSendBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartReceiveBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBytes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEndNode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbStartNode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numPacketSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numMaxPackets;
        private System.Windows.Forms.Label lblMaxPackets;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.NumericUpDown numMaxBytes;
        private System.Windows.Forms.Label lblMaxBytes;
        private System.Windows.Forms.ComboBox cmbStreamType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numEndMaxWindowSize;
        private System.Windows.Forms.Label lblEndMaxWindowSize;
        private System.Windows.Forms.NumericUpDown numEndSendBufferSize;
        private System.Windows.Forms.Label lblEndSendBufferSize;
        private System.Windows.Forms.NumericUpDown numEndReceiveBufferSize;
        private System.Windows.Forms.Label lblEndReceiveBufferSize;
        private System.Windows.Forms.NumericUpDown numStartMaxWindowSize;
        private System.Windows.Forms.Label lblStartMaxWindowSize;
        private System.Windows.Forms.NumericUpDown numStartSendBufferSize;
        private System.Windows.Forms.Label lblStartSendBufferSize;
        private System.Windows.Forms.NumericUpDown numStartReceiveBufferSize;
        private System.Windows.Forms.Label lblStartReceiveBufferSize;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtCBRRate;
        private System.Windows.Forms.Label lblCBRRate;
        private System.Windows.Forms.ComboBox cmbTransportProtocol;
        private System.Windows.Forms.Label lblTransportProtocol;
        private System.Windows.Forms.ComboBox cmbOffDistribution;
        private System.Windows.Forms.Label lblOffDistribution;
        private System.Windows.Forms.ComboBox cmbOnDistribution;
        private System.Windows.Forms.Label lblOnDistribution;
        private System.Windows.Forms.TextBox txtOffInterval;
        private System.Windows.Forms.Label lblOffInterval;
        private System.Windows.Forms.TextBox txtOnInterval;
        private System.Windows.Forms.Label lblOnInterval;
    }
}