using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class TraceEditor : Form
    {
        new private Traces Parent;
        new private LayoutEngine Layout;

        private Trace TraceLink;
        private Trace CurrentTrace;

        public TraceEditor(Traces Parent, Trace Trace)
        {
            InitializeComponent();

            this.Parent = Parent;
            Layout = new LayoutEngine(this)
            {
                ClearOnProcess = true,
                Padding = new Padding(6),
                Margin = new Padding(6)
            };
            LoadTrace(Trace);
        }

        public void LoadTrace(Trace Trace, bool New = true)
        {
            // Don't update link if the trace was updated by this form.
            if (New)
            {
                // Clone object and keep a reference to the original so we can quit without saving.
                TraceLink = Trace;
                CurrentTrace = new Trace(Trace);
                Trace = CurrentTrace;
            }

            txtName.Text = Trace.Name;
            txtStartTime.Text = Trace.StartTime.ToString();
            txtEndTime.Text = Trace.EndTime.ToString();
            chkStartTime.Checked = Trace.CommonStartTime;
            chkEndTime.Checked = Trace.CommonEndTime;

            if (Trace.Attributes == null)
            {
                Trace.Attributes = new List<TraceAttribute>();
            }

            if (Trace.Attributes.Count == 0)
            {
                AddAttribute(Trace);
            }

            Layout.ClearLayout();

            using (Layout.BeginRow())
            {
                Layout.AddControl(lblName);
                Layout.AddControl(txtName);
            }

            using (Layout.BeginRow())
            {
                Layout.AddControl(chkStartTime);
                Layout.AddControl(txtStartTime);
            }

            using (Layout.BeginRow())
            {
                Layout.AddControl(chkEndTime);
                Layout.AddControl(txtEndTime);
            }

            using (Layout.BeginPanel(pnlAttributes))
            {
                var index = 0;
                foreach (var attribute in Trace.Attributes)
                {
                    using (Layout.BeginGroupBox(new GroupBox { Text = $"Attribute {++index}" }))
                    {
                        var attributeCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                        var elementCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                        var code = new TextBox
                        {
                            Text = attribute.Code,
                            Enabled = attribute.IncrementMode == TraceAttribute.Increment.Custom
                        };
                        var deleteButton = new Button { Text = "Delete Attribute" };
                        var evaluationCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };

                        #region "Element Source"
                        elementCombo.Items.AddRange(Parent.Parent.Items.ToArray());
                        elementCombo.SelectedItem = attribute.Element;
                        elementCombo.SelectedValueChanged += (object sender, EventArgs e) =>
                        {
                            attribute.Element = (IDrawable)elementCombo.SelectedItem;
                            LoadTrace(Trace, false);
                        };

                        if (attribute.Element is Node || attribute.Element is Domain)
                        {
                            attributeCombo.Items.AddRange(new string[] { "MacTx", "MacTxDrop", "MacRx", "PhyTxEnd", "PhyTxDrop", "PhyRxEnd", "PhyRxDrop", "Tx", "Rx", "SendOutgoing", "UnicastForward", "LocalDeliver", "Enqueue", "Dequeue", "Drop" });
                        }
                        else if (attribute.Element is Link)
                        {
                            attributeCombo.Items.AddRange(new string[] { "MacTx", "MacTxDrop", "MacRx", "PhyTxBegin", "PhyTxEnd", "PhyTxDrop", "PhyRxEnd", "PhyRxDrop", "Enqueue", "Dequeue", "Drop" });
                        }
                        else if (attribute.Element is Stream)
                        {
                            attributeCombo.Items.AddRange(new string[] { "Tx", "Rx", "CongestionWindow" });
                        }
                        attributeCombo.SelectedItem = attribute.TraceSource;

                        // Element type has changed the old source no longer exists.
                        if (attributeCombo.SelectedIndex == -1)
                        {
                            attributeCombo.SelectedIndex = 0;
                            attribute.TraceSource = attributeCombo.SelectedItem as string;
                        }

                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(new Label { Text = "Element Source" });
                            Layout.AddControl(elementCombo);

                        }
                        #endregion

                        if (attribute.Element is Stream)
                        {
                            Layout.AddControl(new Label { Text = $"Direction: {(attribute.Element as Stream).StartNode.Text} -> {(attribute.Element as Stream).EndNode.Text}" });
                        }

                        if (attribute.Element is Link)
                        {
                            var start = attribute.LinkReverse ? (attribute.Element as Link).EndNode : (attribute.Element as Link).StartNode;
                            var end = attribute.LinkReverse ? (attribute.Element as Link).StartNode : (attribute.Element as Link).EndNode;

                            var direction = new Label { Text = $"Direction: {start.Text} -> {end.Text}" };
                            var reverse = new CheckBox
                            {
                                Text = "Reverse",
                                Checked = attribute.LinkReverse
                            };
                            reverse.CheckedChanged += (object sender, EventArgs e) =>
                            {
                                attribute.LinkReverse = reverse.Checked;
                                direction.Text = $"Direction: {(reverse.Checked ? end.Text : start.Text)} -> {(reverse.Checked ? start.Text : end.Text)}";
                            };

                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(direction);
                                Layout.AddControl(reverse);
                            }
                        }

                        #region "Attribute Source"
                        attributeCombo.SelectedValueChanged += (object sender, EventArgs e) =>
                        {
                            attribute.TraceSource = attributeCombo.SelectedItem as string;
                            code.Text = attribute.Code;
                        };
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(new Label { Text = "Attribute Source" });
                            Layout.AddControl(attributeCombo);
                        }
                        #endregion

                        #region "Delete Row"
                        using (Layout.BeginRow())
                        {
                            deleteButton.Click += (object sender, EventArgs e) => { DeleteAttribute(Trace, attribute); };
                            Layout.AddControl(new Label { Text = "Evaluation Code" });
                            Layout.AddControl(deleteButton);
                        }
                        #endregion


                        #region "Evaluation"
                        code.TextChanged += (object sender, EventArgs e) =>
                        {
                            if ((sender as TextBox).Enabled)
                            {
                                attribute.Code = (sender as TextBox).Text;
                            }
                        };

                        evaluationCombo.Items.AddRange(new object[] { TraceAttribute.Increment.PacketIncrement, TraceAttribute.Increment.PacketDecrement, TraceAttribute.Increment.PacketSizeIncrement, TraceAttribute.Increment.PacketSizeDecrement, TraceAttribute.Increment.Custom });
                        evaluationCombo.SelectedItem = attribute.IncrementMode;
                        evaluationCombo.SelectedValueChanged += (object sender, EventArgs e) =>
                        {
                            attribute.IncrementMode = (TraceAttribute.Increment)evaluationCombo.SelectedItem;
                            code.Enabled = attribute.IncrementMode == TraceAttribute.Increment.Custom;
                            code.Text = attribute.Code;
                        };

                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(evaluationCombo);
                            Layout.AddControl(code);
                        }
                        #endregion
                    }
                }
            }

            var btnAddAttribute = new Button { Text = "Add attribute" };
            btnAddAttribute.Click += (object sender, EventArgs e) =>
            {
                AddAttribute(Trace);
                LoadTrace(Trace, false);
            };
            Layout.AddControl(btnAddAttribute);

            using (Layout.BeginRow())
            {
                Layout.AddControl(btnSave);
                Layout.AddControl(btnDelete);
            }

            Layout.ProcessLayout();

            // Shrink attributes panel so it doesn't go off the bottom of the form.
            if (btnSave.Bottom > ClientSize.Height)
            {
                pnlAttributes.Height -= (btnSave.Bottom - ClientSize.Height);
                pnlAttributes.Left -= 8;
                pnlAttributes.Width += 16;
                btnAddAttribute.Top = pnlAttributes.Bottom + 6;
                btnSave.Top = btnAddAttribute.Bottom + 6;
                btnDelete.Top = btnAddAttribute.Bottom + 6;
                Apex.ControlUtil.ShowScrollBar(pnlAttributes.Handle, Apex.ControlUtil.ScrollBarDirection.Horizontal, false);
            }
        }

        private void AddAttribute(Trace Trace)
        {
            var nodes = Parent.Parent.Nodes;
            if (nodes.Count() > 0)
            {
                Trace.Attributes.Add(new TraceAttribute
                {
                    Parent = Trace,
                    IncrementMode = TraceAttribute.Increment.PacketSizeIncrement,
                    TraceSource = "Rx",
                    Element = nodes.ElementAt(0)
                });
            }
            else
            {
                MessageBox.Show("Cannot add a trace attribute with no nodes in the network.");
            }
        }

        private void DeleteAttribute(Trace Trace, TraceAttribute Attribute)
        {
            Trace.Attributes.Remove(Attribute);
            LoadTrace(Trace, false);
        }

        private void TraceEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.EditorClosed();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.Text = txtName.Text;
            CurrentTrace.Name = txtName.Text;
            LoadTrace(CurrentTrace, false);
            txtName.Focus();
            txtName.SelectionStart = txtName.Text.Length;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TraceLink.Set(CurrentTrace);
            Parent.RefreshTraces();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Parent.DeleteTrace(TraceLink);
            this.Close();
        }

        private void txtStartTime_TextChanged(object sender, EventArgs e)
        {
            if (txtStartTime.Enabled)
            {
                CurrentTrace.StartTime = float.Parse(txtStartTime.Text);
            }
        }

        private void txtEndTime_TextChanged(object sender, EventArgs e)
        {
            if (txtEndTime.Enabled)
            {
                CurrentTrace.EndTime = float.Parse(txtEndTime.Text);
            }
        }

        private void chkStartTime_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTrace.CommonStartTime = chkStartTime.Checked;
            txtStartTime.Enabled = !chkStartTime.Checked;
            txtStartTime.Text = CurrentTrace.StartTime.ToString();
        }

        private void chkEndTime_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTrace.CommonEndTime = chkEndTime.Checked;
            txtEndTime.Enabled = !chkEndTime.Checked;
            txtEndTime.Text = CurrentTrace.EndTime.ToString();
        }

        private void TraceEditor_ResizeEnd(object sender, EventArgs e)
        {
            LoadTrace(CurrentTrace, false);
        }
    }
}
