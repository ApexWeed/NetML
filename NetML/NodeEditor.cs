using System;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class NodeEditor : Form
    {
        new private LayoutEngine Layout;
        new private MainForm Parent;

        private Node NodeLink;
        private bool Loading;

        public NodeEditor(MainForm Parent, Node Node)
        {
            InitializeComponent();
            this.Parent = Parent;
            Layout = new LayoutEngine(pnlMain)
            {
                ClearOnProcess = true,
                UpdateContainerSize = true,
                Margin = new Padding(0, 6, 0, 0),
                Padding = new Padding(6)
            };

            // Fill comboboxes.
            cmbMobilityModel.Items.AddRange(new object[] { Node.MobilityModel.FixedPosition, Node.MobilityModel.RandomWalk });
            cmbNodeType.Items.AddRange(new object[] { Node.NodeType.Node, Node.NodeType.CSMA, Node.NodeType.IEEE81211, Node.NodeType.LTE, Node.NodeType.Wimax });
            cmbSchedulerType.Items.AddRange(new object[] { Node.SchedulerType.Simple, Node.SchedulerType.MBQOS, Node.SchedulerType.RTPS });
            cmbWifiMode.Items.AddRange(new object[] { Node.WifiMode.Infrastructure, Node.WifiMode.AdHoc });
            cmbWifiStandard.Items.AddRange(new object[] { Node.WifiStandard.IEEE80211a, Node.WifiStandard.IEEE80211b, Node.WifiStandard.IEEE80211g, Node.WifiStandard.IEEE80211n24G, Node.WifiStandard.IEEE80211n5G });

            LoadNode(Node);
        }

        public void LoadNode(Node Node)
        {
            Loading = true;
            NodeLink = Node;

            txtName.Text = NodeLink.Name;
            numX.Value = (decimal)NodeLink.X;
            numY.Value = (decimal)NodeLink.Y;
            cmbNodeType.SelectedItem = NodeLink.Type;

            txtBaseAddress.Text = NodeLink.BaseAddress;
            txtDataRate.Text = NodeLink.DataRate;
            txtDelay.Text = NodeLink.Delay;
            cmbWifiStandard.SelectedItem = NodeLink.Standard;
            cmbWifiMode.SelectedItem = NodeLink.Mode;
            cmbSchedulerType.SelectedItem = NodeLink.Scheduler;

            cmbMobilityModel.SelectedItem = NodeLink.Model;
            numXMin.Value = NodeLink.XMin;
            numXMin.Value = NodeLink.YMin;
            numXMin.Value = NodeLink.XMax;
            numXMin.Value = NodeLink.YMax;

            Loading = false;
            UpdateLayout();
        }

        private void cmbNodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (Loading)
            {
                // Stop node type combo box from updating controls while values are being set.
                return;
            }

            Layout.ClearLayout();

            switch ((Node.NodeType)cmbNodeType.SelectedItem)
            {
                case Node.NodeType.Node:
                    {
                        break;
                    }
                case Node.NodeType.CSMA:
                    {
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblBaseAddress);
                            Layout.AddControl(txtBaseAddress);
                        }
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblDataRate);
                            Layout.AddControl(txtDataRate);
                        }
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblDelay);
                            Layout.AddControl(txtDelay);
                        }
                        break;
                    }
                case Node.NodeType.IEEE81211:
                    {
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblBaseAddress);
                            Layout.AddControl(txtBaseAddress);
                        }
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblWifiStandard);
                            Layout.AddControl(cmbWifiStandard);
                        }
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblWifiMode);
                            Layout.AddControl(cmbWifiMode);
                        }
                        break;
                    }
                case Node.NodeType.LTE:
                    {
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblBaseAddress);
                            Layout.AddControl(txtBaseAddress);
                        }
                        break;
                    }
                case Node.NodeType.Wimax:
                    {
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblBaseAddress);
                            Layout.AddControl(txtBaseAddress);
                        }
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(lblSchedulerType);
                            Layout.AddControl(cmbSchedulerType);
                        }
                        break;
                    }
            }

            Layout.ProcessLayout();

            // Toggle visibility of mobility panel.
            if ((Node.NodeType)cmbNodeType.SelectedItem == Node.NodeType.Node)
            {
                pnlMobility.Visible = false;
                btnDelete.Top = cmbNodeType.Bottom + 6;
                btnSave.Top = cmbNodeType.Bottom + 6;
            }
            else if ((Node.NodeType)cmbNodeType.SelectedItem == Node.NodeType.CSMA)
            {
                pnlMobility.Visible = false;
                btnDelete.Top = pnlMain.Bottom + 6;
                btnSave.Top = pnlMain.Bottom + 6;
            }
            else
            {
                pnlMobility.Visible = true;
                pnlMobility.Top = pnlMain.Bottom + 6;
                btnDelete.Top = pnlMobility.Bottom + 6;
                btnSave.Top = pnlMobility.Bottom + 6;
            }
            this.Height = btnSave.Top + 73;
        }

        private void cmbMobilityModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mobilityBoxesEnabled = ((Node.MobilityModel)cmbMobilityModel.SelectedItem) == Node.MobilityModel.RandomWalk;

            numYMin.Enabled = mobilityBoxesEnabled;
            numXMin.Enabled = mobilityBoxesEnabled;
            numYMax.Enabled = mobilityBoxesEnabled;
            numXMax.Enabled = mobilityBoxesEnabled;
        }

        private void NodeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NodeLink.Name = txtName.Text;
            NodeLink.X = (float)numX.Value;
            NodeLink.Y = (float)numY.Value;
            NodeLink.Type = (Node.NodeType)cmbNodeType.SelectedItem;

            NodeLink.BaseAddress = txtBaseAddress.Text;
            NodeLink.DataRate = txtDataRate.Text;
            NodeLink.Delay = txtDelay.Text;
            NodeLink.Standard = (Node.WifiStandard)cmbWifiStandard.SelectedItem;
            NodeLink.Mode = (Node.WifiMode)cmbWifiMode.SelectedItem;
            NodeLink.Scheduler = (Node.SchedulerType)cmbSchedulerType.SelectedItem;

            NodeLink.Model = (Node.MobilityModel)cmbMobilityModel.SelectedItem;
            NodeLink.XMin = (int)numXMin.Value;
            NodeLink.YMin = (int)numYMin.Value;
            NodeLink.XMax = (int)numXMax.Value;
            NodeLink.YMax = (int)numYMax.Value;

            Parent.RefreshCanvas();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Parent.DeleteElement(NodeLink);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.Text = txtName.Text;
        }
    }
}
