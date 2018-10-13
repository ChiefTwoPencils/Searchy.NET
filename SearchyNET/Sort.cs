using System.Collections.Generic;
using System.Linq;

namespace SearchyNET
{
    public class Sort
    {
        public enum Direction
        {
            Asc, Desc
        }

        public Direction SortDirection { get; set; }
        public ISelector Selector { get; set; }

        public IEnumerable<ISelectable> Apply(IEnumerable<ISelectable> data)
        {
            return SortDirection == Direction.Asc 
                ? data.OrderBy(Selector.Select) 
                : data.OrderByDescending(Selector.Select);
        }
    }
}