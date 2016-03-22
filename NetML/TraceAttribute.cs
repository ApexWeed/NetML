namespace NetML
{
    public class TraceAttribute
    {
        public enum Increment
        {
            PacketIncrement,
            PacketDecrement,
            PacketSizeIncrement,
            PacketSizeDecrement,
            Custom
        }

        [Newtonsoft.Json.JsonIgnore]
        public Trace Parent;
        public string TraceSource;
        [Newtonsoft.Json.JsonConverter(typeof(IDrawableConverter))]
        public IDrawable Element;
        public Increment IncrementMode;
        public string AttributeType;
        [Newtonsoft.Json.JsonIgnore]
        public string Code
        {
            get
            {
                switch (IncrementMode)
                {
                    case Increment.PacketIncrement:
                        return $"(T{Parent.Name}++)";
                    case Increment.PacketDecrement:
                        return $"(T{Parent.Name}--)";
                    case Increment.PacketSizeIncrement:
                        return $"(T{Parent.Name} += pkt->GetSize())";
                    case Increment.PacketSizeDecrement:
                        return $"(T{Parent.Name} -= pkt->GetSize())";
                    case Increment.Custom:
                        return CustomCode;
                }
                return CustomCode;
            }
            set { CustomCode = value; }
        }

        [Newtonsoft.Json.JsonProperty]
        private string CustomCode;

        public TraceAttribute()
        { }

        public TraceAttribute(TraceAttribute Other, Trace Parent)
        {
            this.AttributeType = Other.AttributeType;
            this.CustomCode = Other.CustomCode;
            this.Element = Other.Element;
            this.IncrementMode = Other.IncrementMode;
            this.Parent = Parent;
            this.TraceSource = Other.TraceSource;
        }

        public override string ToString()
        {
            var elementName = (Element == null ? "" : (Element is Node ? (Element as Node).Name : (Element is Link ? (Element as Link).Name : (Element as Stream).Name)));
            return $"{elementName}.{TraceSource} -> {Code}";
            //return $"T{(Element is Node ? (Element as Node).Name : (Element as Link).Name)}_{Parent.Name}";
        }
    }
}
