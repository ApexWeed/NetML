using System.Collections.Generic;

namespace NetML
{
    public class NetMLSorter : IDrawableSorter
    {
        public void Sort(List<IDrawable> Items)
        {
            var sorted = new List<IDrawable>();

            // Put links first (drawn lowest).
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Link)
                {
                    sorted.Add(Items[i]);
                    Items.RemoveAt(i--);
                }
            }

            // Put streams second (drawn second lowest).
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Stream)
                {
                    sorted.Add(Items[i]);
                    Items.RemoveAt(i--);
                }
            }

            // Put nodes last (drawn highest).
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Node)
                {
                    sorted.Add(Items[i]);
                    Items.RemoveAt(i--);
                }
            }

            Items.AddRange(sorted);
        }
    }
}
