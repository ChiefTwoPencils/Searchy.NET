using System;

namespace SearchyNET
{
    public class Chain
    {
        public Chain(string type, Func<Func<bool>, Func<bool>, bool> doop)
        {
            Type = type;
            Doop = doop;
        }
        public string Type { get; private set; }
        public Func<Func<bool>, Func<bool>, bool> Doop { get; private set; }
    }
}