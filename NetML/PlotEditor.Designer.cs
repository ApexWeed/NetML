namespace NetML
{
    partial class PlotEditor
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblLegend = new System.Windows.Forms.Label();
            this.cmbLegend = new System.Windows.Forms.ComboBox();
            this.pnlWidth = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.pnlHeight = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtXAxis = new System.Windows.Forms.TextBox();
            this.lblXAxis = new System.Windows.Forms.Label();
            this.txtYAxis = new System.Windows.Forms.TextBox();
            this.lblYAxis = new System.Windows.Forms.Label();
            this.txtY2Axis = new System.Windows.Forms.TextBox();
            this.lblY2Axis = new System.Windows.Forms.Label();
            this.chkDisabled = new System.Windows.Forms.CheckBox();
            this.pnlAttributes = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlWidth.SuspendLayout();
            this.pnlHeight.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(34, 12);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(270, 19);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblLegend
            // 
            this.lblLegend.AutoSize = true;
            this.lblLegend.Location = new System.Drawing.Point(14, 40);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(88, 12);
            this.lblLegend.TabIndex = 2;
            this.lblLegend.Text = "Legend Location";
            // 
            // cmbLegend
            // 
            this.cmbLegend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLegend.FormattingEnabled = true;
            this.cmbLegend.Location = new System.Drawing.Point(110, 37);
            this.cmbLegend.Name = "cmbLegend";
            this.cmbLegend.Size = new System.Drawing.Size(270, 20);
            this.cmbLegend.TabIndex = 3;
            this.cmbLegend.SelectedIndexChanged += new System.EventHandler(this.cmbLegend_SelectedIndexChanged);
            // 
            // pnlWidth
            // 
            this.pnlWidth.Controls.Add(this.label1);
            this.pnlWidth.Controls.Add(this.txtWidth);
            this.pnlWidth.Location = new System.Drawing.Point(16, 63);
            this.pnlWidth.Name = "pnlWidth";
            this.pnlWidth.Size = new System.Drawing.Size(179, 19);
            this.pnlWidth.TabIndex = 4;
            this.pnlWidth.SizeChanged += new System.EventHandler(this.pnlWidth_SizeChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(42, 0);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(137, 19);
            this.txtWidth.TabIndex = 0;
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Apex.InputFiltering.FilterInteger);
            // 
            // pnlHeight
            // 
            this.pnlHeight.Controls.Add(this.label2);
            this.pnlHeight.Controls.Add(this.txtHeight);
            this.pnlHeight.Location = new System.Drawing.Point(201, 63);
            this.pnlHeight.Name = "pnlHeight";
            this.pnlHeight.Size = new System.Drawing.Size(179, 19);
            this.pnlHeight.TabIndex = 5;
            this.pnlHeight.SizeChanged += new System.EventHandler(this.pnlHeight_SizeChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(47, 0);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(132, 19);
            this.txtHeight.TabIndex = 0;
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Apex.InputFiltering.FilterInteger);
            // 
            // txtXAxis
            // 
            this.txtXAxis.Location = new System.Drawing.Point(110, 88);
            this.txtXAxis.Name = "txtXAxis";
            this.txtXAxis.Size = new System.Drawing.Size(270, 19);
            this.txtXAxis.TabIndex = 7;
            this.txtXAxis.TextChanged += new System.EventHandler(this.txtXAxis_TextChanged);
            // 
            // lblXAxis
            // 
            this.lblXAxis.AutoSize = true;
            this.lblXAxis.Location = new System.Drawing.Point(13, 91);
            this.lblXAxis.Name = "lblXAxis";
            this.lblXAxis.Size = new System.Drawing.Size(70, 12);
            this.lblXAxis.TabIndex = 6;
            this.lblXAxis.Text = "X Axis Label";
            // 
            // txtYAxis
            // 
            this.txtYAxis.Location = new System.Drawing.Point(110, 113);
            this.txtYAxis.Name = "txtYAxis";
            this.txtYAxis.Size = new System.Drawing.Size(270, 19);
            this.txtYAxis.TabIndex = 9;
            this.txtYAxis.TextChanged += new System.EventHandler(this.txtYAxis_TextChanged);
            // 
            // lblYAxis
            // 
            this.lblYAxis.AutoSize = true;
            this.lblYAxis.Location = new System.Drawing.Point(13, 116);
            this.lblYAxis.Name = "lblYAxis";
            this.lblYAxis.Size = new System.Drawing.Size(70, 12);
            this.lblYAxis.TabIndex = 8;
            this.lblYAxis.Text = "Y Axis Label";
            // 
            // txtY2Axis
            // 
            this.txtY2Axis.Location = new System.Drawing.Point(110, 138);
            this.txtY2Axis.Name = "txtY2Axis";
            this.txtY2Axis.Size = new System.Drawing.Size(270, 19);
            this.txtY2Axis.TabIndex = 11;
            this.txtY2Axis.TextChanged += new System.EventHandler(this.txtY2Axis_TextChanged);
            // 
            // lblY2Axis
            // 
            this.lblY2Axis.AutoSize = true;
            this.lblY2Axis.Location = new System.Drawing.Point(13, 141);
            this.lblY2Axis.Name = "lblY2Axis";
            this.lblY2Axis.Size = new System.Drawing.Size(76, 12);
            this.lblY2Axis.TabIndex = 10;
            this.lblY2Axis.Text = "Y2 Axis Label";
            // 
            // chkDisabled
            // 
            this.chkDisabled.AutoSize = true;
            this.chkDisabled.Location = new System.Drawing.Point(12, 163);
            this.chkDisabled.Name = "chkDisabled";
            this.chkDisabled.Size = new System.Drawing.Size(68, 16);
            this.chkDisabled.TabIndex = 12;
            this.chkDisabled.Text = "Disabled";
            this.chkDisabled.UseVisualStyleBackColor = true;
            this.chkDisabled.CheckedChanged += new System.EventHandler(this.chkDisabled_CheckedChanged);
            // 
            // pnlAttributes
            // 
            this.pnlAttributes.AutoScroll = true;
            this.pnlAttributes.Location = new System.Drawing.Point(12, 185);
            this.pnlAttributes.Name = "pnlAttributes";
            this.pnlAttributes.Size = new System.Drawing.Size(200, 100);
            this.pnlAttributes.TabIndex = 13;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(309, 229);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(228, 229);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PlotEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 480);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlAttributes);
            this.Controls.Add(this.chkDisabled);
            this.Controls.Add(this.txtY2Axis);
            this.Controls.Add(this.lblY2Axis);
            this.Controls.Add(this.txtYAxis);
            this.Controls.Add(this.lblYAxis);
            this.Controls.Add(this.txtXAxis);
            this.Controls.Add(this.lblXAxis);
            this.Controls.Add(this.pnlHeight);
            this.Controls.Add(this.pnlWidth);
            this.Controls.Add(this.cmbLegend);
            this.Controls.Add(this.lblLegend);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "PlotEditor";
            this.Text = "PlotEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlotEditor_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.PlotEditor_ResizeEnd);
            this.pnlWidth.ResumeLayout(false);
            this.pnlWidth.PerformLayout();
            this.pnlHeight.ResumeLayout(false);
            this.pnlHeight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.ComboBox cmbLegend;
        private System.Windows.Forms.Panel pnlWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Panel pnlHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtXAxis;
        private System.Windows.Forms.Label lblXAxis;
        private System.Windows.Forms.TextBox txtYAxis;
        private System.Windows.Forms.Label lblYAxis;
        private System.Windows.Forms.TextBox txtY2Axis;
        private System.Windows.Forms.Label lblY2Axis;
        private System.Windows.Forms.CheckBox chkDisabled;
        private System.Windows.Forms.Panel pnlAttributes;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
    }
}