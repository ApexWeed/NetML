using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NetML
{
    public class SimulationParameters
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                escapedName = new Regex($"[{Path.GetInvalidFileNameChars()}]").Replace(Name, "");
            }
        }
        private string escapedName;
        [Newtonsoft.Json.JsonIgnore]
        public string EscapedName
        {
            get { return escapedName; }
        }
        public bool PrintAttributes;
        public bool AsciiTrace;
        public float ObservationStartTime;
        public float ObservationStopTime;
        private List<ComponentLog> componentLogs;
        public List<ComponentLog> ComponentLogs
        {
            get
            {
                if (componentLogs == null)
                {
                    componentLogs = new List<ComponentLog>();
                }
                return componentLogs;
            }
            set { componentLogs = value; }
        }
        private List<Trace> traces;
        public List<Trace> Traces
        {
            get
            {
                if (traces == null)
                {
                    traces = new List<Trace>();
                }
                return traces;
            }
            set { traces = value; }
        }
        private List<Plot> plots;
        public List<Plot> Plots
        {
            get
            {
                if (plots == null)
                {
                    plots = new List<Plot>();
                }
                return plots;
            }
            set { plots = Plots; }
        }
    }
}
