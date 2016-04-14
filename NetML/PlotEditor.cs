using System;
using System.Windows.Forms;
using Apex.Layout;

namespace NetML
{
    public partial class PlotEditor : Form
    {
        new private Plots Parent;
        new private LayoutEngine Layout;

        private Plot PlotLink;
        private Plot CurrentPlot;

        public PlotEditor(Plots Parent, Plot Plot)
        {
            InitializeComponent();

            this.Parent = Parent;
            cmbLegend.Items.AddRange(Apex.EnumUtil.GetValuesCombo<Plot.Legend>());
            Layout = new LayoutEngine(this)
            {
                ClearOnProcess = true,
                Padding = new Padding(6),
                Margin = new Padding(6)
            };
            LoadPlot(Plot);
        }

        public void LoadPlot(Plot Plot, bool New = true)
        {
            var controlPairs = new Tuple<Control, Control>[] 
            {
                new Tuple<Control, Control>(lblName, txtName),
                new Tuple<Control, Control>(lblLegend, cmbLegend),
                new Tuple<Control, Control>(pnlWidth, pnlHeight),
                new Tuple<Control, Control>(lblXAxis, txtXAxis),
                new Tuple<Control, Control>(lblYAxis, txtYAxis),
                new Tuple<Control, Control>(lblY2Axis, txtY2Axis)
            };
            
            // Don't update link if the plot was updated by this form.
            if (New)
            {
                // Clone object and keep a reference to the original so we can quit without saving.
                PlotLink = Plot;
                CurrentPlot = new Plot(Plot);
                Plot = CurrentPlot;
            }

            txtName.Text = Plot.Name;
            cmbLegend.SelectedItem = Plot.LegendLocation;
            txtWidth.Text = Plot.Width.ToString();
            txtHeight.Text = Plot.Height.ToString();
            txtXAxis.Text = Plot.XAxisLabel;
            txtYAxis.Text = Plot.YAxisLabel;
            txtY2Axis.Text = Plot.Y2AxisLabel;
            chkDisabled.Checked = Plot.Disabled;

            if (Plot.Attributes.Count == 0)
            {
                AddAttribute(Plot);
            }

            Layout.ClearLayout();
            if (this.Width > 500)
            {
                for (int i = 0; i < controlPairs.Length; i += 2)
                {
                    using (Layout.BeginRow())
                    {
                        Layout.AddControl(controlPairs[i].Item1);
                        Layout.AddControl(controlPairs[i].Item2);
                        if (controlPairs.Length > i + 1)
                        {
                            Layout.AddControl(controlPairs[i + 1].Item1);
                            Layout.AddControl(controlPairs[i + 1].Item2);
                        }
                    }
                }
            }
            else
            {
                foreach (var pair in controlPairs)
                {
                    using (Layout.BeginRow())
                    {
                        Layout.AddControl(pair.Item1);
                        Layout.AddControl(pair.Item2);
                    }
                }
            }

            using (Layout.BeginPanel(pnlAttributes))
            {
                var index = 0;
                foreach (var attribute in Plot.Attributes)
                {
                    using (Layout.BeginGroupBox(new GroupBox { Text = $"Attribute {++index}" }))
                    {
                        var cmbParameter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                        cmbParameter.Items.AddRange(Parent.Parent.NetworkParameters.Traces.ToArray());
                        cmbParameter.SelectedItem = attribute.TraceParameter;
                        cmbParameter.SelectedIndexChanged += (object sender, EventArgs e) =>
                        {
                            attribute.TraceParameter = (Trace)cmbParameter.SelectedItem;
                        };

                        var cmbAveragingType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                        cmbAveragingType.Items.AddRange(Apex.EnumUtil.GetValuesCombo<PlotAttribute.Averaging>());
                        cmbAveragingType.SelectedItem = attribute.AveragingType;
                        cmbAveragingType.SelectedIndexChanged += (object sender, EventArgs e) =>
                        {
                            attribute.AveragingType = (PlotAttribute.Averaging)cmbAveragingType.SelectedItem;
                        };

                        var cmbPlotType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                        cmbPlotType.Items.AddRange(Apex.EnumUtil.GetValuesCombo<PlotAttribute.PlotType>());
                        cmbPlotType.SelectedItem = attribute.Type;
                        cmbPlotType.SelectedIndexChanged += (object sender, EventArgs e) =>
                        {
                            attribute.Type = (PlotAttribute.PlotType)cmbPlotType.SelectedItem;
                        };

                        var txtWindowSize = new TextBox { Text = attribute.WindowWidth.ToString() };
                        txtWindowSize.KeyPress += Apex.InputFiltering.FilterNumeric;
                        txtWindowSize.TextChanged += (object sender, EventArgs e) =>
                        {
                            attribute.WindowWidth = float.Parse(txtWindowSize.Text);
                        };

                        var txtModulo = new TextBox
                        {
                            Text = attribute.ModuloValue.ToString(),
                            Enabled = attribute.ModuloEnabled
                        };
                        txtModulo.KeyPress += Apex.InputFiltering.FilterNumeric;
                        txtModulo.TextChanged += (object sender, EventArgs e) =>
                        {
                            attribute.ModuloValue = float.Parse(txtModulo.Text);
                        };

                        var chkModulo = new CheckBox
                        {
                            Text = "Modulo",
                            Checked = attribute.ModuloEnabled
                        };
                        chkModulo.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.ModuloEnabled = chkModulo.Checked;
                            txtModulo.Enabled = chkModulo.Checked;
                        };

                        var txtMin = new TextBox
                        {
                            Text = attribute.MinValue.ToString(),
                            Enabled = attribute.MinEnabled
                        };
                        txtMin.KeyPress += Apex.InputFiltering.FilterNumeric;
                        txtMin.TextChanged += (object sender, EventArgs e) =>
                        {
                            attribute.MinValue = float.Parse(txtMin.Text);
                        };

                        var chkMin = new CheckBox
                        {
                            Text = "Min",
                            Checked = attribute.MinEnabled
                        };
                        chkMin.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.MinEnabled = chkMin.Checked;
                            txtMin.Enabled = chkMin.Checked;
                        };

                        var txtMax = new TextBox
                        {
                            Text = attribute.MaxValue.ToString(),
                            Enabled = attribute.MaxEnabled
                        };
                        txtMax.KeyPress += Apex.InputFiltering.FilterNumeric;
                        txtMax.TextChanged += (object sender, EventArgs e) =>
                        {
                            attribute.MaxValue = float.Parse(txtMax.Text);
                        };

                        var chkMax = new CheckBox
                        {
                            Text = "Max",
                            Checked = attribute.MaxEnabled
                        };
                        chkMax.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.MaxEnabled = chkMax.Checked;
                            txtMax.Enabled = chkMax.Checked;
                        };

                        var txtScaleData = new TextBox
                        {
                            Text = attribute.ScaleDataCommand,
                            Enabled = attribute.ScaleData
                        };
                        txtScaleData.TextChanged += (object sender, EventArgs e) =>
                        {
                            attribute.ScaleDataCommand = txtScaleData.Text;
                        };

                        var chkScaleData = new CheckBox
                        {
                            Text = "Scale Data",
                            Checked = attribute.ScaleData
                        };
                        chkScaleData.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.ScaleData = chkScaleData.Checked;
                            txtScaleData.Enabled = chkScaleData.Checked;
                        };

