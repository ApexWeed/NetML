using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Apex.Extensions;

namespace NetML
{
    public class Domain : IDrawable
    {
        public enum DomainType
        {
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

        private static Brush CSMABackgroundBrush;
        private static Brush IEEE81211BackgroundBrush;
        private static Brush LTEBackgroundBrush;
        private static Brush WimaxBackgroundBrush;
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

        // Domain fields.
        public string Name;
        public string Text
        {
            get
            {
                switch (DisplayProperties.DomainDisplayMode)
                {
                    case DisplayProperties.DomainDisplay.Name:
                        return Name;
                    case DisplayProperties.DomainDisplay.DomainType:
                        return Type.ToString();
                    case DisplayProperties.DomainDisplay.MobilityModel:
                        return (Type == DomainType.IEEE81211 || Type == DomainType.LTE || Type == DomainType.Wimax) ? Model.ToString() : Name;
                    case DisplayProperties.DomainDisplay.WifiStandard:
                        return Standard.ToString();
                    case DisplayProperties.DomainDisplay.WifiMode:
                        return Mode.ToString();
                    case DisplayProperties.DomainDisplay.SchedulerType:
                        return Scheduler.ToString();
                    case DisplayProperties.DomainDisplay.BaseAddress:
                        return BaseAddress;
                    case DisplayProperties.DomainDisplay.Walk:
                        return ((Type == DomainType.IEEE81211 || Type == DomainType.LTE || Type == DomainType.Wimax) && Model == MobilityModel.RandomWalk) ? $"{{{XMin}, {XMax}, {YMin}, {YMax}}}" : Name;
                    case DisplayProperties.DomainDisplay.DataRate:
                        return Type == DomainType.CSMA ? DataRate.ToString() : Name;
                    case DisplayProperties.DomainDisplay.Delay:
                        return Type == DomainType.CSMA ? Delay.ToString() : Name;
                }
                return Name;
            }
        }
        public List<Node> Nodes;
        public float Radius;

        public DomainType Type;
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

        public Domain()
        {
            Position = new Point();
            Nodes = new List<Node>();

            Type = DomainType.CSMA;
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

            if (CSMABackgroundBrush == null)
            {
                CSMABackgroundBrush = new SolidBrush(Color.FromArgb(064, 048, 128, 048));
            }
            if (IEEE81211BackgroundBrush == null)
            {
                IEEE81211BackgroundBrush = new SolidBrush(Color.FromArgb(064, 064, 000, 128));
            }
            if (LTEBackgroundBrush == null)
            {
                LTEBackgroundBrush = new SolidBrush(Color.FromArgb(064, 048, 048, 128));
            }
            if (WimaxBackgroundBrush == null)
            {
                WimaxBackgroundBrush = new SolidBrush(Color.FromArgb(064, 128, 048, 048));
            }

            if (TextBrush == null)
            {
                TextBrush = Brushes.Black;
            }
        }

        public void Update()
        {

        }

        public void Draw(Graphics g)
        {
            var BackgroundBrush = CSMABackgroundBrush;
            switch (Type)
            {
                case DomainType.CSMA:
                    {
                        BackgroundBrush = CSMABackgroundBrush;
                        break;
                    }
                case DomainType.IEEE81211:
                    {
                        BackgroundBrush = IEEE81211BackgroundBrush;
                        break;
                    }
                case DomainType.LTE:
                    {
                        BackgroundBrush = LTEBackgroundBrush;
                        break;
                    }
                case DomainType.Wimax:
                    {
                        BackgroundBrush = WimaxBackgroundBrush;
                        break;
                    }
            }

            if (Nodes.Count == 0)
            {
                if (Radius > 0)
                {
                    g.FillEllipse(BackgroundBrush, new Rectangle((int)(X - Radius), (int)(Y - Radius), (int)(Radius * 2), (int)(Radius * 2)));
                }
            }
            else
            {
                if (DisplayProperties.RenderDomain)
                {
                    var nodeRadius = 0.0d;
                    foreach (var node in Nodes)
                    {
                        var dist = Position.Distance(node.Position);
                        if (dist > nodeRadius)
                        {
                            nodeRadius = dist;
                        }
                    }

                    g.FillEllipse(BackgroundBrush, new Rectangle((int)(X - nodeRadius), (int)(Y - nodeRadius), (int)(nodeRadius * 2), (int)(nodeRadius * 2)));
                }
            }
            
            if (DisplayProperties.RenderDomainText)
            {
                var textWidth = (int)g.MeasureString(Text, SystemFonts.DefaultFont).Width;
                var textHeight = (int)g.MeasureString(Text, SystemFonts.DefaultFont).Height;
                var diameter = textWidth > textHeight ? textWidth : textHeight;

                if (diameter < 35)
                {
                    diameter = 35;
                }
                g.FillEllipse(BackgroundBrush, new Rectangle((int)(X - diameter / 2), (int)(Y - diameter / 2), diameter, diameter));
                g.DrawString(Text, SystemFonts.DefaultFont, Brushes.Black, new Point((int)(X - textWidth / 2), (int)(Y - textHeight / 2)));
            }
        }

        public Rectangle DrawableBounds()
        {
            if (DisplayProperties.RenderDomain)
            {
                var textWidth = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Width;
                var textHeight = TextRenderer.MeasureText(Text, SystemFonts.DefaultFont).Height;
                var diameter = textWidth > textHeight ? textWidth : textHeight;

                if (Nodes.Count == 0)
                {
                    if (diameter < Radius * 2)
                    {
                        diameter = (int)(Radius * 2);
                    }
                }
                else
                {
                    var nodeRadius = 0.0d;
                    foreach (var node in Nodes)
                    {
                        var dist = Position.Distance(node.Position);
                        if (dist > nodeRadius)
                        {
                            nodeRadius = dist;
                        }
                    }
                    if (diameter < nodeRadius * 2)
                    {
                        diameter = (int)(nodeRadius * 2);
                    }
                }

                if (diameter < 35)
                {
                    diameter = 35;
                }
                return new Rectangle((int)(X - (diameter / 2) * 1.2), (int)(Y - (diameter / 2) * 1.2), (int)(diameter * 1.2), (int)(diameter * 1.2));
            }
            else
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
            throw new NotImplementedException();
        }

        public Rectangle CollisionBounds()
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
    }
}
