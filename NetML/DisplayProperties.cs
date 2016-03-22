using System.Collections.Generic;

namespace NetML
{
    public static class DisplayProperties
    {
        public enum NodeDisplay
        {
            Name,
            NodeType,
            MobilityModel,
            WifiStandard,
            WifiMode,
            SchedulerType,
            BaseAddress,
            Walk,
            DataRate,
            Delay
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

        public static NodeDisplay NodeDisplayMode;
        public static LinkDisplay LinkDisplayMode;
        public static StreamDisplay StreamDisplayMode;
        public static bool RenderNode;
        public static bool RenderNodeText;
        public static bool RenderLinkText;
        public static bool RenderLink;
        public static bool RenderStreamText;
        public static bool RenderStream;

        public static void Reset()
        {
            NodeDisplayMode = NodeDisplay.Name;
            LinkDisplayMode = LinkDisplay.Name;
            StreamDisplayMode = StreamDisplay.Name;

            RenderNode = true;
            RenderNodeText = true;
            RenderLink = true;
            RenderLinkText = true;
            RenderStream = true;
            RenderStreamText = true;
        }
    }
}
