using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Apex;

namespace NetML
{
    public partial class DisplayPropertiesEditor : Form
    {
        new private MainForm Parent;

        public DisplayPropertiesEditor(MainForm Parent)
        {
            InitializeComponent();
            this.Parent = Parent;
        }

        private void DisplayPropertiesEditor_Load(object sender, EventArgs e)
        {
            //cmbNodeDisplayMode.Items.AddRange(EnumUtil.GetValues<DisplayProperties.NodeDisplay>().Cast<object>().ToArray());
            //cmbLinkDisplayMode.Items.AddRange(EnumUtil.GetValues<DisplayProperties.LinkDisplay>().Cast<object>().ToArray());
            //cmbStreamDisplayMode.Items.AddRange(EnumUtil.GetValues<DisplayProperties.StreamDisplay>().Cast<object>().ToArray());

            //cmbNodeDisplayMode.SelectedItem = DisplayProperties.NodeDisplayMode;
            //cmbLinkDisplayMode.SelectedItem = DisplayProperties.LinkDisplayMode;
            //cmbStreamDisplayMode.SelectedItem = DisplayProperties.StreamDisplayMode;

            var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var fields = typeof(DisplayProperties).GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    var controlField = typeof(DisplayPropertiesEditor).GetField($"cmb{field.Name}", bindFlags);
                    var comboBox = (ComboBox)controlField.GetValue(this);
                    var items = (Enum.GetValues(field.FieldType)).Cast<object>().ToArray();
                    comboBox.Items.AddRange(items);
                    comboBox.SelectedItem = field.GetValue(null);
                }
                else if (field.FieldType == typeof(bool))
                {
                    var controlField = typeof(DisplayPropertiesEditor).GetField($"chk{field.Name}", bindFlags);
                    var checkBox = (CheckBox)controlField.GetValue(this);
                    checkBox.Checked = (bool)field.GetValue(null);
                }
            }
        }

        private void ComboBoxChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;

            var field = typeof(DisplayProperties).GetField(comboBox.Name.Substring(3));
            field.SetValue(null, comboBox.SelectedItem);

            Parent.RefreshCanvas();
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;

            var field = typeof(DisplayProperties).GetField(checkBox.Name.Substring(3));
            field.SetValue(null, checkBox.Checked);

            Parent.RefreshCanvas();
        }

        private void DisplayPropertiesEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosing(this);
        }
    }
}
