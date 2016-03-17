using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML
{
    public class NetMLSorter : IDrawableSorter
    {
        public void Sort(List<IDrawable> Items)
        {
            var sorted = new List<IDrawable>();
            var toSort = Items;

            for (int i = 0; i < toSort.Count; i++)
            {

            }

            throw new NotImplementedException();
        }
    }
}
