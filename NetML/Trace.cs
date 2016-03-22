using System.Collections.Generic;

namespace NetML
{
    public class Trace
    {
        public string Name;
        public float StartTime;
        public float EndTime;
        public List<TraceAttribute> Attributes;

        public Trace()
        { }

        public Trace(Trace Other)
        {
            this.Name = Other.Name;
            this.StartTime = Other.StartTime;
            this.EndTime = Other.EndTime;

            if (Other.Attributes != null)
            {
                this.Attributes = new List<TraceAttribute>();
                foreach (var attribute in Other.Attributes)
                {
                    this.Attributes.Add(new TraceAttribute(attribute, this));
                }
            }
        }

        public void Set(Trace Other)
        {
            this.Name = Other.Name;
            this.StartTime = Other.StartTime;
            this.EndTime = Other.EndTime;

            if (Other.Attributes != null)
            {
                this.Attributes = new List<TraceAttribute>();
                foreach (var attribute in Other.Attributes)
                {
                    this.Attributes.Add(new TraceAttribute(attribute, this));
                }
            }
        }
    }
}
