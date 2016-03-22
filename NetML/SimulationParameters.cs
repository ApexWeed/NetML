using System.Collections.Generic;

namespace NetML
{
    public class SimulationParameters
    {
        public string Name;
        public bool PrintAttributes;
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
    }
}
