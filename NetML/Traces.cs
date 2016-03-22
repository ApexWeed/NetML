using System;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class Traces : Form
    {
        new public MainForm Parent
        {
            get;
            private set;
        }

        new private LayoutEngine Layout;
        private TraceEditor Editor;

        public Traces(MainForm Parent)
        {
            InitializeComponent();

            this.Parent = Parent;
            Layout = new LayoutEngine(pnlMain, Apex.Layout.LayoutEngine.ContainerType.Panel)
            {
                ClearOnProcess = true,
            };
        }

        private void Traces_Load(object sender, EventArgs e)
        {
            RefreshTraces();
        }

        public void RefreshTraces()
        {
            Layout.ClearLayout();
            foreach (var trace in Parent.NetworkParameters.Traces)
            {
                using (Layout.BeginGroupBox(new GroupBox { Text = trace.Name }))
                {
                    if (trace.Attributes != null)
                    {
                        foreach (var traceAttribute in trace.Attributes)
                        {
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label() { Text = traceAttribute.ToString() });
                            }
                        }
                    }
                    using (Layout.BeginRow())
                    {
                        var editButton = new Button() { Text = "Edit" };
                        editButton.Click += (object sender, EventArgs e) => { EditTrace(trace); };
                        var deleteButton = new Button() { Text = "Delete" };
                        deleteButton.Click += (object sender, EventArgs e) => { DeleteTrace(trace); };
                        Layout.AddControl(editButton);
                        Layout.AddControl(deleteButton);
                    }
                }
            }

            Layout.ProcessLayout();
        }

        public void EditorClosed()
        {
            RefreshTraces();
            Editor = null;
        }

        public void DeleteTrace(Trace Trace)
        {
            Parent.NetworkParameters.Traces.Remove(Trace);
            RefreshTraces();
        }

        private void EditTrace(Trace Trace)
        {
            if (Editor == null)
            {
                Editor = new TraceEditor(this, Trace);
                Editor.Show();
            }
            else
            {
                Editor.LoadTrace(Trace);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTraces();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var trace = new Trace
            {
                Name = "wow",
                StartTime = 0.1f,
                EndTime = 1.0f
            };
            Parent.NetworkParameters.Traces.Add(trace);
            RefreshTraces();
            EditTrace(trace);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete every trace?", "Confirm Clear") == DialogResult.OK)
            {
                Parent.NetworkParameters.Traces.Clear();
                RefreshTraces();
            }
        }

        private void Traces_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Close();
            }
            Parent.ChildClosing(this);
        }
    }
}
