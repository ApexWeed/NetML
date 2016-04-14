using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NetML
{
    public class Trace
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
        [Newtonsoft.Json.JsonProperty]
        private bool commonStartTime;
        [Newtonsoft.Json.JsonIgnore]
        public bool CommonStartTime
        {
            get
            {
                if (Attributes != null && Attributes.Count > 0)
                {
                    foreach (var attribute in Attributes)
                    {
                        if (attribute.TraceSource == "CongestionWindow")
                        {
                            return false;
                        }
                    }
                }
                return commonStartTime;
            }
            set { commonStartTime = value; }
        }
        [Newtonsoft.Json.JsonProperty]
        private float startTime;
        [Newtonsoft.Json.JsonIgnore]
        public float StartTime
        {
            get
            {
                if (Attributes != null && Attributes.Count > 0)
                {
                    foreach (var attribute in Attributes)
                    {
                        if (attribute.TraceSource == "CongestionWindow")
                        {
                            if ((attribute.Element as Stream).StartTime + 0.01f > startTime)
                            {
                                return (attribute.Element as Stream).StartTime + 0.01f;
                            }
                        }
                    }
                }
                return startTime;
            }
            set { startTime = value; }
        }
        [Newtonsoft.Json.JsonProperty]
        private bool commonEndTime;
        [Newtonsoft.Json.JsonIgnore]
        public bool CommonEndTime
        {
            get { return commonEndTime; }
            set { commonEndTime = value; }
        }
        [Newtonsoft.Json.JsonProperty]
        private float endTime;
        [Newtonsoft.Json.JsonIgnore]
        public float EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        public List<TraceAttribute> Attributes;

        public Trace()
        {
            CommonEndTime = true;
            CommonStartTime = true;
        }

        public Trace(Trace Other)
        {
            this.Set(Other);
        }

        public void Set(Trace Other)
        {
            this.Name = Other.Name;
            this.startTime = Other.startTime;
            this.endTime = Other.endTime;
            this.commonStartTime = Other.commonStartTime;
            this.commonEndTime = Other.commonEndTime;

            if (Other.Attributes != null)
            {
                this.Attributes = new List<TraceAttribute>();
                foreach (var attribute in Other.Attributes)
                {
                    this.Attributes.Add(new TraceAttribute(attribute, this));
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
