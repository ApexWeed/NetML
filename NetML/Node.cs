using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public int X;
        public int Y;
        public string Name;
        public NodeType Type;

        public void Draw(Graphics g)
        {
            var textWidth = (int)g.MeasureString(Name, SystemFonts.DefaultFont).Width;
            var textHeight = (int)g.MeasureString(Name, SystemFonts.DefaultFont).Height;
            g.DrawEllipse(Pens.Black, new Rectangle(X - textWidth / 2, Y - textWidth / 2, textWidth, textWidth));
            g.DrawString(Name, SystemFonts.DefaultFont, Brushes.Black, new Point(X - textWidth / 2, Y - textHeight / 2));
        }

        public Rectangle Bounds()
        {
            var textWidth = (int)TextRenderer.MeasureText(Name, SystemFonts.DefaultFont).Width;
            var textHeight = (int)TextRenderer.MeasureText(Name, SystemFonts.DefaultFont).Height;
            return new Rectangle((int)(X - (textWidth / 2) * 1.2), (int)(Y - (textWidth / 2) * 1.2), (int)(textWidth * 1.2), (int)(textWidth * 1.2));
        }
    }
}
