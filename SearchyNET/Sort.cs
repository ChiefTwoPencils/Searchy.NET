using System.Collections.Generic;
using System.Linq;

namespace SearchyNET
{
    /// <summary>
    /// Represents a sort definition to be performed
    /// on a collection of data.
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Enumerated values for sort directions.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Used to perform an ascending sort.
            /// </summary>
            Asc, 
            /// <summary>
            /// Used to perform a descending sort.
            /// </summary>
            Desc
        }

        /// <summary>
        /// Constructs a sort definition to be applied to collections
        /// of data. 
        /// </summary>
        /// <param name="direction"><see cref="SortDirection"/></param>
        /// <param name="selector"><see cref="Selector"/></param>
        public Sort(Direction direction, ISelector selector)
        {
            SortDirection = direction;
            Selector = selector;
        }

        /// <summary>
        /// Direction for the sort definition.
        /// </summary>
        public Direction SortDirection { get; }
        
        /// <summary>
        /// <see cref="SearchyNET.ISelector"/>
        /// </summary>
        public ISelector Selector { get; }

        /// <summary>
        /// Apply the sort definition to the given data collection.
        /// </summary>
        /// <param name="data">A collection of selectable data.</param>
        /// <returns>The given collection sorted according to the sort definition.</returns>
        public IEnumerable<ISelectable> Apply(IEnumerable<ISelectable> data)
        {
            return SortDirection == Direction.Asc 
                ? data.OrderBy(Selector.Select) 
                : data.OrderByDescending(Selector.Select);
        }
    }
}