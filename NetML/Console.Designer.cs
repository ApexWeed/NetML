namespace NetML
{
    partial class Console
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomRight = new System.Windows.Forms.Panel();
            this.pnlBottomLeft = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtEntry = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.pnlBottom.SuspendLayout();
            this.pnlBottomRight.SuspendLayout();
            this.pnlBottomLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlBottomLeft);
            this.pnlBottom.Controls.Add(this.pnlBottomRight);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 501);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(848, 29);
            this.pnlBottom.TabIndex = 0;
            // 
            // pnlBottomRight
            // 
            this.pnlBottomRight.Controls.Add(this.btnSend);
            this.pnlBottomRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBottomRight.Location = new System.Drawing.Point(767, 0);
            this.pnlBottomRight.Name = "pnlBottomRight";
            this.pnlBottomRight.Size = new System.Drawing.Size(81, 29);
            this.pnlBottomRight.TabIndex = 0;
            // 
            // pnlBottomLeft
            // 
            this.pnlBottomLeft.Controls.Add(this.txtEntry);
            this.pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomLeft.Name = "pnlBottomLeft";
            this.pnlBottomLeft.Padding = new System.Windows.Forms.Padding(5);
            this.pnlBottomLeft.Size = new System.Drawing.Size(767, 29);
            this.pnlBottomLeft.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(3, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtEntry
            // 
            this.txtEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEntry.Location = new System.Drawing.Point(5, 5);
            this.txtEntry.Name = "txtEntry";
            this.txtEntry.Size = new System.Drawing.Size(757, 19);
            this.txtEntry.TabIndex = 0;
            this.txtEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEntry_KeyDown);
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(848, 501);
            this.txtOutput.TabIndex = 1;
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 530);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.pnlBottom);
            this.Name = "Console";
            this.Text = "Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Console_FormClosing);
            this.Load += new System.EventHandler(this.Console_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottomRight.ResumeLayout(false);
            this.pnlBottomLeft.ResumeLayout(false);
            this.pnlBottomLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomLeft;
        private System.Windows.Forms.TextBox txtEntry;
        private System.Windows.Forms.Panel pnlBottomRight;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtOutput;
    }
}