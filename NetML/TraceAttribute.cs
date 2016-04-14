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
        public bool LinkReverse;
        public Increment IncrementMode;
        [Newtonsoft.Json.JsonIgnore]
        public string AttributeType
        {
            get
            {
                switch (TraceSource)
                {
                    case "Tx":
                        {
                            if (Element is Stream)
                            {
                                return "Ptr<const Packet> pkt";
                            }
                            if (Element is Node)
                            {
                                return "Ptr<const Packet> pkt, Ptr<Ipv4> ip, uint32_t length";
                            }
                            break;
                        }
                    case "Rx":
                        {
                            if (Element is Stream)
                            {
                                return "Ptr<const Packet> pkt, const Address & addr";
                            }
                            if (Element is Node)
                            {
                                return "Ptr<const Packet> pkt, Ptr<Ipv4> ip, uint32_t length";
                            }
                            break;
                        }
                    case "CongestionWindow":
                        {
                            return "uint32_t oldval, uint32_t newval";
                        }
                    case "MacTx":
                    case "MacTxDrop":
                    case "MacTxBackoff":
                    case "MacPromiscRx":
                    case "MacRx":
                    case "MacRxDrop":
                    case "PhyTxBegin":
                    case "PhyTxEnd":
                    case "PhyTxDrop":
                    case "PhyRxBegin":
                    case "PhyRxEnd":
                    case "PhyRxDrop":
                    case "Enqueue":
                    case "Dequeue":
                    case "Drop":
                        {
                            if (Element is Node)
                            {
                                return "Ptr<const Packet> pkt";
                            }
                            if (Element is Link)
                            {
                                return "Ptr<const Packet> pkt";
                            }
                            break;
                        }
                    case "SendOutgoing":
                    case "UnicastForward":
                    case "LocalDeliver":
                        {
                            if (Element is Node)
                            {
                                return "const Ipv4Header & hdr, Ptr<const Packet> pkt, uint32_t length";
                            }
                            break;
                        }
                }
                return "Ptr<const Packet> pkt";
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public string Code
        {
            get
            {
                if (TraceSource == "CongestionWindow")
                {
                    return "newval";
                }

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
            this.Set(Other, Parent);
        }

        public void Set(TraceAttribute Other, Trace Parent)
        {
            this.CustomCode = Other.CustomCode;
            this.Element = Other.Element;
            this.IncrementMode = Other.IncrementMode;
            this.Parent = Parent;
            this.TraceSource = Other.TraceSource;
            this.LinkReverse = Other.LinkReverse;
        }

        public override string ToString()
        {
            var elementName = (Element == null ? "" : (Element is Node ? (Element as Node).Name : (Element is Link ? (Element as Link).Name : (Element as Stream).Name)));
            return $"{elementName}.{TraceSource} -> {Code}";
            //return $"T{(Element is Node ? (Element as Node).Name : (Element as Link).Name)}_{Parent.Name}";
        }
    }
}
