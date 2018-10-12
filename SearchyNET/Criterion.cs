using System;

namespace SearchyNET
{
    public class Criterion
    {
        public Criterion() : this(Searchy.Chains.Root, new Field(), Searchy.Operators.Noop, new Value()) { }
        public Criterion(Chain chain, Field field, Operator dooperator, Value value)
        {
            Chain = chain;
            Field = field;
            Operator = dooperator;
            Value = value;
        }
        public Chain Chain { get; set; }
        public Field Field { get; set; }
        public Operator Operator { get; set; }
        public Value Value { get; set; }
        public bool Satisfies(IComparable comparable)
        {
            var actual = Field.FieldType.Convert(Value.Text);
            return Operator.Doop(comparable, actual);
        }
    }
}