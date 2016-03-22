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
            Layout = new LayoutEngine(pnlAttributes, Apex.Layout.LayoutEngine.ContainerType.Panel)
            {
                ClearOnProcess = true
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

            if (Trace.Attributes == null)
            {
                Trace.Attributes = new List<TraceAttribute>();
            }

            if (Trace.Attributes.Count == 0)
            {
                AddAttribute(Trace);
            }

            Layout.ClearLayout();
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
                    elementCombo.SelectedValueChanged += (object sender, EventArgs e) =>
                    {
                        attribute.Element = (IDrawable)elementCombo.SelectedItem;
                        attributeCombo.Items.Clear();
                        if (attribute.Element is Node)
                        {
                            attributeCombo.Items.AddRange(new string[] { "MaxTx", "MaxTxDrop", "PhyTxEnd", "PhyTxDrop", "MaxRx", "PhyRxEnd", "PhyRxDrop", "Tx", "Rx", "SendOutgoing", "UnicastForward", "LocalDeliver" });
                        }
                        else if (attribute.Element is Link)
                        {
                            attributeCombo.Items.AddRange(new string[] { "MaxTx", "MaxTxDrop", "PhyTxEnd", "PhyTxDrop", "Enqueue", "Dequeue", "Drop" });
                        }
                        else if (attribute.Element is Stream)
                        {
                            attributeCombo.Items.AddRange(new string[] { "Tx", "Rx" });
                        }
                        attributeCombo.SelectedItem = attribute.TraceSource;

                        // Element type has changed the old source no longer exists.
                        if (attributeCombo.SelectedIndex == -1)
                        {
                            attributeCombo.SelectedIndex = 0;
                            attribute.TraceSource = attributeCombo.SelectedItem as string;
                        }
                    };
                    elementCombo.SelectedItem = attribute.Element;
                    using (Layout.BeginRow())
                    {
                        Layout.AddControl(new Label { Text = "Element Source" });
                        Layout.AddControl(elementCombo);
                        
                    }
                    #endregion


                    //if (attribute.Element is Node)
                    //{
                    //    attributeCombo.Items.AddRange(new string[] { "MaxTx", "MaxTxDrop", "PhyTxEnd", "PhyTxDrop", "MaxRx", "PhyRxEnd", "PhyRxDrop", "Tx", "Rx", "SendOutgoing", "UnicastForward", "LocalDeliver" });
                    //}
                    //else if (attribute.Element is Link)
                    //{
                    //    attributeCombo.Items.AddRange(new string[] { "MaxTx", "MaxTxDrop", "PhyTxEnd", "PhyTxDrop", "Enqueue", "Dequeue", "Drop" });
                    //}
                    //else if (attribute.Element is Stream)
                    //{
                    //    attributeCombo.Items.AddRange(new string[] { "Tx", "Rx" });
                    //}
                    //attributeCombo.SelectedItem = attribute.TraceSource;
                    #region "Attribute Source"
                    attributeCombo.SelectedValueChanged += (object sender, EventArgs e) => { attribute.TraceSource = attributeCombo.SelectedItem as string; };
                    using (Layout.BeginRow())
                    {
                        Layout.AddControl(new Label { Text = "Attribute Source" });
                        Layout.AddControl(attributeCombo);
                    }
                    #endregion

                    #region "Delete Row"
                    using (Layout.BeginRow())
                    {
                        deleteButton.Click += (object sender, EventArgs e) => { DeleteAttribute(Trace, index - 1); };
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

            var addAttributeButton = new Button { Text = "Add attribute" };
            addAttributeButton.Click += (object sender, EventArgs e) =>
            {
                AddAttribute(Trace);
                LoadTrace(Trace, false);
            };
            Layout.AddControl(addAttributeButton);
            Layout.ProcessLayout();
        }

        private void AddAttribute(Trace Trace)
        {
            var nodes = Parent.Parent.Nodes;
            if (nodes.Count() > 0)
            {
                Trace.Attributes.Add(new TraceAttribute
                {
                    Parent = Trace,
                    AttributeType = "Ptr<const Packet> pkt",
                    IncrementMode = TraceAttribute.Increment.PacketSizeIncrement,
                    TraceSource = "Rx",
                    Element = nodes.ElementAt(0)
                });
            }
            else
            {
                MessageBox.Show("Cannot add an attribute with no nodes in the network.");
            }
        }

        private void DeleteAttribute(Trace Trace, int Index)
        {
            Trace.Attributes.RemoveAt(Index);
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

        private void FilterNumeric(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtStartTime_TextChanged(object sender, EventArgs e)
        {
            CurrentTrace.StartTime = float.Parse(txtStartTime.Text);
        }

        private void txtEndTime_TextChanged(object sender, EventArgs e)
        {
            CurrentTrace.EndTime = float.Parse(txtEndTime.Text);
        }
    }
}
