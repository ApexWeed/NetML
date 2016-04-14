namespace NetML
{
    public static class DisplayProperties
    {
        public enum NodeDisplay
        {
            Name
        }

        public enum LinkDisplay
        {
            Name,
            Duplex,
            Speed,
            Delay,
            BaseAddress,
            LinkMode,
            MaxData,
            LinkType,
            QueueType
        }

        public enum StreamDisplay
        {
            Name,
            StartTime,
            EndTime,
            PacketSize,
            StreamType,
            MaxUnits,
            Interval,
            Port
        }
        
        public enum DomainDisplay
        {
            Name,
            DomainType,
            MobilityModel,
            WifiStandard,
            WifiMode,
            SchedulerType,
            BaseAddress,
            Walk,
            DataRate,
            Delay
        }

        public static NodeDisplay NodeDisplayMode;
        public static LinkDisplay LinkDisplayMode;
        public static StreamDisplay StreamDisplayMode;
        public static DomainDisplay DomainDisplayMode;
        public static bool RenderNode;
        public static bool RenderNodeText;
        public static bool RenderLinkText;
        public static bool RenderLink;
        public static bool RenderStreamText;
        public static bool RenderStream;
        public static bool RenderDomainText;
        public static bool RenderDomain;

        public static void Reset()
        {
            NodeDisplayMode = NodeDisplay.Name;
            LinkDisplayMode = LinkDisplay.Name;
            StreamDisplayMode = StreamDisplay.Name;
            DomainDisplayMode = DomainDisplay.Name;

            RenderNode = true;
            RenderNodeText = true;
            RenderLink = true;
            RenderLinkText = true;
            RenderStream = true;
            RenderStreamText = true;
            RenderDomain = true;
            RenderDomainText = true;
        }
    }
}
