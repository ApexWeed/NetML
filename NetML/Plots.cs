using System;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class Plots : Form
    {
        new public MainForm Parent
        {
            get;
            private set;
        }
        new private LayoutEngine Layout;

        private PlotEditor Editor;

        public Plots(MainForm Parent)
        {
            InitializeComponent();

            this.Parent = Parent;
            Layout = new LayoutEngine(pnlMain, Apex.Layout.LayoutEngine.ContainerType.Panel)
            {
                ClearOnProcess = true,
                Padding = new Padding(6)
            };
            RefreshPlots();
        }

        public void RefreshPlots()
        {
            Layout.ClearLayout();

            foreach (var plot in Parent.NetworkParameters.Plots)
            {
                using (Layout.BeginGroupBox(new GroupBox { Text = $"{plot.Name} ({plot.Width}x{plot.Height})" }))
                {
                    foreach (var attribute in plot.Attributes)
                    {
                        using (Layout.BeginRow())
                        {
                            Layout.AddControl(new Label() { Text = attribute.ToString() });
                        }
                    }

                    using (Layout.BeginRow())
                    {
                        var editButton = new Button() { Text = "Edit" };
                        editButton.Click += (object sender, EventArgs e) => { EditPlot(plot); };
                        var deleteButton = new Button() { Text = "Delete" };
                        deleteButton.Click += (object sender, EventArgs e) => { DeletePlot(plot); };
                        Layout.AddControl(editButton);
                        Layout.AddControl(deleteButton);
                    }
                }
            }

            Layout.ProcessLayout();
        }

        public void EditorClosed()
        {
            RefreshPlots();
            Editor = null;
        }

        private void EditPlot(Plot Plot)
        {
            if (Editor == null)
            {
                Editor = new PlotEditor(this, Plot);
                Editor.Show();
            }
            else
            {
                Editor.LoadPlot(Plot);
                Editor.BringToFront();
            }
        }

        public void DeletePlot(Plot Plot)
        {
            Parent.NetworkParameters.Plots.Remove(Plot);
            RefreshPlots();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPlots();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var plot = new Plot
            {
                Name = "NewPlot"
            };
            Parent.NetworkParameters.Plots.Add(plot);
            RefreshPlots();
            EditPlot(plot);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete every plot?", "Confirm Clear") == DialogResult.OK)
            {
                Parent.NetworkParameters.Plots.Clear();
                RefreshPlots();
            }
        }

        private void Plots_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Close();
            }
            Parent.ChildClosing(this);
        }
    }
}
