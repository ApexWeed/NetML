namespace NetML
{
    partial class DisplayPropertiesEditor
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
            this.cmbNodeDisplayMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLinkDisplayMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStreamDisplayMode = new System.Windows.Forms.ComboBox();
            this.chkRenderNode = new System.Windows.Forms.CheckBox();
            this.chkRenderNodeText = new System.Windows.Forms.CheckBox();
            this.chkRenderLinkText = new System.Windows.Forms.CheckBox();
            this.chkRenderLink = new System.Windows.Forms.CheckBox();
            this.chkRenderStreamText = new System.Windows.Forms.CheckBox();
            this.chkRenderStream = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbNodeDisplayMode
            // 
            this.cmbNodeDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNodeDisplayMode.FormattingEnabled = true;
            this.cmbNodeDisplayMode.Location = new System.Drawing.Point(144, 12);
            this.cmbNodeDisplayMode.Name = "cmbNodeDisplayMode";
            this.cmbNodeDisplayMode.Size = new System.Drawing.Size(296, 20);
            this.cmbNodeDisplayMode.TabIndex = 0;
            this.cmbNodeDisplayMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Node Display Mode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Link Display Mode";
            // 
            // cmbLinkDisplayMode
            // 
            this.cmbLinkDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinkDisplayMode.FormattingEnabled = true;
            this.cmbLinkDisplayMode.Location = new System.Drawing.Point(144, 38);
            this.cmbLinkDisplayMode.Name = "cmbLinkDisplayMode";
            this.cmbLinkDisplayMode.Size = new System.Drawing.Size(296, 20);
            this.cmbLinkDisplayMode.TabIndex = 2;
            this.cmbLinkDisplayMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stream Display Mode";
            // 
            // cmbStreamDisplayMode
            // 
            this.cmbStreamDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStreamDisplayMode.FormattingEnabled = true;
            this.cmbStreamDisplayMode.Location = new System.Drawing.Point(144, 64);
            this.cmbStreamDisplayMode.Name = "cmbStreamDisplayMode";
            this.cmbStreamDisplayMode.Size = new System.Drawing.Size(296, 20);
            this.cmbStreamDisplayMode.TabIndex = 4;
            this.cmbStreamDisplayMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxChanged);
            // 
            // chkRenderNode
            // 
            this.chkRenderNode.AutoSize = true;
            this.chkRenderNode.Location = new System.Drawing.Point(14, 90);
            this.chkRenderNode.Name = "chkRenderNode";
            this.chkRenderNode.Size = new System.Drawing.Size(90, 16);
            this.chkRenderNode.TabIndex = 6;
            this.chkRenderNode.Text = "Render Node";
            this.chkRenderNode.UseVisualStyleBackColor = true;
            this.chkRenderNode.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // chkRenderNodeText
            // 
            this.chkRenderNodeText.AutoSize = true;
            this.chkRenderNodeText.Location = new System.Drawing.Point(223, 90);
            this.chkRenderNodeText.Name = "chkRenderNodeText";
            this.chkRenderNodeText.Size = new System.Drawing.Size(117, 16);
            this.chkRenderNodeText.TabIndex = 7;
            this.chkRenderNodeText.Text = "Render Node Text";
            this.chkRenderNodeText.UseVisualStyleBackColor = true;
            this.chkRenderNodeText.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // chkRenderLinkText
            // 
            this.chkRenderLinkText.AutoSize = true;
            this.chkRenderLinkText.Location = new System.Drawing.Point(223, 112);
            this.chkRenderLinkText.Name = "chkRenderLinkText";
            this.chkRenderLinkText.Size = new System.Drawing.Size(112, 16);
            this.chkRenderLinkText.TabIndex = 9;
            this.chkRenderLinkText.Text = "Render Link Text";
            this.chkRenderLinkText.UseVisualStyleBackColor = true;
            this.chkRenderLinkText.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // chkRenderLink
            // 
            this.chkRenderLink.AutoSize = true;
            this.chkRenderLink.Location = new System.Drawing.Point(14, 112);
            this.chkRenderLink.Name = "chkRenderLink";
            this.chkRenderLink.Size = new System.Drawing.Size(85, 16);
            this.chkRenderLink.TabIndex = 8;
            this.chkRenderLink.Text = "Render Link";
            this.chkRenderLink.UseVisualStyleBackColor = true;
            this.chkRenderLink.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // chkRenderStreamText
            // 
            this.chkRenderStreamText.AutoSize = true;
            this.chkRenderStreamText.Location = new System.Drawing.Point(223, 134);
            this.chkRenderStreamText.Name = "chkRenderStreamText";
            this.chkRenderStreamText.Size = new System.Drawing.Size(127, 16);
            this.chkRenderStreamText.TabIndex = 11;
            this.chkRenderStreamText.Text = "Render Stream Text";
            this.chkRenderStreamText.UseVisualStyleBackColor = true;
            this.chkRenderStreamText.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // chkRenderStream
            // 
            this.chkRenderStream.AutoSize = true;
            this.chkRenderStream.Location = new System.Drawing.Point(14, 134);
            this.chkRenderStream.Name = "chkRenderStream";
            this.chkRenderStream.Size = new System.Drawing.Size(100, 16);
            this.chkRenderStream.TabIndex = 10;
            this.chkRenderStream.Text = "Render Stream";
            this.chkRenderStream.UseVisualStyleBackColor = true;
            this.chkRenderStream.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // DisplayPropertiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 163);
            this.Controls.Add(this.chkRenderStreamText);
            this.Controls.Add(this.chkRenderStream);
            this.Controls.Add(this.chkRenderLinkText);
            this.Controls.Add(this.chkRenderLink);
            this.Controls.Add(this.chkRenderNodeText);
            this.Controls.Add(this.chkRenderNode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbStreamDisplayMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLinkDisplayMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbNodeDisplayMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DisplayPropertiesEditor";
            this.Text = "Display Properties Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisplayPropertiesEditor_FormClosing);
            this.Load += new System.EventHandler(this.DisplayPropertiesEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbNodeDisplayMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLinkDisplayMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStreamDisplayMode;
        private System.Windows.Forms.CheckBox chkRenderNode;
        private System.Windows.Forms.CheckBox chkRenderNodeText;
        private System.Windows.Forms.CheckBox chkRenderLinkText;
        private System.Windows.Forms.CheckBox chkRenderLink;
        private System.Windows.Forms.CheckBox chkRenderStreamText;
        private System.Windows.Forms.CheckBox chkRenderStream;
    }
}