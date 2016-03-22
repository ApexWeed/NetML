using System.Linq;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class StreamEditor : Form
    {
        new private LayoutEngine Layout;
        new private MainForm Parent;
        private Stream StreamLink;
        private bool Loading;

        public StreamEditor(MainForm Parent, Stream Stream)
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
            cmbStreamType.Items.AddRange(new object[] { Stream.StreamType.BulkFTP, Stream.StreamType.UDPPing });

            LoadStream(Stream);
        }

        public void LoadStream(Stream Stream)
        {
            Loading = true;
            StreamLink = Stream;

            // Nodes.
            cmbStartNode.Items.Clear();
            cmbEndNode.Items.Clear();
            cmbStartNode.Items.AddRange(Parent.Nodes.ToArray());
            cmbEndNode.Items.AddRange(Parent.Nodes.ToArray());
            cmbStartNode.SelectedItem = Stream.StartNode;
            cmbEndNode.SelectedItem = Stream.EndNode;

            // Generic fields.
            txtName.Text = Stream.Name;
            numX.Value = (decimal)Stream.X;
            numY.Value = (decimal)Stream.Y;
            txtStartTime.Text = Stream.StartTime.ToString();
            txtEndTime.Text = Stream.EndTime.ToString();
            numPort.Value = Stream.Type == Stream.StreamType.BulkFTP ? Stream.FTPPort : Stream.UDPPort;
            numPacketSize.Value = Stream.PacketSize;
            cmbStreamType.SelectedItem = Stream.Type;
            numStartReceiveBufferSize.Value = Stream.StartReceiveBufferSize;
            numEndReceiveBufferSize.Value = Stream.EndReceiveBufferSize;

            // UDP fields.
            numMaxPackets.Value = Stream.MaxPackets;
            txtInterval.Text = Stream.Interval.ToString();

            // FTP fields.
            numMaxBytes.Value = Stream.MaxBytes;
            numStartSendBufferSize.Value = Stream.StartSendBufferSize;
            numEndSendBufferSize.Value = Stream.EndSendBufferSize;
            numStartMaxWindowSize.Value = Stream.StartMaxWindowSize;
            numEndMaxWindowSize.Value = Stream.EndMaxWindowSize;

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

            if ((Stream.StreamType)cmbStreamType.SelectedItem == Stream.StreamType.BulkFTP)
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
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblInterval);
                    Layout.AddControl(txtInterval);
                }
            }

            using (Layout.BeginRow())
            {
                Layout.AddControl(lblStartReceiveBufferSize);
                Layout.AddControl(numStartReceiveBufferSize);
            }
            if ((Stream.StreamType)cmbStreamType.SelectedItem == Stream.StreamType.BulkFTP)
            {
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblStartSendBufferSize);
                    Layout.AddControl(numStartSendBufferSize);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblStartMaxWindowSize);
                    Layout.AddControl(numStartMaxWindowSize);
                }
            }

            using (Layout.BeginRow())
            {
                Layout.AddControl(lblEndReceiveBufferSize);
                Layout.AddControl(numEndReceiveBufferSize);
            }
            if ((Stream.StreamType)cmbStreamType.SelectedItem == Stream.StreamType.BulkFTP)
            {
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblEndSendBufferSize);
                    Layout.AddControl(numEndSendBufferSize);
                }
                using (Layout.BeginRow())
                {
                    Layout.AddControl(lblEndMaxWindowSize);
                    Layout.AddControl(numEndMaxWindowSize);
                }
            }

            Layout.ProcessLayout();

            btnSave.Top = pnlMain.Bottom + 6;
            btnDelete.Top = pnlMain.Bottom + 6;
            this.Height = btnSave.Top + 73;
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

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // Nodes.
            StreamLink.StartNode = (Node)cmbStartNode.SelectedItem;
            StreamLink.EndNode = (Node)cmbEndNode.SelectedItem;

            // Generic fields.
            StreamLink.Name = txtName.Text;
            StreamLink.X = (float)numX.Value;
            StreamLink.Y = (float)numY.Value;
            StreamLink.StartTime = float.Parse(txtStartTime.Text);
            StreamLink.EndTime = float.Parse(txtEndTime.Text);
            if (StreamLink.Type == Stream.StreamType.BulkFTP)
            {
                StreamLink.FTPPort = (int)numPort.Value;
            }
            else
            {
                StreamLink.UDPPort = (int)numPort.Value;
            }
            StreamLink.PacketSize = (int)numPacketSize.Value;
            StreamLink.Type = (Stream.StreamType)cmbStreamType.SelectedItem;
            StreamLink.StartReceiveBufferSize = (int)numStartReceiveBufferSize.Value;
            StreamLink.EndReceiveBufferSize = (int)numEndReceiveBufferSize.Value;

            // UDP fields.
            StreamLink.MaxPackets = (int)numMaxPackets.Value;
            StreamLink.Interval = float.Parse(txtInterval.Text);

            // FTP fields.
            StreamLink.MaxBytes = (int)numMaxBytes.Value;
            StreamLink.StartSendBufferSize = (int)numStartSendBufferSize.Value;
            StreamLink.EndSendBufferSize = (int)numEndSendBufferSize.Value;
            StreamLink.StartMaxWindowSize = (int)numStartMaxWindowSize.Value;
            StreamLink.EndMaxWindowSize = (int)numEndMaxWindowSize.Value;

            Parent.RefreshCanvas();
            this.Close();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            Parent.DeleteElement(StreamLink);
            this.Close();
        }

        private void StreamEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }

        private void cmbStreamType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateLayout();
        }

        private void txtName_TextChanged(object sender, System.EventArgs e)
        {
            this.Text = txtName.Text;
        }
    }
}
