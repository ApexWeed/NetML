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
            BulkFTP,
            OnOff
        }

        public enum Distribution
        {
            Constant,
            Exponential
        }

        public enum Protocol
        {
            Tcp,
            Udp
        }

        private static Pen BulkFTPLinePen;
        private static Pen UDPPingLinePen;
        private static Pen OnOffLinePen;
        private static Pen OutlinePen;
        private static Brush BulkFTPArrowBrush;
        private static Brush UDPPingArrowBrush;
        private static Brush OnOffArrowBrush;
        private static Brush BulkFTPBackgroundBrush;
        private static Brush UDPPingBackgroundBrush;
        private static Brush OnOffBackgroundBrush;
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

        // OnOff fields.
        public string OnCBRRate;
        public Distribution OnDistribution;
        public float OnInterval;
        public Distribution OffDistribution;
        public float OffInterval;
        public Protocol TransportProtocol;

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

            OnCBRRate = "1Mbps";
            OnDistribution = Distribution.Constant;
            OnInterval = 1.0f;
            OffDistribution = Distribution.Constant;
            OffInterval = 1.0f;
            TransportProtocol = Protocol.Tcp;

            StartSendBufferSize = 100000;
            StartReceiveBufferSize = 100000;
            StartMaxWindowSize = 65535;
            EndSendBufferSize = 100000;
            EndReceiveBufferSize = 100000;
            EndMaxWindowSize = 65535;

            if (BulkFTPLinePen == null)
            {
                BulkFTPLinePen = new Pen(Color.FromArgb(128, 064, 192, 064), 6);
            }
            if (UDPPingLinePen == null)
            {
                UDPPingLinePen = new Pen(Color.FromArgb(128, 128, 032, 128), 6);
            }
            if (OnOffLinePen == null)
            {
                OnOffLinePen = new Pen(Color.FromArgb(128, 064, 064, 192), 6);
            }

            if (OutlinePen == null)
            {
                OutlinePen = Pens.Black;
            }

            if (BulkFTPArrowBrush == null)
            {
                BulkFTPArrowBrush = new SolidBrush(Color.FromArgb(128, 064, 192, 064));
            }
            if (UDPPingArrowBrush == null)
            {
                UDPPingArrowBrush = new SolidBrush(Color.FromArgb(128, 128, 032, 128));
            }
            if (OnOffArrowBrush == null)
            {
                OnOffArrowBrush = new SolidBrush(Color.FromArgb(128, 064, 064, 192));
            }
            if (BulkFTPBackgroundBrush == null)
            {
                BulkFTPBackgroundBrush = new SolidBrush(Color.FromArgb(064, 048, 128, 048));
            }
            if (UDPPingBackgroundBrush == null)
            {
                UDPPingBackgroundBrush = new SolidBrush(Color.FromArgb(064, 064, 000, 128));
            }
            if (OnOffBackgroundBrush == null)
            {
                OnOffBackgroundBrush = new SolidBrush(Color.FromArgb(064, 048, 048, 128));
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
            var linePen = BulkFTPLinePen;
            var arrowBrush = BulkFTPArrowBrush;
            var backgroundBrush = BulkFTPBackgroundBrush;

            switch (Type)
            {
                case StreamType.UDPPing:
                    {
                        linePen = UDPPingLinePen;
                        arrowBrush = UDPPingArrowBrush;
                        backgroundBrush = UDPPingBackgroundBrush;
                        break;
                    }
                case StreamType.BulkFTP:
                    {
                        linePen = BulkFTPLinePen;
                        arrowBrush = BulkFTPArrowBrush;
                        backgroundBrush = BulkFTPBackgroundBrush;
                        break;
                    }
                case StreamType.OnOff:
                    {
                        linePen = OnOffLinePen;
                        arrowBrush = OnOffArrowBrush;
                        backgroundBrush = OnOffBackgroundBrush;
                        break;
                    }
            }

            var textWidth = g.MeasureString(Text, SystemFonts.DefaultFont).Width;
            var textHeight = g.MeasureString(Text, SystemFonts.DefaultFont).Height;
            if (StartNode != null && EndNode != null)
            {
                if (DisplayProperties.RenderStream)
                {
                    // Full rendering.
                    g.DrawCurve(linePen, new PointF[] { new PointF(StartNode.X, StartNode.Y), new PointF(X, Y), new PointF(EndNode.X, EndNode.Y) });

                    // Arrows to show direction.
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

                    g.FillPolygon(arrowBrush, points);
                }
            }
            else if (StartNode != null)
            {
                // Only render arrow from start to current location.
                if (DisplayProperties.RenderStream)
                {
                    g.DrawLine(linePen, StartNode.X, StartNode.Y, X, Y);
                }
                return;
            }

            if (DisplayProperties.RenderStreamText)
            {
                // Only render the stream itself.
                g.FillRectangle(backgroundBrush, new Rectangle((int)(X - textWidth / 2), (int)(Y - textHeight / 2), (int)textWidth, (int)textHeight));
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
