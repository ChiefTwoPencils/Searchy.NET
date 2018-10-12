using System;

namespace SearchyNET
{
    public class Operator
    {
        public Operator(string symbol, Func<IComparable, IComparable, bool> doop)
        {
            Symbol = symbol;
            Doop = doop;
        }
        public String Symbol { get; private set; }
        public Func<IComparable, IComparable, bool> Doop { get; private set; }
    }
}