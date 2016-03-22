namespace NetML
{
    partial class NodeEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbNodeType = new System.Windows.Forms.ComboBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblSchedulerType = new System.Windows.Forms.Label();
            this.lblWifiStandard = new System.Windows.Forms.Label();
            this.lblWifiMode = new System.Windows.Forms.Label();
            this.cmbSchedulerType = new System.Windows.Forms.ComboBox();
            this.cmbWifiStandard = new System.Windows.Forms.ComboBox();
            this.cmbWifiMode = new System.Windows.Forms.ComboBox();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.txtDataRate = new System.Windows.Forms.TextBox();
            this.lblDataRate = new System.Windows.Forms.Label();
            this.txtBaseAddress = new System.Windows.Forms.TextBox();
            this.lblBaseAddress = new System.Windows.Forms.Label();
            this.pnlMobility = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numYMax = new System.Windows.Forms.NumericUpDown();
            this.numXMax = new System.Windows.Forms.NumericUpDown();
            this.numYMin = new System.Windows.Forms.NumericUpDown();
            this.numXMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMobilityModel = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlMobility.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXMin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(53, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 19);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(53, 37);
            this.numX.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(128, 19);
            this.numX.TabIndex = 2;
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
            this.numY.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Type";
            // 
            // cmbNodeType
            // 
            this.cmbNodeType.FormattingEnabled = true;
            this.cmbNodeType.Location = new System.Drawing.Point(53, 62);
            this.cmbNodeType.Name = "cmbNodeType";
            this.cmbNodeType.Size = new System.Drawing.Size(280, 20);
            this.cmbNodeType.TabIndex = 7;
            this.cmbNodeType.SelectedIndexChanged += new System.EventHandler(this.cmbNodeType_SelectedIndexChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblSchedulerType);
            this.pnlMain.Controls.Add(this.lblWifiStandard);
            this.pnlMain.Controls.Add(this.lblWifiMode);
            this.pnlMain.Controls.Add(this.cmbSchedulerType);
            this.pnlMain.Controls.Add(this.cmbWifiStandard);
            this.pnlMain.Controls.Add(this.cmbWifiMode);
            this.pnlMain.Controls.Add(this.txtDelay);
            this.pnlMain.Controls.Add(this.lblDelay);
            this.pnlMain.Controls.Add(this.txtDataRate);
            this.pnlMain.Controls.Add(this.lblDataRate);
            this.pnlMain.Controls.Add(this.txtBaseAddress);
            this.pnlMain.Controls.Add(this.lblBaseAddress);
            this.pnlMain.Location = new System.Drawing.Point(12, 88);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(321, 152);
            this.pnlMain.TabIndex = 8;
            // 
            // lblSchedulerType
            // 
            this.lblSchedulerType.AutoSize = true;
            this.lblSchedulerType.Location = new System.Drawing.Point(3, 132);
            this.lblSchedulerType.Name = "lblSchedulerType";
            this.lblSchedulerType.Size = new System.Drawing.Size(84, 12);
            this.lblSchedulerType.TabIndex = 11;
            this.lblSchedulerType.Text = "Scheduler Type";
            // 
            // lblWifiStandard
            // 
            this.lblWifiStandard.AutoSize = true;
            this.lblWifiStandard.Location = new System.Drawing.Point(3, 107);
            this.lblWifiStandard.Name = "lblWifiStandard";
            this.lblWifiStandard.Size = new System.Drawing.Size(73, 12);
            this.lblWifiStandard.TabIndex = 10;
            this.lblWifiStandard.Text = "Wifi Standard";
            // 
            // lblWifiMode
            // 
            this.lblWifiMode.AutoSize = true;
            this.lblWifiMode.Location = new System.Drawing.Point(3, 81);
            this.lblWifiMode.Name = "lblWifiMode";
            this.lblWifiMode.Size = new System.Drawing.Size(55, 12);
            this.lblWifiMode.TabIndex = 9;
            this.lblWifiMode.Text = "Wifi Mode";
            // 
            // cmbSchedulerType
            // 
            this.cmbSchedulerType.FormattingEnabled = true;
            this.cmbSchedulerType.Location = new System.Drawing.Point(86, 129);
            this.cmbSchedulerType.Name = "cmbSchedulerType";
            this.cmbSchedulerType.Size = new System.Drawing.Size(203, 20);
            this.cmbSchedulerType.TabIndex = 8;
            // 
            // cmbWifiStandard
            // 
            this.cmbWifiStandard.FormattingEnabled = true;
            this.cmbWifiStandard.Location = new System.Drawing.Point(86, 104);
            this.cmbWifiStandard.Name = "cmbWifiStandard";
            this.cmbWifiStandard.Size = new System.Drawing.Size(203, 20);
            this.cmbWifiStandard.TabIndex = 7;
            // 
            // cmbWifiMode
            // 
            this.cmbWifiMode.FormattingEnabled = true;
            this.cmbWifiMode.Location = new System.Drawing.Point(86, 78);
            this.cmbWifiMode.Name = "cmbWifiMode";
            this.cmbWifiMode.Size = new System.Drawing.Size(203, 20);
            this.cmbWifiMode.TabIndex = 6;
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(86, 53);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(203, 19);
            this.txtDelay.TabIndex = 5;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(3, 56);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(34, 12);
            this.lblDelay.TabIndex = 4;
            this.lblDelay.Text = "Delay";
            // 
            // txtDataRate
            // 
            this.txtDataRate.Location = new System.Drawing.Point(86, 28);
            this.txtDataRate.Name = "txtDataRate";
            this.txtDataRate.Size = new System.Drawing.Size(203, 19);
            this.txtDataRate.TabIndex = 3;
            // 
            // lblDataRate
            // 
            this.lblDataRate.AutoSize = true;
            this.lblDataRate.Location = new System.Drawing.Point(3, 31);
            this.lblDataRate.Name = "lblDataRate";
            this.lblDataRate.Size = new System.Drawing.Size(57, 12);
            this.lblDataRate.TabIndex = 2;
            this.lblDataRate.Text = "Data Rate";
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Location = new System.Drawing.Point(86, 3);
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Size = new System.Drawing.Size(203, 19);
            this.txtBaseAddress.TabIndex = 1;
            // 
            // lblBaseAddress
            // 
            this.lblBaseAddress.AutoSize = true;
            this.lblBaseAddress.Location = new System.Drawing.Point(3, 6);
            this.lblBaseAddress.Name = "lblBaseAddress";
            this.lblBaseAddress.Size = new System.Drawing.Size(77, 12);
            this.lblBaseAddress.TabIndex = 0;
            this.lblBaseAddress.Text = "Base Address";
            // 
            // pnlMobility
            // 
            this.pnlMobility.Controls.Add(this.label9);
            this.pnlMobility.Controls.Add(this.label8);
            this.pnlMobility.Controls.Add(this.label7);
            this.pnlMobility.Controls.Add(this.label6);
            this.pnlMobility.Controls.Add(this.numYMax);
            this.pnlMobility.Controls.Add(this.numXMax);
            this.pnlMobility.Controls.Add(this.numYMin);
            this.pnlMobility.Controls.Add(this.numXMin);
            this.pnlMobility.Controls.Add(this.label5);
            this.pnlMobility.Controls.Add(this.cmbMobilityModel);
            this.pnlMobility.Location = new System.Drawing.Point(12, 246);
            this.pnlMobility.Name = "pnlMobility";
            this.pnlMobility.Size = new System.Drawing.Size(321, 88);
            this.pnlMobility.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "Max";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(82, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "Min";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "X";
            // 
            // numYMax
            // 
            this.numYMax.Location = new System.Drawing.Point(173, 66);
            this.numYMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numYMax.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numYMax.Name = "numYMax";
            this.numYMax.Size = new System.Drawing.Size(145, 19);
            this.numYMax.TabIndex = 10;
            // 
            // numXMax
            // 
            this.numXMax.Location = new System.Drawing.Point(173, 41);
            this.numXMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numXMax.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numXMax.Name = "numXMax";
            this.numXMax.Size = new System.Drawing.Size(145, 19);
            this.numXMax.TabIndex = 9;
            // 
            // numYMin
            // 
            this.numYMin.Location = new System.Drawing.Point(21, 66);
            this.numYMin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numYMin.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numYMin.Name = "numYMin";
            this.numYMin.Size = new System.Drawing.Size(145, 19);
            this.numYMin.TabIndex = 8;
            // 
            // numXMin
            // 
            this.numXMin.Location = new System.Drawing.Point(21, 41);
            this.numXMin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numXMin.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numXMin.Name = "numXMin";
            this.numXMin.Size = new System.Drawing.Size(145, 19);
            this.numXMin.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "Mobility Model";
            // 
            // cmbMobilityModel
            // 
            this.cmbMobilityModel.FormattingEnabled = true;
            this.cmbMobilityModel.Location = new System.Drawing.Point(88, 3);
            this.cmbMobilityModel.Name = "cmbMobilityModel";
            this.cmbMobilityModel.Size = new System.Drawing.Size(230, 20);
            this.cmbMobilityModel.TabIndex = 0;
            this.cmbMobilityModel.SelectedIndexChanged += new System.EventHandler(this.cmbMobilityModel_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(258, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // NodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 374);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlMobility);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.cmbNodeType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NodeEditor";
            this.Text = "NodeEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NodeEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlMobility.ResumeLayout(false);
            this.pnlMobility.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbNodeType;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlMobility;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numYMax;
        private System.Windows.Forms.NumericUpDown numXMax;
        private System.Windows.Forms.NumericUpDown numYMin;
        private System.Windows.Forms.NumericUpDown numXMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMobilityModel;
        private System.Windows.Forms.TextBox txtBaseAddress;
        private System.Windows.Forms.Label lblBaseAddress;
        private System.Windows.Forms.ComboBox cmbSchedulerType;
        private System.Windows.Forms.ComboBox cmbWifiStandard;
        private System.Windows.Forms.ComboBox cmbWifiMode;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.TextBox txtDataRate;
        private System.Windows.Forms.Label lblDataRate;
        private System.Windows.Forms.Label lblSchedulerType;
        private System.Windows.Forms.Label lblWifiStandard;
        private System.Windows.Forms.Label lblWifiMode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}