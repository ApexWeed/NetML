using System.Drawing;
using System.Windows.Forms;

namespace NetML
{
    public class Node : IDrawable
    {
        public enum NodeType
        {
            Node,
            CSMA,
            IEEE81211,
            LTE,
            Wimax
        }

        public enum MobilityModel
        {
            RandomWalk,
            FixedPosition
        }

        public enum WifiStandard
        {
            IEEE80211a,
            IEEE80211b,
            IEEE80211g,
            IEEE80211n24G,
            IEEE80211n5G
        }

        public enum WifiMode
        {
            Infrastructure,
            AdHoc
        }

        public enum SchedulerType
        {
            Simple,
            MBQOS,
            RTPS
        }

        // IDrawable fields.
        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }
        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }
        public PointF Position;

        // Node fields.
        public string Name;
        public string Text
        {
            get
            {
                switch (DisplayProperties.NodeDisplayMode)
                {
                    case DisplayProperties.NodeDisplay.Name:
                        return Name;
                    case DisplayProperties.NodeDisplay.NodeType:
                        return Type.ToString();
                    case DisplayProperties.NodeDisplay.MobilityModel:
                        return (Type == NodeType.IEEE81211 || Type == NodeType.LTE || Type == NodeType.Wimax) ? Model.ToString() : Name;
                    case DisplayProperties.NodeDisplay.WifiStandard:
                        return Standard.ToString();
                    case DisplayProperties.NodeDisplay.WifiMode:
                        return Mode.ToString();
                    case DisplayProperties.NodeDisplay.SchedulerType:
                        return Scheduler.ToString();
                    case DisplayProperties.NodeDisplay.BaseAddress:
                        return BaseAddress;
                    case DisplayProperties.NodeDisplay.Walk:
                        return ((Type == NodeType.IEEE81211 || Type == NodeType.LTE || Type == NodeType.Wimax) && Model == MobilityModel.RandomWalk) ? $"{{{XMin}, {XMax}, {YMin}, {YMax}}}" : Name;
                    case DisplayProperties.NodeDisplay.DataRate:
                        return Type == NodeType.CSMA ? DataRate.ToString() : Name;
                    case DisplayProperties.NodeDisplay.Delay:
                        return Type == NodeType.CSMA ? Delay.ToString() : Name;
                }
                return Name;
            }
        }

        public NodeType Type;
        public string BaseAddress;
        public MobilityModel Model;
        public int XMin;
        public int XMax;
        public int YMin;
        public int YMax;

        // CSMA fields.
        public string DataRate;
        public string Delay;

        // 802.11 fields.
        public WifiStandard Standard;
        public WifiMode Mode;

        // Wimax fields.
        public SchedulerType Scheduler;

        public Node()
        {
            Position = new Point();

            Type = NodeType.Node;
            BaseAddress = "10.0.0.0";
            Model = MobilityModel.RandomWalk;
            XMin = -50;
            XMax = 50;
            YMin = -50;
            YMax = 50;

            DataRate = "10Mbps";
            Delay = "40ms";

            Standard = WifiStandard.IEEE80211a;
            Mode = WifiMode.Infrastructure;

            Scheduler = SchedulerType.Simple;
        }

        public void Draw(Graphics g)
        {
            var textWidth = (int)g.MeasureString(Text, SystemFonts.DefaultFont).Width;
            var textHeight = (int)g.MeasureString(Text, SystemFonts.DefaultFont).Height;
            var diameter = textWidth > textHeight ? textWidth : textHeight;
            if (DisplayProperties.RenderNode)
            {
                g.FillEllipse(Brushes.ForestGreen, new Rectangle((int)(X - diameter / 2), (int)(Y - diameter / 2), diameter, diameter));
                g.DrawEllipse(Pens.Black, new Rectangle((int)(X - diameter / 2), (int)(Y - diameter / 2), diameter, diameter));
            }
            if (DisplayProperties.RenderNodeText)
            {
                g.DrawString(Text, SystemFonts.DefaultFont, Brushes.Black, new Point((int)(X - textWidth / 2), (int)(Y - textHeight / 2)));
            }
        }

        public Rectangle DrawableBounds()
        {
            var textWidth = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Width;
            var textHeight = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Height;
            var diameter = textWidth > textHeight ? textWidth : textHeight;
            return new Rectangle((int)(X - (diameter / 2) * 1.2), (int)(Y - (diameter / 2) * 1.2), (int)(diameter * 1.2), (int)(diameter * 1.2));
        }

        public Rectangle CollisionBounds()
        {
            return DrawableBounds();
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
