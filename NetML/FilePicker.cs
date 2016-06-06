using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetML
{
    public partial class FilePicker : Form
    {
        public string ChosenFile;

        public FilePicker(string Title, string Description, string DefaultFolder, string Filter = "")
        {
            InitializeComponent();
            this.Text = Title;
            lblDescription.Text = Description;
            txtFile.Text = DefaultFolder;
            fileDialogue.Filter = Filter;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ChosenFile = txtFile.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fileDialogue.FileName = txtFile.Text;
            if (fileDialogue.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = fileDialogue.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
