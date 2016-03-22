using System;
using System.Linq;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class LinkEditor : Form
    {
        new private MainForm Parent;
        new private LayoutEngine Layout;
        private Link LinkLink;
        private bool Loading;

        public LinkEditor(MainForm Parent, Link Link)
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

            // Load comboboxes.
            cmbQueueType.Items.AddRange(new object[] { Link.QueueType.DropTailQueue, Link.QueueType.RandomEarlyDiscard });
            cmbPacketMode.Items.AddRange(new object[] { Link.LinkMode.Bytes, Link.LinkMode.Packets });

            LoadLink(Link);
        }

        public void LoadLink(Link Link)
        {
            Loading = true;
            LinkLink = Link;

            // Nodes.
            cmbStartNode.Items.Clear();
            cmbEndNode.Items.Clear();
            cmbStartNode.Items.AddRange(Parent.Nodes.ToArray());
            cmbEndNode.Items.AddRange(Parent.Nodes.ToArray());
            cmbStartNode.SelectedItem = Link.StartNode;
            cmbEndNode.SelectedItem = Link.EndNode;

            // Generic fields.
            txtName.Text = Link.Name;
            txtBaseAddress.Text = Link.BaseAddress;
            txtDataRate.Text = Link.DataRate;
            txtDelay.Text = Link.Delay;
            numX.Value = (decimal)Link.X;
            numY.Value = (decimal)Link.Y;
            numMtu.Value = Link.Mtu;

            // Combo boxes.
            cmbQueueType.SelectedItem = Link.Queue;
            cmbPacketMode.SelectedItem = Link.Mode;
            chkDuplex.Checked = Link.Duplex;

            // Probably miscategorised values.
            numMaxBytes.Value = Link.MaxBytes;
            numMaxPackets.Value = Link.MaxPackets;

            // RED queue fields.
            numMeanPacketSize.Value = Link.MeanPacketSize;
            numIdlePacketSize.Value = Link.IdlePacketSize;
            numLinterm.Value = Link.Linterm;
            numMinTh.Value = Link.MinTh;
            numMaxTh.Value = Link.MaxTh;
            numQueueLimit.Value = Link.QueueLimit;
            numQW.Value = (decimal)Link.QW;
            txtLinkBandwidth.Text = Link.LinkBandwidth;
            txtLinkDelay.Text = Link.LinkDelay;
            chkGentle.Checked = Link.Gentle;
            chkWait.Checked = Link.Wait;

            Loading = false;
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (Loading)
            {
                return;
            }

            Layout.ClearLayout();

            if ((Link.LinkMode)cmbPacketMode.SelectedItem == Link.LinkMode.Bytes)
            {
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblMaxBytes);
                    Layout.AddControl(numMaxBytes);
                }
            }
            else
            {
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblMaxPackets);
                    Layout.AddControl(numMaxPackets);
                }
            }

            if ((Link.QueueType)cmbQueueType.SelectedItem == Link.QueueType.RandomEarlyDiscard)
            {
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblMeanPacketSize);
                    Layout.AddControl(numMeanPacketSize);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblIdlePacketSize);
                    Layout.AddControl(numIdlePacketSize);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(chkGentle);
                    Layout.AddControl(chkWait);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblQW);
                    Layout.AddControl(numQW);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblLinterm);
                    Layout.AddControl(numLinterm);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblLinkBandwidth);
                    Layout.AddControl(txtLinkBandwidth);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblLinkDelay);
                    Layout.AddControl(txtLinkDelay);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblMinTh);
                    Layout.AddControl(numMinTh);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblMaxTh);
                    Layout.AddControl(numMaxTh);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblQueueLimit);
                    Layout.AddControl(numQueueLimit);
                }
            }

            Layout.ProcessLayout();

            btnDelete.Top = pnlMain.Bottom + 6;
            btnSave.Top = pnlMain.Bottom + 6;
            this.Height = btnSave.Top + 73;
        }

        private void cmbPacketMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLayout();
        }

        private void cmbQueueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLayout();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Nodes.
            LinkLink.StartNode = (Node)cmbStartNode.SelectedItem;
            LinkLink.EndNode = (Node)cmbEndNode.SelectedItem;

            // Generic fields.
            LinkLink.Name = txtName.Text;
            LinkLink.BaseAddress = txtBaseAddress.Text;
            LinkLink.DataRate = txtDataRate.Text;
            LinkLink.Delay = txtDelay.Text;
            LinkLink.X = (float)numX.Value;
            LinkLink.Y = (float)numY.Value;
            LinkLink.Mtu = (int)numMtu.Value;

            // Combo boxes.
            LinkLink.Queue = (Link.QueueType)cmbQueueType.SelectedItem;
            LinkLink.Mode = (Link.LinkMode)cmbPacketMode.SelectedItem;
            LinkLink.Duplex = chkDuplex.Checked;

            // Probably miscategorised values.
            LinkLink.MaxBytes = (int)numMaxBytes.Value;
            LinkLink.MaxPackets = (int)numMaxPackets.Value;

            // RED queue fields.
            LinkLink.MeanPacketSize = (int)numMeanPacketSize.Value;
            LinkLink.IdlePacketSize = (int)numIdlePacketSize.Value;
            LinkLink.Linterm = (int)numLinterm.Value;
            LinkLink.MinTh = (int)numMinTh.Value;
            LinkLink.MaxTh = (int)numMaxTh.Value;
            LinkLink.QueueLimit = (int)numQueueLimit.Value;
            LinkLink.QW = (int)numQW.Value;
            LinkLink.LinkBandwidth = txtLinkBandwidth.Text;
            LinkLink.LinkDelay = txtLinkDelay.Text;
            LinkLink.Gentle = chkGentle.Checked;
            LinkLink.Wait = chkWait.Checked;

            Parent.RefreshCanvas();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Parent.DeleteElement(LinkLink);
            this.Close();
        }

        private void LinkEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.Text = txtName.Text;
        }
    }
}
