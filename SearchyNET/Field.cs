using System;

namespace SearchyNET
{
    public class Field
    {
        public Field() : this("", Searchy.DataType.String, (a) => true) { }
        public Field(string name, FieldType fieldType, Func<ISelectable, IComparable> selector)
        {
            Name = name;
            FieldType = fieldType;
            Selector = selector;
        }
        public string Name { get; private set; }
        public FieldType FieldType { get; private set; }
        public Func<ISelectable, IComparable> Selector { get; set; }
    }
}