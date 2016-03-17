using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetML
{
    public class Canvas : Control
    {
        public List<IDrawable> Items;
        public IDrawableSorter Sorter;

        public Canvas()
        {
            Items = new List<IDrawable>();
        }

        public void AddItem(IDrawable Item)
        {
            Items.Add(Item);

            if (Sorter != null)
            {
                Sorter.Sort(Items);
            }

            UpdateItem(Item);
        }

        public void UpdateItem(IDrawable Item)
        {
            using (var region = new Region(Item.Bounds()))
            {
                Invalidate(region);
            }
        }


        public IDrawable GetItem(Point P) => GetItem(P.X, P.Y);

        public IDrawable GetItem(int X, int Y)
        {
            foreach (var item in Items)
            {
                if (item.Bounds().Contains(X, Y))
                {
                    return item;
                }
            }

            return null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (var item in Items)
            {
                if (e.ClipRectangle.IntersectsWith(item.Bounds()))
                {
                    item.Draw(e.Graphics);
                }
            }
        }
    }
}
