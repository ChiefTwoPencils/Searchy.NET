using System;

namespace SearchyNET
{
    /// <summary>
    /// Represents a chaining of two functions.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Constructs a Chain with a type name and operation to
        /// do to chain functions.
        /// </summary>
        /// <param name="type"><see cref="Type"/></param>
        /// <param name="doop"><see cref="Doop"/></param>
        public Chain(string type, Func<Func<bool>, Func<bool>, bool> doop)
        {
            Type = type;
            Doop = doop;
        }
        
        /// <summary>
        /// Name for the type of Chain.
        /// </summary>
        public string Type { get; }
        
        /// <summary>
        /// Operation used to chain two functions.
        /// </summary>
        public Func<Func<bool>, Func<bool>, bool> Doop { get; }
    }
}