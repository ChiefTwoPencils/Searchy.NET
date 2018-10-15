using System;

namespace SearchyNET
{
    /// <summary>
    /// Provides the contract for selecting IComparable values
    /// from a given selectable.
    /// </summary>
    public interface ISelector {
        /// <summary>
        /// A function that extracts a comparable value from the
        /// given selectable. Provides a general way to extract
        /// values from complex objects.
        /// </summary>
        /// <param name="selectable"><see cref="SearchyNET.ISelectable"/></param>
        /// <returns>Some comparable value.</returns>
        IComparable Select(ISelectable selectable);
    }
}