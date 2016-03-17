using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML
{
    public interface IDrawableSorter
    {
        void Sort(List<IDrawable> Items);
    }
}
