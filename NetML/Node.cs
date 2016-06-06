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
                }
                return Name;
            }
        }

        public Node()
        {
            Position = new Point();
        }

        public void Draw(Graphics g)
        {
            var textWidth = (int)g.MeasureString(Text, SystemFonts.DefaultFont).Width;
            var textHeight = (int)g.MeasureString(Text, SystemFonts.DefaultFont).Height;
            var diameter = textWidth > textHeight ? textWidth : textHeight;
            if (DisplayProperties.RenderNode)
            {
                if (diameter < 35)
                {
                    diameter = 35;
                }
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
            if (diameter < 35)
            {
                diameter = 35;
            }
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
