using System;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class NetworkPropertiesEditor : Form
    {
        new private MainForm Parent;
        new private LayoutEngine Layout;
        private SimulationParameters Parameters;

        public NetworkPropertiesEditor(MainForm Parent, SimulationParameters Parameters)
        {
            InitializeComponent();

            Layout = new LayoutEngine(pnlMain, Apex.Layout.LayoutEngine.ContainerType.Panel)
            {
                ClearOnProcess = true,
                Padding = new Padding(6),
            };
            this.Parameters = Parameters;
            this.Parent = Parent;

            txtName.Text = Parameters.Name;
            txtObservationStartTime.Text = Parameters.ObservationStartTime.ToString();
            txtObservationStopTime.Text = Parameters.ObservationStopTime.ToString();
            chkPrintAttributes.Checked = Parameters.PrintAttributes;
            chkAsciiTrace.Checked = Parameters.AsciiTrace;

            UpdateLayout();
        }

        private void UpdateLayout()
        {
            Layout.ClearLayout();

            var index = 0;
            foreach (var componentLog in Parameters.ComponentLogs)
            {
                using (Layout.BeginGroupBox(new GroupBox { Text = $"Component {++index}" }))
                {
                    var module = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    module.Items.AddRange(Apex.EnumUtil.GetValuesCombo<LogModule>());
                    module.SelectedItem = componentLog.LoggingModule;
                    module.SelectedIndexChanged += (object sender, EventArgs e) => { componentLog.LoggingModule = (LogModule)module.SelectedItem; };

                    var level = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    level.Items.AddRange(Apex.EnumUtil.GetValuesCombo<LogLevel>());
                    level.SelectedItem = componentLog.LoggingLevel;
                    level.SelectedIndexChanged += (object sender, EventArgs e) => { componentLog.LoggingLevel = (LogLevel)level.SelectedItem; };

                    var delete = new Button { Text = "Delete Module" };
                    delete.Click += (object sender, EventArgs e) => { DeleteModule(componentLog); };

                    using (Layout.BeginRow())
                    {
                        Layout.AddControl(new Label { Text = "Logging Module" });
                        Layout.AddControl(module);
                    }

                    using (Layout.BeginRow())
                    {
                        Layout.AddControl(new Label { Text = "Logging Level" });
                        Layout.AddControl(level);
                    }
                    
                    Layout.AddControl(delete);
                }
            }

            Layout.AddControl(btnNew);

            Layout.ProcessLayout();
        }

        private void DeleteModule(ComponentLog Module)
        {
            Parameters.ComponentLogs.Remove(Module);
            UpdateLayout();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Parameters.ComponentLogs.Add(new ComponentLog());
            UpdateLayout();
        }

        private void NetworkPropertiesEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Parameters.Name = txtName.Text;
            Parent.SetTitle();
        }

        private void txtObservationStartTime_TextChanged(object sender, EventArgs e)
        {
            Parameters.ObservationStartTime = float.Parse(txtObservationStartTime.Text);
        }

        private void txtObservationStopTime_TextChanged(object sender, EventArgs e)
        {
            Parameters.ObservationStopTime = float.Parse(txtObservationStopTime.Text);
        }

        private void chkPrintAttributes_CheckedChanged(object sender, EventArgs e)
        {
            Parameters.PrintAttributes = chkPrintAttributes.Checked;
        }

        private void chkAsciiTrace_CheckedChanged(object sender, EventArgs e)
        {
            Parameters.AsciiTrace = chkAsciiTrace.Checked;
        }
    }
}