                        var chkY2 = new CheckBox
                        {
                            Text = "Use Y2 Axis",
                            Checked = attribute.UseY2Axis
                        };
                        chkY2.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.UseY2Axis = chkY2.Checked;
                        };

                        var chkShowPoints = new CheckBox
                        {
                            Text = "Show Points",
                            Checked = attribute.ShowPoints
                        };
                        chkShowPoints.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.ShowPoints = chkShowPoints.Checked;
                        };

                        var chkShowLines = new CheckBox
                        {
                            Text = "Show Lines",
                            Checked = attribute.ShowLines
                        };
                        chkShowLines.CheckedChanged += (object sender, EventArgs e) =>
                        {
                            attribute.ShowLines = chkShowLines.Checked;
                        };

                        var btnDeleteAttribute = new Button
                        {
                            Text = "Delete Attribute"
                        };
                        btnDeleteAttribute.Click += (object sender, EventArgs e) => { DeleteAttribute(Plot, attribute); };

                        // Put twice as much stuff on one line if there is space.
                        if (this.Width > 500)
                        {
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label { Text = "Traced Parameter" });
                                Layout.AddControl(cmbParameter);
                                Layout.AddControl(new Label { Text = "Window Width" });
                                Layout.AddControl(txtWindowSize);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label { Text = "Averaging Type" });
                                Layout.AddControl(cmbAveragingType);
                                Layout.AddControl(new Label { Text = "Plot Type" });
                                Layout.AddControl(cmbPlotType);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkModulo);
                                Layout.AddControl(txtModulo);
                                Layout.AddControl(chkScaleData);
                                Layout.AddControl(txtScaleData);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkMin);
                                Layout.AddControl(txtMin);
                                Layout.AddControl(chkMax);
                                Layout.AddControl(txtMax);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkY2);
                                Layout.AddControl(chkShowPoints);
                                Layout.AddControl(chkShowLines);
                                Layout.AddControl(btnDeleteAttribute);
                            }
                        }
                        else
                        {
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label { Text = "Traced Parameter" });
                                Layout.AddControl(cmbParameter);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label { Text = "Window Width" });
                                Layout.AddControl(txtWindowSize);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label { Text = "Averaging Type" });
                                Layout.AddControl(cmbAveragingType);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(new Label { Text = "Plot Type" });
                                Layout.AddControl(cmbPlotType);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkModulo);
                                Layout.AddControl(txtModulo);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkScaleData);
                                Layout.AddControl(txtScaleData);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkMin);
                                Layout.AddControl(txtMin);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkMax);
                                Layout.AddControl(txtMax);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkY2);
                                Layout.AddControl(chkShowPoints);
                            }
                            using (Layout.BeginRow())
                            {
                                Layout.AddControl(chkShowLines);
                                Layout.AddControl(btnDeleteAttribute);
                            }
                        }
                    }
                }
            }

            var btnAddAttribute = new Button { Text = "Add attribute" };
            btnAddAttribute.Click += (object sender, EventArgs e) =>
            {
                AddAttribute(Plot);
                LoadPlot(Plot, false);
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

        private void AddAttribute(Plot Plot)
        {
            var traces = Parent.Parent.NetworkParameters.Traces;
            if (traces.Count > 0)
            {
                Plot.Attributes.Add(new PlotAttribute
                {
                    TraceParameter = traces[0]
                });
            }
            else
            {
                MessageBox.Show("Can't add a plot attribute with no traces.");
            }
        }

        private void DeleteAttribute(Plot Plot, PlotAttribute Attribute)
        {
            Plot.Attributes.Remove(Attribute);
            LoadPlot(Plot, false);
        }

        private void pnlWidth_SizeChanged(object sender, EventArgs e)
        {
            txtWidth.Width = pnlWidth.Width - txtWidth.Left;
        }

        private void pnlHeight_SizeChanged(object sender, EventArgs e)
        {
            txtHeight.Width = pnlHeight.Width - txtHeight.Left;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.Text = $"Plot Editor - {txtName.Text}";
            CurrentPlot.Name = txtName.Text;
        }

        private void PlotEditor_ResizeEnd(object sender, EventArgs e)
        {
            LoadPlot(CurrentPlot, false);
        }

        private void PlotEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.EditorClosed();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PlotLink.Set(CurrentPlot);
            Parent.RefreshPlots();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Parent.DeletePlot(PlotLink);
            this.Close();
        }

        private void cmbLegend_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPlot.LegendLocation = (Plot.Legend)cmbLegend.SelectedItem;
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            CurrentPlot.Width = int.Parse(txtWidth.Text);
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            CurrentPlot.Height = int.Parse(txtHeight.Text);
        }

        private void txtXAxis_TextChanged(object sender, EventArgs e)
        {
            CurrentPlot.XAxisLabel = txtXAxis.Text;
        }

        private void txtYAxis_TextChanged(object sender, EventArgs e)
        {
            CurrentPlot.YAxisLabel = txtYAxis.Text;
        }

        private void txtY2Axis_TextChanged(object sender, EventArgs e)
        {
            CurrentPlot.Y2AxisLabel = txtY2Axis.Text;
        }

        private void chkDisabled_CheckedChanged(object sender, EventArgs e)
        {
            CurrentPlot.Disabled = chkDisabled.Checked;
        }
    }
}
