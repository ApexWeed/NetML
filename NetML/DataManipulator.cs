using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Apex.Extensions;

namespace NetML
{
    public static class DataManipulator
    {
        private class AverageScheme
        {
            public string EscapedName;
            public string TraceName;
            public string SourceFile
            {
                get { return $"{EscapedName}_T{TraceName}_trace.txt"; }
            }
            public string OutputFile
            {
                get { return $"{EscapedName}_T{TraceName}_{(Delta ? "del" : "avg")}_{Window}.txt"; }
            }
            public float Window;
            public bool Delta;
            public bool ModuloEnabled;
            public float ModuloValue;
            public bool MinEnabled;
            public float MinValue;
            public bool MaxEnabled;
            public float MaxValue;

            public override bool Equals(object obj)
            {
                if (obj is AverageScheme)
                {
                    var other = obj as AverageScheme;
                    return (SourceFile == other.SourceFile && Window == other.Window && Delta == other.Delta);
                }
                return false;
            }
        }

        public static void AverageData(SimulationParameters Parameters)
        {
            var directory = Path.Combine(Properties.Settings.Default.NS3Dir, "build/scratch/out");

            var dataSources = new Dictionary<string, List<Tuple<float, int>>>();
            foreach (var trace in Parameters.Traces)
            {
                var filename = $"{Parameters.EscapedName}_T{trace.Name}_trace.txt";
                var lines = File.ReadAllLines(Path.Combine(directory, filename));
                var data = new List<Tuple<float, int>>(lines.Length);
                foreach (var line in lines)
                {
                    var parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        var time = float.Parse(parts[0]);
                        var value = int.Parse(parts[1]);
                        var tuple = new Tuple<float, int>(time, value);
                        data.Add(tuple);
                    }
                }
                dataSources.Add(filename, data);
            }

            var averageSchemes = new List<AverageScheme>();
            foreach (var plot in Parameters.Plots)
            {
                foreach (var attribute in plot.Attributes)
                {
                    var scheme = new AverageScheme
                    {
                        EscapedName = Parameters.EscapedName,
                        TraceName = attribute.TraceParameter.EscapedName,
                        Window = attribute.WindowWidth,
                        Delta = attribute.AveragingType == PlotAttribute.Averaging.Delta,
                        ModuloEnabled = attribute.ModuloEnabled,
                        ModuloValue = attribute.ModuloValue,
                        MinEnabled = attribute.MinEnabled,
                        MinValue = attribute.MinValue,
                        MaxEnabled = attribute.MaxEnabled,
                        MaxValue = attribute.MaxValue
                    };
                    averageSchemes.AddDistinct(scheme);
                }
            }

            foreach (var scheme in averageSchemes)
            {
                var inputData = dataSources[scheme.SourceFile];
                var outputData = new List<Tuple<float, double>>();

                var index = 0;
                var currentWindow = scheme.Window;
                var currentTotal = 0d;
                var currentCount = 0;
                while (index < inputData.Count)
                {
                    // Gone past the end of the current window, average it up.
                    if (inputData[index].Item1 > currentWindow)
                    {
                        if (currentCount > 0)
                        {
                            var result = currentTotal / currentCount;
                            outputData.Add(new Tuple<float, double>(currentWindow, result));
                            currentWindow += scheme.Window;

                            // Keep outputting same value so we don't end up with gaps.
                            while (inputData[index].Item1 > currentWindow)
                            {
                                outputData.Add(new Tuple<float, double>(currentWindow, result));
                                currentWindow += scheme.Window;
                            }

                            currentCount = 1;
                            currentTotal = inputData[index].Item2;
                        }
                        else
                        {
                            // Move window until the data starts.
                            while (inputData[index].Item1 > currentWindow)
                            {
                                currentWindow += scheme.Window;
                            }

                            currentCount = 1;
                            currentTotal = inputData[index].Item2;
                        }
                    }
                    else
                    {
                        currentCount++;
                        currentTotal += inputData[index].Item2;
                    }
                    index++;
                }

                // Output the last bits of data in the buffer.
                if (currentCount > 0)
                {
                    outputData.Add(new Tuple<float, double>(currentWindow, currentTotal / currentCount));
                }

                if (scheme.Delta)
                {

                    for (int i = outputData.Count - 1; i > 0; i--)
                    {
                        outputData[i] = new Tuple<float, double>(outputData[i].Item1, (outputData[i].Item2 - outputData[i - 1].Item2) * 10.0f);
                    }
                }

                if (scheme.ModuloEnabled)
                {
                    for (int i = 0; i < outputData.Count; i++)
                    {
                        outputData[i] = new Tuple<float, double>(outputData[i].Item1, outputData[i].Item2 % scheme.ModuloValue);
                    }
                }

                if (scheme.MinEnabled)
                {
                    for (int i = 0; i < outputData.Count; i++)
                    {
                        outputData[i] = new Tuple<float, double>(outputData[i].Item1, outputData[i].Item2 < scheme.MinValue ? scheme.MinValue : outputData[i].Item2);
                    }
                }

                if (scheme.MaxEnabled)
                {
                    for (int i = 0; i < outputData.Count; i++)
                    {
                        outputData[i] = new Tuple<float, double>(outputData[i].Item1, outputData[i].Item2 > scheme.MaxValue ? scheme.MaxValue : outputData[i].Item2);
                    }
                }

                using (var fs = File.Create(Path.Combine(directory, scheme.OutputFile)))
                {
                    using (var write = new StreamWriter(fs))
                    {
                        foreach (var tuple in outputData)
                        {
                            write.WriteLine($"{tuple.Item1} {tuple.Item2}");
                        }
                    }
                }
            }

        }

        public static void ScaleData(SimulationParameters Parameters)
        {
            // TODO: scale data.
            foreach (var plot in Parameters.Plots)
            {
                foreach (var attribute in plot.Attributes)
                {
                    if (attribute.ScaleData)
                    {
                        var filename = $"{Parameters.EscapedName}_T{attribute.GetFileName()}.txt";
                        var dir = $"{Properties.Settings.Default.NS3Dir}\\build\\scratch\\out\\";
                        if (!File.Exists(Path.Combine(dir, filename)))
                        {
                            using (var w = new StreamWriter(Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName(true)}_conf.ini")))
                            {
                                w.WriteLine("double double none");
                                w.WriteLine($"double double {attribute.ScaleDataCommand}");
                            }

                            //System.Windows.Forms.MessageBox.Show($"\"{Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName(true)}.txt")}\"");
                            var startInfo = new ProcessStartInfo("DataScalar.exe", $"\"{Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName(true)}.txt")}\"");
                            using (var process = new Process())
                            {
                                process.StartInfo = startInfo;
                                process.Start();
                                process.WaitForExit();
                            }

                            File.Move(Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName(true)}_out.txt"), Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName()}.txt"));
                            File.Move(Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName(true)}_conf.ini"), Path.Combine(dir, $"{Parameters.EscapedName}_T{attribute.GetFileName()}_conf.ini"));
                        }
                    }
                }
            }
        }
    }
}
