using System;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class NodeEditor : Form
    {
        new private MainForm Parent;

        private Node NodeLink;

        public NodeEditor(MainForm Parent, Node Node)
        {
            InitializeComponent();
            this.Parent = Parent;

            LoadNode(Node);
        }

        public void LoadNode(Node Node)
        {
            NodeLink = Node;

            txtName.Text = NodeLink.Name;
            numX.Value = (decimal)NodeLink.X;
            numY.Value = (decimal)NodeLink.Y;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NodeLink.Name = txtName.Text;
            NodeLink.X = (float)numX.Value;
            NodeLink.Y = (float)numY.Value;

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

        private void NodeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }
    }
}
