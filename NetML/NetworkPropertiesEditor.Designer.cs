namespace NetML
{
    partial class NetworkPropertiesEditor
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObservationStartTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObservationStopTime = new System.Windows.Forms.TextBox();
            this.chkPrintAttributes = new System.Windows.Forms.CheckBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAsciiTrace = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(276, 19);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Observation Start Time";
            // 
            // txtObservationStartTime
            // 
            this.txtObservationStartTime.Location = new System.Drawing.Point(140, 37);
            this.txtObservationStartTime.Name = "txtObservationStartTime";
            this.txtObservationStartTime.Size = new System.Drawing.Size(276, 19);
            this.txtObservationStartTime.TabIndex = 2;
            this.txtObservationStartTime.TextChanged += new System.EventHandler(this.txtObservationStartTime_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Observation Stop Time";
            // 
            // txtObservationStopTime
            // 
            this.txtObservationStopTime.Location = new System.Drawing.Point(140, 62);
            this.txtObservationStopTime.Name = "txtObservationStopTime";
            this.txtObservationStopTime.Size = new System.Drawing.Size(276, 19);
            this.txtObservationStopTime.TabIndex = 4;
            this.txtObservationStopTime.TextChanged += new System.EventHandler(this.txtObservationStopTime_TextChanged);
            // 
            // chkPrintAttributes
            // 
            this.chkPrintAttributes.AutoSize = true;
            this.chkPrintAttributes.Location = new System.Drawing.Point(12, 87);
            this.chkPrintAttributes.Name = "chkPrintAttributes";
            this.chkPrintAttributes.Size = new System.Drawing.Size(108, 16);
            this.chkPrintAttributes.TabIndex = 6;
            this.chkPrintAttributes.Text = "Dump Attributes";
            this.chkPrintAttributes.UseVisualStyleBackColor = true;
            this.chkPrintAttributes.CheckedChanged += new System.EventHandler(this.chkPrintAttributes_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Location = new System.Drawing.Point(12, 109);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(404, 350);
            this.pnlMain.TabIndex = 7;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(177, 88);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New Module";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAsciiTrace
            // 
            this.chkAsciiTrace.AutoSize = true;
            this.chkAsciiTrace.Location = new System.Drawing.Point(200, 87);
            this.chkAsciiTrace.Name = "chkAsciiTrace";
            this.chkAsciiTrace.Size = new System.Drawing.Size(83, 16);
            this.chkAsciiTrace.TabIndex = 8;
            this.chkAsciiTrace.Text = "Ascii Trace";
            this.chkAsciiTrace.UseVisualStyleBackColor = true;
            this.chkAsciiTrace.CheckedChanged += new System.EventHandler(this.chkAsciiTrace_CheckedChanged);
            // 
            // NetworkPropertiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 471);
            this.Controls.Add(this.chkAsciiTrace);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.chkPrintAttributes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtObservationStopTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtObservationStartTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NetworkPropertiesEditor";
            this.Text = "Network Properties Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkPropertiesEditor_FormClosing);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtObservationStartTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObservationStopTime;
        private System.Windows.Forms.CheckBox chkPrintAttributes;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAsciiTrace;
    }
}