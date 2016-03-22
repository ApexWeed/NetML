using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            DoubleBuffered = true;
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

        public void AddItems(IEnumerable<IDrawable> Items)
        {
            this.Items.AddRange(Items);

            if (Sorter != null)
            {
                Sorter.Sort(this.Items);
            }

            Invalidate();
        }

        public void UpdateItem(IDrawable Item)
        {
            using (var region = new Region(Item.DrawableBounds()))
            {
                Invalidate(region);
            }
        }


        public IDrawable GetItem(Point P) => GetItem(P.X, P.Y);

        public IDrawable GetItem(int X, int Y)
        {
            foreach (var item in Items)
            {
                if (item.CollisionBounds().Contains(X, Y))
                {
                    return item;
                }
            }

            return null;
        }

        public IDrawable GetItem(Point P, Func<IDrawable, bool> Selector) => GetItem(P.X, P.Y, Selector);

        public IDrawable GetItem(int X, int Y, Func<IDrawable, bool> Selector)
        {
            foreach (var item in Items.Where(Selector))
            {
                if (item.CollisionBounds().Contains(X, Y))
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
                if (e.ClipRectangle.IntersectsWith(item.DrawableBounds()))
                {
                    item.Draw(e.Graphics);
                }
            }
        }
    }
}
