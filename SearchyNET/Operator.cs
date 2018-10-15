using System;

namespace SearchyNET
{
    /// <summary>
    /// Represents an operator used on two comparable values such
    /// as less than, greater than, equal, etc.
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// Constructs an Operator with the given symbol and operation to
        /// perform.
        /// </summary>
        /// <param name="symbol"><see cref="Symbol"/></param>
        /// <param name="doop"><see cref="Doop"/></param>
        public Operator(string symbol, Func<IComparable, IComparable, bool> doop)
        {
            Symbol = symbol;
            Doop = doop;
        }
        
        /// <summary>
        /// Symbol to be used as a visual representation of the
        /// operator.
        /// </summary>
        public string Symbol { get; }
        
        /// <summary>
        /// Function that performs the operation represented by
        /// the symbol.
        /// </summary>
        public Func<IComparable, IComparable, bool> Doop { get; }
    }
}