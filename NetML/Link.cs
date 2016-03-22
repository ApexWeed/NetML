using System;
using System.Drawing;
using System.Windows.Forms;
using Apex.Extensions;

namespace NetML
{
    public class Link : IDrawable
    {
        public enum LinkMode
        {
            Packets,
            Bytes
        }

        public enum LinkType
        {
            Optical,
            OpticalEthernet
        }

        public enum QueueType
        {
            DropTailQueue,
            RandomEarlyDiscard
        }

        private static Pen LinePen;
        private static Pen OutlinePen;
        private static Brush ArrowBrush;
        private static Brush BackgroundBrush;
        private static Brush TextBrush;

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

        // Link fields.
        [Newtonsoft.Json.JsonConverter(typeof(NodeConverter))]
        public Node StartNode;
        [Newtonsoft.Json.JsonConverter(typeof(NodeConverter))]
        public Node EndNode;
        public string Name;
        public string Text
        {
            get
            {
                switch (DisplayProperties.LinkDisplayMode)
                {
                    case DisplayProperties.LinkDisplay.Name:
                        return Name;
                    case DisplayProperties.LinkDisplay.Duplex:
                        return $"Duplex: {Duplex}";
                    case DisplayProperties.LinkDisplay.Speed:
                        return DataRate;
                    case DisplayProperties.LinkDisplay.Delay:
                        return Delay;
                    case DisplayProperties.LinkDisplay.BaseAddress:
                        return BaseAddress;
                    case DisplayProperties.LinkDisplay.LinkMode:
                        return Mode.ToString();
                    case DisplayProperties.LinkDisplay.MaxData:
                        return Mode == LinkMode.Bytes ? $"{MaxBytes} bytes" : $"{MaxPackets} pkts";
                    case DisplayProperties.LinkDisplay.LinkType:
                        return Type.ToString();
                    case DisplayProperties.LinkDisplay.QueueType:
                        return Queue.ToString();
                }
                return Name;
            }
        }

        public bool Duplex;
        public string DataRate;
        public string Delay;
        public int Mtu;
        public string BaseAddress;
        public string Mask;
        public LinkMode Mode;
        public int MaxPackets;
        public int MaxBytes;
        public LinkType Type;
        public QueueType Queue;

        // RED fields.
        public int MeanPacketSize;
        public int IdlePacketSize;
        public bool Gentle;
        public bool Wait;
        public float QW;
        public int Linterm;
        public string LinkBandwidth;
        public string LinkDelay;
        public int MinTh;
        public int MaxTh;
        public int QueueLimit;

        public Link()
        {
            Position = new Point();

            Duplex = true;
            DataRate = "10Mbps";
            Delay = "10ms";
            Mtu = 1500;
            BaseAddress = "10.0.0.0";
            Mask = "255.255.255.0";
            Mode = LinkMode.Packets;
            MaxPackets = 200;
            MaxBytes = 100000;
            Type = LinkType.Optical;
            Queue = QueueType.DropTailQueue;

            // RED fields.
            MeanPacketSize = 1000;
            IdlePacketSize = 0;
            Gentle = false;
            Wait = false;
            QW = 0.002f;
            Linterm = 50;
            LinkBandwidth = "1500000bps";
            LinkDelay = "20ms";
            MinTh = 5;
            MaxTh = 15;
            QueueLimit = 200;

            if (OutlinePen == null)
            {
                OutlinePen = Pens.Black;
            }
            if (LinePen == null)
            {
                LinePen = new Pen(Color.Blue, 3);
            }
            if (ArrowBrush == null)
            {
                ArrowBrush = Brushes.Blue;
            }
            if (BackgroundBrush == null)
            {
                BackgroundBrush = Brushes.LightSkyBlue;
            }
            if (TextBrush == null)
            {
                TextBrush = Brushes.Black;
            }
        }

        public Rectangle DrawableBounds()
        {
            if (StartNode != null && EndNode != null)
            {
                return new Rectangle(0, 0, 2000, 2000);
                //return new Rectangle().FromPoints(new Point[] { new Point(X, Y), new Point(Start.X, Start.Y), new Point(End.X, End.Y) });
            }
            else if (StartNode != null)
            {
                return new PointF(StartNode.X, StartNode.Y).GetRect(new PointF(X, Y));
            }
            else
            {
                var textWidth = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Width;
                var textHeight = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Height;
                return new Rectangle((int)(X - 1 - (textWidth / 2) * 1.2), (int)(Y - 1 - (textHeight / 2) * 1.3), (int)(textWidth * 1.3 + 2), (int)(textHeight * 1.3 + 2));
            }
        }

