using System;
using System.Diagnostics;
using System.Threading;

namespace NetML
{
    public class BuildSlave
    {
        private Thread Worker;
        private Process Job;

        public event EventHandler<DataReceivedEventArgs> OutputDataReceived;
        public event EventHandler<DataReceivedEventArgs> ErrorDataReceived;

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived?.Invoke(this, e);
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived?.Invoke(this, e);
        }

        public void SendInput(string Input)
        {
            if (Job != null)
            {
                Job.StandardInput.WriteLine(Input);
            }
        }

        public void StartConsole(string Path, string Arguments)
        {
            Worker = new Thread(() =>
            {
                var startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    Arguments = Arguments,
                    FileName = Path
                };

                Job = new Process
                {
                    StartInfo = startInfo,
                    EnableRaisingEvents = true,
                };
                Job.OutputDataReceived += OnOutputDataReceived;
                Job.ErrorDataReceived += OnErrorDataReceived;

                Job.Start();
                Job.BeginOutputReadLine();

                Job.WaitForExit();
                Job.CancelOutputRead();
                Job.Dispose();
                Job = null;
            });
            Worker.Start();
        }

        public void Abort()
        {
            Worker.Abort();
            Job.Kill();
            Job.Dispose();
        }
    }
}
