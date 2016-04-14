using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class DomainEditor : Form
    {
        new private MainForm Parent;
        private LayoutEngine MainLayout;
        private LayoutEngine NodeLayout;
        private Domain DomainLink;
        private bool Loading;

        public DomainEditor(MainForm Parent, Domain Domain)
        {
            InitializeComponent();
            this.Parent = Parent;
            MainLayout = new LayoutEngine(pnlMain)
            {
                ClearOnProcess = true,
                UpdateContainerSize = true,
                Margin = new Padding(0, 6, 0, 0),
                Padding = new Padding(6)
            };
            NodeLayout = new LayoutEngine(pnlNodes)
            {
                ClearOnProcess = true,
                Margin = new Padding(0, 6, 0, 0),
                Padding = new Padding(6)
            };

            // Fill comboboxes.
            cmbMobilityModel.Items.AddRange(Apex.EnumUtil.GetValuesCombo<Domain.MobilityModel>());
            cmbDomainType.Items.AddRange(Apex.EnumUtil.GetValuesCombo<Domain.DomainType>());
            cmbSchedulerType.Items.AddRange(Apex.EnumUtil.GetValuesCombo<Domain.SchedulerType>());
            cmbWifiMode.Items.AddRange(Apex.EnumUtil.GetValuesCombo<Domain.WifiMode>());
            cmbWifiStandard.Items.AddRange(Apex.EnumUtil.GetValuesCombo<Domain.WifiStandard>());

            LoadDomain(Domain);
        }

        public void LoadDomain(Domain Domain)
        {
            Loading = true;
            DomainLink = Domain;

            txtName.Text = DomainLink.Name;
            numX.Value = (decimal)DomainLink.X;
            numY.Value = (decimal)DomainLink.Y;
            cmbDomainType.SelectedItem = DomainLink.Type;

            txtBaseAddress.Text = DomainLink.BaseAddress;
            txtDataRate.Text = DomainLink.DataRate;
            txtDelay.Text = DomainLink.Delay;
            cmbWifiStandard.SelectedItem = DomainLink.Standard;
            cmbWifiMode.SelectedItem = DomainLink.Mode;
            cmbSchedulerType.SelectedItem = DomainLink.Scheduler;

            cmbMobilityModel.SelectedItem = DomainLink.Model;
            numXMin.Value = DomainLink.XMin;
            numYMin.Value = DomainLink.YMin;
            numXMax.Value = DomainLink.XMax;
            numYMax.Value = DomainLink.YMax;

            Loading = false;
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            UpdateNodeLayout();
            UpdateMainLayout();
        }

        private void UpdateMainLayout()
        {
            if (Loading)
            {
                // Stop Domain type combo box from updating controls while values are being set.
                return;
            }

            MainLayout.ClearLayout();

            switch ((Domain.DomainType)cmbDomainType.SelectedItem)
            {
                case Domain.DomainType.CSMA:
                    {
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblBaseAddress);
                            MainLayout.AddControl(txtBaseAddress);
                        }
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblDataRate);
                            MainLayout.AddControl(txtDataRate);
                        }
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblDelay);
                            MainLayout.AddControl(txtDelay);
                        }
                        break;
                    }
                case Domain.DomainType.IEEE81211:
                    {
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblBaseAddress);
                            MainLayout.AddControl(txtBaseAddress);
                        }
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblWifiStandard);
                            MainLayout.AddControl(cmbWifiStandard);
                        }
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblWifiMode);
                            MainLayout.AddControl(cmbWifiMode);
                        }
                        break;
                    }
                case Domain.DomainType.LTE:
                    {
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblBaseAddress);
                            MainLayout.AddControl(txtBaseAddress);
                        }
                        break;
                    }
                case Domain.DomainType.Wimax:
                    {
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblBaseAddress);
                            MainLayout.AddControl(txtBaseAddress);
                        }
                        using (MainLayout.BeginRow())
                        {
                            MainLayout.AddControl(lblSchedulerType);
                            MainLayout.AddControl(cmbSchedulerType);
                        }
                        break;
                    }
            }

            MainLayout.ProcessLayout();

            UpdateSize();
        }

        private void UpdateNodeLayout()
        {
            NodeLayout.ClearLayout();

            while (pnlNodes.Controls.Count > 0)
            {
                pnlNodes.Controls[0].Dispose();
                pnlNodes.Controls.RemoveAt(0);
            }

            var oddNode = true;
            foreach (var node in Parent.Nodes)
            {
                if (oddNode)
                {
                    NodeLayout.BeginRow();
                }

                var checkBox = new CheckBox
                {
                    Text = node.Text,
                    Checked = DomainLink.Nodes.Contains(node),
                    Tag = node
                };
                NodeLayout.AddControl(checkBox);

                if (!oddNode)
                {
                    NodeLayout.EndRow();
                }
                oddNode = !oddNode;
            }

            if (!oddNode)
            {
                NodeLayout.EndRow();
            }

            NodeLayout.ProcessLayout();

            Apex.ControlUtil.ShowScrollBar(pnlNodes.Handle, Apex.ControlUtil.ScrollBarDirection.Horizontal, false);
        }

        private void UpdateSize()
        {
            // Toggle visibility of mobility panel.
            if ((Domain.DomainType)cmbDomainType.SelectedItem == Domain.DomainType.CSMA)
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

        private void cmbDomainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainLayout();
        }

        private void cmbMobilityModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mobilityBoxesEnabled = ((Node.MobilityModel)cmbMobilityModel.SelectedItem) == Node.MobilityModel.RandomWalk;

            numYMin.Enabled = mobilityBoxesEnabled;
            numXMin.Enabled = mobilityBoxesEnabled;
            numYMax.Enabled = mobilityBoxesEnabled;
            numXMax.Enabled = mobilityBoxesEnabled;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DomainLink.Name = txtName.Text;
            DomainLink.X = (float)numX.Value;
            DomainLink.Y = (float)numY.Value;
            DomainLink.Type = (Domain.DomainType)cmbDomainType.SelectedItem;

            DomainLink.BaseAddress = txtBaseAddress.Text;
            DomainLink.DataRate = txtDataRate.Text;
            DomainLink.Delay = txtDelay.Text;
            DomainLink.Standard = (Domain.WifiStandard)cmbWifiStandard.SelectedItem;
            DomainLink.Mode = (Domain.WifiMode)cmbWifiMode.SelectedItem;
            DomainLink.Scheduler = (Domain.SchedulerType)cmbSchedulerType.SelectedItem;

            DomainLink.Model = (Domain.MobilityModel)cmbMobilityModel.SelectedItem;
            DomainLink.XMin = (int)numXMin.Value;
            DomainLink.YMin = (int)numYMin.Value;
            DomainLink.XMax = (int)numXMax.Value;
            DomainLink.YMax = (int)numYMax.Value;

            foreach (CheckBox control in pnlNodes.Controls)
            {
                var node = (Node)control.Tag;
                if (control.Checked)
                {
                    if (!DomainLink.Nodes.Contains(node))
                    {
                        DomainLink.Nodes.Add(node);
                    }
                }
                else
                {
                    if (DomainLink.Nodes.Contains(node))
                    {
                        DomainLink.Nodes.Remove(node);
                    }
                }
            }

            Parent.RefreshCanvas();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Parent.DeleteElement(DomainLink);
        }

        private void DomainEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }
    }
}