        public void Draw(Graphics g)
        {
            var textWidth = g.MeasureString(Text, SystemFonts.DefaultFont).Width;
            var textHeight = g.MeasureString(Text, SystemFonts.DefaultFont).Height;
            if (StartNode != null && EndNode != null)
            {
                if (DisplayProperties.RenderLink)
                {
                    // Full rendering.
                    g.DrawCurve(LinePen, new PointF[] { new PointF(StartNode.X, StartNode.Y), new PointF(X, Y), new PointF(EndNode.X, EndNode.Y) });

                    // Arrows to show direction and duplex.
                    // Get line angles.
                    var lineAngle = StartNode.Position.Angle(EndNode.Position);
                    var triAngle1 = lineAngle + (Math.PI / 2);
                    var triAngle2 = lineAngle - (Math.PI / 2);

                    // Convert angles to direction vectors.
                    var lineUnit = new PointF((float)Math.Cos(lineAngle), (float)Math.Sin(lineAngle));
                    var triUnit1 = new PointF((float)Math.Cos(triAngle1), (float)Math.Sin(triAngle1));
                    var triUnit2 = new PointF((float)Math.Cos(triAngle2), (float)Math.Sin(triAngle2));

                    // Extend unit vectors to desired length.
                    var triPoint1 = triUnit1.Multiply(6);
                    var triPoint2 = triUnit2.Multiply(6);
                    var triPoint3 = lineUnit.Multiply(12);

                    var mid = StartNode.Position.Mid(EndNode.Position);
                    var offset = lineUnit.Multiply(textWidth / 1.8f);

                    var points = new PointF[]
                    {
                    new PointF(X + offset.X + triPoint1.X, Y + offset.Y + triPoint1.Y),
                    new PointF(X + offset.X + triPoint2.X, Y + offset.Y + triPoint2.Y),
                    new PointF(X + offset.X + triPoint3.X, Y + offset.Y + triPoint3.Y)
                    };

                    g.FillPolygon(ArrowBrush, points);

                    // Only show one arrow if the link isn't duplex.
                    if (Duplex)
                    {
                        triPoint3 = lineUnit.Multiply(-10);
                        offset = lineUnit.Multiply(textWidth / -1.8f);

                        points = new PointF[]
                        {
                        new PointF(X + offset.X + triPoint1.X, Y + offset.Y + triPoint1.Y),
                        new PointF(X + offset.X + triPoint2.X, Y + offset.Y + triPoint2.Y),
                        new PointF(X + offset.X + triPoint3.X, Y + offset.Y + triPoint3.Y)
                        };

                        g.FillPolygon(ArrowBrush, points);
                    }
                }
            }
            else if (StartNode != null)
            {
                if (DisplayProperties.RenderLink)
                {
                    // Only render arrow from start to current location.
                    g.DrawLine(LinePen, StartNode.X, StartNode.Y, X, Y);
                }
                return;
            }

            if (DisplayProperties.RenderLinkText)
            {
                // Only render the link itself.
                g.FillRectangle(BackgroundBrush, new Rectangle((int)(X - textWidth / 2), (int)(Y - textHeight / 2), (int)textWidth, (int)textHeight));
                g.DrawRectangle(OutlinePen, new Rectangle((int)(X - textWidth / 2), (int)(Y - textHeight / 2), (int)textWidth, (int)textHeight));
                g.DrawString(Text, SystemFonts.DefaultFont, TextBrush, new Point((int)(X - textWidth / 2), (int)(Y - textHeight / 2)));
            }
        }

        public Rectangle CollisionBounds()
        {
            var textWidth = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Width;
            var textHeight = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Height;
            return new Rectangle((int)(X - 1 - (textWidth / 2) * 1.2), (int)(Y - 1 - (textHeight / 2) * 1.3), (int)(textWidth * 1.3 + 2), (int)(textHeight * 1.3 + 2));
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
