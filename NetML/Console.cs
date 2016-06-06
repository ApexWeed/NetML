using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using Priority_Queue;

namespace NetML
{
    public partial class Console : Form
    {
        new private bool Closing;
        new private MainForm Parent;

        private BuildSlave BS;
        private SimulationParameters Parameters;
        //private Queue<string> OutputQueue;
        private SimplePriorityQueue<string> OutputQueue;
        private Thread QueueThread;
        private Thread SimulationAwaitThread;
        
        public Console(MainForm Parent, SimulationParameters Parameters)
        {
            InitializeComponent();

            this.Parent = Parent;
            LoadParameters(Parameters);

            //QueueThread = new Thread(() => { HandleOutputQueue(); });
            //QueueThread.Start();
            HandleOutputQueue();
        }

        private delegate string RichTextBoxGetTextDelegate(RichTextBox Control);
        private string RichTextBoxGetText(RichTextBox Control)
        {
            if (Control.InvokeRequired)
            {

                var d = new RichTextBoxGetTextDelegate(RichTextBoxGetText);

                try
                {
                    return (string)this.Invoke(d, Control);
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return Control.Text;
            }
        }

        private delegate void RichTextBoxAppendTextDelegate(RichTextBox Control, string Text);
        private void RichTextBoxAppendText(RichTextBox Control, string Text)
        {
            if (Control.InvokeRequired)
            {
                var d = new RichTextBoxAppendTextDelegate(RichTextBoxAppendText);
                this.Invoke(d, Control, Text);
            }
            else
            {
                Control.AppendText(Text);
            }
        }

        private delegate void RichTextBoxScrollToCaretDelegate(RichTextBox Control);
        private void RichTextBoxScrollToCaret(RichTextBox Control)
        {
            if (Control.InvokeRequired)
            {
                var d = new RichTextBoxScrollToCaretDelegate(RichTextBoxScrollToCaret);
                this.Invoke(d, Control);
            }
            else
            {
                Control.ScrollToCaret();
            }
        }

        private delegate void RichTextBoxSetSelectionColourDelegate(RichTextBox Control, Color Colour);
        private void RichTextBoxSetSelectionColour(RichTextBox Control, Color Colour)
        {
            if (Control.InvokeRequired)
            {
                var d = new RichTextBoxSetSelectionColourDelegate(RichTextBoxSetSelectionColour);
                this.Invoke(d, Control, Colour);
            }
            else
            {
                try
                {
                    Control.SelectionColor = Colour;
                }
                catch (Exception)
                {
                    // WinForms is a dick.
                }
            }
        }

        private delegate void RichTextBoxSetSelectionBackColourDelegate(RichTextBox Control, Color Colour);
        private void RichTextBoxSetSelectionBackColour(RichTextBox Control, Color Colour)
        {
            if (Control.InvokeRequired)
            {
                var d = new RichTextBoxSetSelectionBackColourDelegate(RichTextBoxSetSelectionBackColour);
                this.Invoke(d, Control, Colour);
            }
            else
            {
                Control.SelectionBackColor = Colour;
            }
        }

        private void ProcessRichText(RichTextBox Control, string Text)
        {
            if (Text.IndexOf((char)27) > -1)
            { 
                // There are escape codes in here.
                while (Text.IndexOf((char)27) > -1)
                {
                    var index = Text.IndexOf((char)27);
                    // Output line before format.
                    if (index > 0)
                    {
                        //Control.AppendText(Text.Substring(0, index));
                        RichTextBoxAppendText(Control, Text.Substring(0, index));
                    }

                    Text = Text.Substring(index);
                    if (Text[1] == '[')
                    {
                        // Foreground colour.
                        if (Text[2] == '3')
                        {
                            switch (Text[3])
                            {
                                case '0':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Black);
                                        //Control.SelectionColor = Color.Black;
                                        break;
                                    }
                                case '1':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Red);
                                        //Control.SelectionColor = Color.Red;
                                        break;
                                    }
                                case '2':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Green);
                                        //Control.SelectionColor = Color.Green;
                                        break;
                                    }
                                case '3':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Yellow);
                                        //Control.SelectionColor = Color.Yellow;
                                        break;
                                    }
                                case '4':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Blue);
                                        //Control.SelectionColor = Color.Blue;
                                        break;
                                    }
                                case '5':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Magenta);
                                        //Control.SelectionColor = Color.Magenta;
                                        break;
                                    }
                                case '6':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.Cyan);
                                        //Control.SelectionColor = Color.Cyan;
                                        break;
                                    }
                                case '7':
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.White);
                                        //Control.SelectionColor = Color.White;
                                        break;
                                    }
                                default:
                                    {
                                        RichTextBoxSetSelectionColour(Control, Color.White);
                                        //Control.SelectionColor = Color.White;
                                        break;
                                    }
                            }
                            Text = Text.Substring(5);
                        }
                        else // Background colour.
                        if (Text[2] == '4')
                        {
                            switch (Text[3])
                            {
                                case '0':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Black);
                                        //Control.SelectionBackColor = Color.Black;
                                        break;
                                    }
                                case '1':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Red);
                                        //Control.SelectionBackColor = Color.Red;
                                        break;
                                    }
                                case '2':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Green);
                                        //Control.SelectionBackColor = Color.Green;
                                        break;
                                    }
                                case '3':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Yellow);
                                        //Control.SelectionBackColor = Color.Yellow;
                                        break;
                                    }
                                case '4':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Blue);
                                        //Control.SelectionBackColor = Color.Blue;
                                        break;
                                    }
                                case '5':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Magenta);
                                        //Control.SelectionBackColor = Color.Magenta;
                                        break;
                                    }
                                case '6':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Cyan);
                                        //Control.SelectionBackColor = Color.Cyan;
                                        break;
                                    }
                                case '7':
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.White);
                                        //Control.SelectionBackColor = Color.White;
                                        break;
                                    }
                                default:
                                    {
                                        RichTextBoxSetSelectionBackColour(Control, Color.Black);
                                        //Control.SelectionBackColor = Color.Black;
                                        break;
                                    }
                            }
                            Text = Text.Substring(5);
                        }
                        else if (Text[2] >= 'A' && Text[2] <= 'H')
                        {
                            Text = Text.Substring(3);
                        }
                        else
                        {
                            var nextChar = 2;
                            while (Text[++nextChar] != 'm')
                            {
                                switch (Text[nextChar])
                                {
                                    case '0':
                                        {
                                            RichTextBoxSetSelectionBackColour(Control, Color.Black);
                                            RichTextBoxSetSelectionColour(Control, Color.White);
                                            //Control.SelectionBackColor = Color.Black;
                                            //Control.SelectionColor = Color.White;
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }
                            Text = Text.Substring(++nextChar);
                        }
                    }
                    else if (Text[1] == ']')
                    {
                        if (Text[2] == '0' && Text[3] == ';')
                        {
                            // Don't actually have a window title to set.
                            return;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Text = Text.Substring(1);
                    }
                }
            }
            else
            {
                RichTextBoxAppendText(Control, Text);
                //Control.AppendText(Text);
            }
            RichTextBoxScrollToCaret(Control);
            //Control.ScrollToCaret();
        }

        public void LoadParameters(SimulationParameters Parameters)
        {
            this.Parameters = Parameters;
            this.Text = $"Console - {Parameters.Name}";
            //OutputQueue = new Queue<string>();
            OutputQueue = new SimplePriorityQueue<string>();

            if (BS != null)
            {
                BS.OutputDataReceived -= OutputDataReceived;
                BS.ErrorDataReceived -= ErrorDataReceived;
                BS.Abort();
            }

            BS = new BuildSlave();
            BS.OutputDataReceived += OutputDataReceived;
            BS.ErrorDataReceived += ErrorDataReceived;
            BS.StartConsole(Properties.Settings.Default.BashPath, "-i -l");
            Thread.Sleep(250);

            rtbOutput.Clear();
            lock (OutputQueue)
            {
                OutputQueue.Enqueue($"{(char)27}[36mCleaning up...\n{(char)27}[0m", DateTime.Now.ToBinary());
            }
            if (!Directory.Exists(Path.Combine(Properties.Settings.Default.NS3Dir, "build/scratch/out")))
            {
                Directory.CreateDirectory(Path.Combine(Properties.Settings.Default.NS3Dir, "build/scratch/out"));
            }
            foreach (var file in Directory.GetFiles(Path.Combine(Properties.Settings.Default.NS3Dir, "build/scratch/out")))
            {
                File.Delete(file);
            }
            if (!Directory.Exists(Path.Combine(Parameters.EscapedName, "output")))
            {
                Directory.CreateDirectory(Path.Combine(Parameters.EscapedName, "output"));
            }
            foreach (var file in Directory.GetFiles(Path.Combine(Parameters.EscapedName, "output")))
            {
                File.Delete(file);
            }

            BS.SendInput($"cd {Properties.Settings.Default.CygwinDir}");
            lock (OutputQueue)
            {
                OutputQueue.Enqueue($"{(char)27}[36mRunning simulation...\n{(char)27}[0m", DateTime.Now.ToBinary());
            }
            BS.SendInput($"./waf --run {Parameters.EscapedName} --cwd=\"build/scratch/out\"");

            SimulationAwaitThread = new Thread(() => 
            {
                AwaitSimulation();
            });
            SimulationAwaitThread.Start();
        }

        private void AwaitSimulation()
        {
            while (true)
            {
                if (Closing)
                {
                    return;
                }
                if (RichTextBoxGetText(rtbOutput).Contains("Simulation complete."))
                {
                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"\n{(char)27}[36mAveraging data...\n{(char)27}[0m", DateTime.Now.ToBinary());
                    }
                    DataManipulator.AverageData(Parameters);

                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"{(char)27}[36mScaling data...\n{(char)27}[0m", DateTime.Now.ToBinary());
                    }
                    DataManipulator.ScaleData(Parameters);

                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"{(char)27}[36mGenerating plots...\n{(char)27}[0m", DateTime.Now.ToBinary());
                    }
                    SourceGenerator.GeneratePlots(Parameters);
                    BS.SendInput("cd build/scratch/out");
                    Thread.Sleep(50);
                    foreach (var plot in Parameters.Plots)
                    {
                        BS.SendInput($"gnuplot {plot.EscapedName}.gpl");
                        Thread.Sleep(500);
                    }
                    
                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"{(char)27}[36mMoving files to local directory...\n{(char)27}[0m", DateTime.Now.ToBinary());
                        Thread.Sleep(50);
                    }
                    foreach (var file in Directory.GetFiles(Path.Combine(Properties.Settings.Default.NS3Dir, "build/scratch/out")))
                    {
                        File.Copy(file, Path.Combine($"{Parameters.EscapedName}/output", Path.GetFileName(file)));
                    }

                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"\n{(char)27}[36mJob complete.\n{(char)27}[0m", DateTime.Now.ToBinary());
                    }
                    return;
                }
                Thread.Sleep(100);
            }
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (!e.Data.StartsWith("bash"))
                {
                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"{e.Data}\n", DateTime.Now.ToBinary());
                    }
                }
            }
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                lock (OutputQueue)
                {
                    lock (OutputQueue)
                    {
                        OutputQueue.Enqueue($"{e.Data}\n", DateTime.Now.ToBinary());
                    }
                }
            }
        }

        private async void HandleOutputQueue()
        {
            while (true)
            {
                lock (OutputQueue)
                {
                    while (OutputQueue.Count > 0)
                    {
                        var line = OutputQueue.Dequeue();
                        ProcessRichText(rtbOutput, line);
                    }
                }
                await Task.Run(() => { Thread.Sleep(50); });
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            BS.SendInput(txtEntry.Text);
            txtEntry.Text = "";
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

        private async void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            Closing = true;
            BS.OutputDataReceived -= OutputDataReceived;
            BS.ErrorDataReceived -= ErrorDataReceived;
            BS.Abort();
            //QueueThread.Abort();
            Parent.ChildClosing(this);
        }
    }
}
