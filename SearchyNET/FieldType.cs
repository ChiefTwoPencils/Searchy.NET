using System;
using System.Collections.Generic;

namespace SearchyNET
{
    public class FieldType
    {
        public FieldType(string dataType, Func<string, IComparable> converter, List<Operator> ops)
        {
            DataType = dataType;
            Ops = ops;
            Convert = converter;
        }
        public string DataType { get; private set; }
        public List<Operator> Ops { get; private set; }
        public Func<string, IComparable> Convert { get; set; }
    }
}