using System;
using System.Drawing;
using System.Windows.Forms;
using Apex.Extensions;

namespace NetML
{
    public class Stream : IDrawable
    {
        public enum StreamType
        {
            UDPPing,
            BulkFTP
        }

        private static Pen FTPLinePen;
        private static Pen UDPLinePen;
        private static Pen OutlinePen;
        private static Brush FTPArrowBrush;
        private static Brush UDPArrowBrush;
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

        // Stream fields.
        [Newtonsoft.Json.JsonConverter(typeof(NodeConverter))]
        public Node StartNode;
        [Newtonsoft.Json.JsonConverter(typeof(NodeConverter))]
        public Node EndNode;
        public string Name;
        public string Text
        {
            get
            {
                switch (DisplayProperties.StreamDisplayMode)
                {
                    case DisplayProperties.StreamDisplay.Name:
                        return Name;
                    case DisplayProperties.StreamDisplay.StartTime:
                        return $"Start: {StartTime}";
                    case DisplayProperties.StreamDisplay.EndTime:
                        return $"End: {EndTime}";
                    case DisplayProperties.StreamDisplay.PacketSize:
                        return PacketSize.ToString();
                    case DisplayProperties.StreamDisplay.StreamType:
                        return Type.ToString();
                    case DisplayProperties.StreamDisplay.MaxUnits:
                        return Type == StreamType.UDPPing ? $"{MaxPackets} pkts" : $"{MaxBytes} bytes";
                    case DisplayProperties.StreamDisplay.Interval:
                        return Type == StreamType.UDPPing ? Interval.ToString() : Name;
                    case DisplayProperties.StreamDisplay.Port:
                        return Type == StreamType.UDPPing ? UDPPort.ToString() : FTPPort.ToString();
                }
                return Name;
            }
        }

        public StreamType Type;
        public float StartTime;
        public float EndTime;
        public int PacketSize;

        // UDPPing fields.
        public int MaxPackets;
        public float Interval;
        public int UDPPort;

        // BulkFTP fields.
        public int MaxBytes;
        public int FTPPort;

        // Per node fields.
        public int StartSendBufferSize;
        public int StartReceiveBufferSize;
        public int StartMaxWindowSize;
        public int EndSendBufferSize;
        public int EndReceiveBufferSize;
        public int EndMaxWindowSize;

        public Stream()
        {
            Position = new PointF();

            Type = StreamType.UDPPing;
            StartTime = 0.1f;
            EndTime = 10f;
            PacketSize = 1000;

            MaxPackets = 10;
            Interval = 0.1f;
            UDPPort = 9;

            MaxBytes = 1000000;
            FTPPort = 21;

            StartSendBufferSize = 100000;
            StartReceiveBufferSize = 100000;
            StartMaxWindowSize = 65535;
            EndSendBufferSize = 100000;
            EndReceiveBufferSize = 100000;
            EndMaxWindowSize = 65535;

            if (OutlinePen == null)
            {
                OutlinePen = Pens.Black;
            }
            if (FTPLinePen == null)
            {
                FTPLinePen = new Pen(Color.FromArgb(128, 064, 192, 064), 6);
            }
            if (UDPLinePen == null)
            {
                UDPLinePen = new Pen(Color.FromArgb(128, 128, 032, 128), 6);
            }
            if (FTPArrowBrush == null)
            {
                FTPArrowBrush = new SolidBrush(Color.FromArgb(128, 064, 192, 064));
            }
            if (UDPArrowBrush == null)
            {
                UDPArrowBrush = new SolidBrush(Color.FromArgb(128, 128, 032, 128));
            }
            if (BackgroundBrush == null)
            {
                BackgroundBrush = new SolidBrush(Color.FromArgb(064, 064, 000, 128));
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
                if (DisplayProperties.RenderStream)
                {
                    // Full rendering.
                    g.DrawCurve(Type == StreamType.BulkFTP ? FTPLinePen : UDPLinePen, new PointF[] { new PointF(StartNode.X, StartNode.Y), new PointF(X, Y), new PointF(EndNode.X, EndNode.Y) });

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
                    var triPoint1 = triUnit1.Multiply(12);
                    var triPoint2 = triUnit2.Multiply(12);
                    var triPoint3 = lineUnit.Multiply(24);

                    var mid = StartNode.Position.Mid(EndNode.Position);
                    var offset = lineUnit.Multiply(-12);

                    var points = new PointF[]
                    {
                    new PointF(X + triPoint1.X + offset.X, Y + triPoint1.Y + offset.Y),
                    new PointF(X + triPoint2.X + offset.X, Y + triPoint2.Y + offset.Y),
                    new PointF(X + triPoint3.X + offset.X, Y + triPoint3.Y + offset.Y)
                    };

                    g.FillPolygon(Type == StreamType.BulkFTP ? FTPArrowBrush : UDPArrowBrush, points);
                }
            }
            else if (StartNode != null)
            {
                // Only render arrow from start to current location.
                if (DisplayProperties.RenderStream)
                {
                    g.DrawLine(Type == StreamType.BulkFTP ? FTPLinePen : UDPLinePen, StartNode.X, StartNode.Y, X, Y);
                }
                return;
            }

            if (DisplayProperties.RenderStreamText)
            {
                // Only render the stream itself.
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
