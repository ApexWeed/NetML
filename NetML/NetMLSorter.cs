using System.Collections.Generic;

namespace NetML
{
    public class NetMLSorter : IDrawableSorter
    {
        public void Sort(List<IDrawable> Items)
        {
            var sorted = new List<IDrawable>();

            // Put domains first (drawn lowest).
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Domain)
                {
                    sorted.Add(Items[i]);
                    Items.RemoveAt(i--);
                }
            }

            // Put links second (drawn second lowest).
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is Link)
                {
                    sorted.Add(Items[i]);
                    Items.RemoveAt(i--);
                }
            }

            // Put streams third (drawn third lowest).
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
