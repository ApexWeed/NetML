using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace NetML
{
    public partial class Console : Form
    {
        private BuildSlave BS;

        public Console()
        {
            InitializeComponent();
        }

        private delegate void AppendTextDelegate(TextBox Control, string Text);
        private void AppendText(TextBox Control, string Text)
        {
            if (Control.InvokeRequired)
            {
                var d = new AppendTextDelegate(AppendText);
                this.Invoke(d, Control, Text);
            }
            else
            {
                Control.AppendText(Text);
                Control.AppendText("\n");
            }
        }

        private void Console_Load(object sender, EventArgs e)
        {
            BS = new BuildSlave();
            BS.OutputDataReceived += OutputDataReceived;
            BS.ErrorDataReceived += ErrorDataReceived;
            BS.StartConsole(@"C:\cygwin64\bin\bash.exe", "-i -l");
            Thread.Sleep(250);
            BS.SendInput($"cd {Properties.Settings.Default.CygwinDir}");
            //BS.SendInput("./waf");
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                AppendText(txtOutput, $"stdout: {e.Data}");
            }
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                AppendText(txtOutput, $"stderr: {e.Data}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void txtEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                BS.SendInput(txtEntry.Text);
                txtEntry.Text = "";
            }
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            BS.Abort();
        }
    }
}
