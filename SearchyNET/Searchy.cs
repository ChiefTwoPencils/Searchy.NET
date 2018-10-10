using System;
using System.Linq;
using System.Collections.Generic;

namespace SearchyNET
{
    public static class Searchy
    {
        public static class Chains
        {
            private static readonly Chain root = new Chain(
                "Root", (f, g) => And.Doop(() => true, g)
            );

            private static readonly Chain and = new Chain(
                "And", (f, g) => f() && g()
            );

            private static readonly Chain or = new Chain(
                "Or", (f, g) => f() || g()
            );

            public static Chain Root { get; } = root;
            public static Chain And { get; } = and;
            public static Chain Or { get; } = or;
        }

        public static class DataType
        {
            private static readonly List<Operator> baseTypes = new List<Operator>
            {
                Operators.Equal,
                Operators.NotEqual,
                Operators.LessThan,
                Operators.LessThanOrEqual,
                Operators.GreaterThan,
                Operators.GreaterThanOrEqual
            };

            private static readonly List<Operator> stringTypes = new List<Operator>
            (baseTypes)
            {
                Operators.Contains,
                Operators.DoesNotContain
            };

            private static readonly FieldType dateTime = new FieldType(
                nameof(DateTime), text => System.DateTime.Parse(text), baseTypes);            

            private static readonly FieldType @string = new FieldType(
                nameof(String), text => text, stringTypes);            

            public static FieldType DateTime { get; } = dateTime;
            public static FieldType String { get; } = @string;
        }

        public static class Operators
        {
            private static readonly Operator noop = new Operator(
                "no", (a, b) => default(bool)
            );

            private static readonly Operator equal = new Operator(
                "=", (a, b) => a.CompareTo(b) == 0
            );

            private static readonly Operator notEqual = new Operator(
                "<>", (a, b) => Not(() => Equal.Doop(a, b))
            );

            private static readonly Operator lessThan = new Operator(
                "<", (a, b) => a.CompareTo(b) < 0
            );

            private static readonly Operator lessThanOrEqual = new Operator(
                "<=", (a, b) => OrEqual(a, b, LessThan)
            );

            private static readonly Operator greaterThan = new Operator(
                ">", (a, b) => a.CompareTo(b) > 0
            );

            private static readonly Operator greaterThanOrEqual = new Operator(
                ">=", (a, b) => OrEqual(a, b, GreaterThan)
            );

            private static readonly Operator contains = new Operator(
                "Contains", (a, b) => ((string) a).Contains((string) b)
            );

            private static readonly Operator doesNotContain = new Operator(
                "Does Not Contain", (a, b) => Not(() => Contains.Doop(a, b))
            );

            private static readonly Operator @in = new Operator(
                "In", (a, b) =>
                {
                    string[] cleanSplitToLower(string list)
                    {
                        return list.Trim()
                            .Split(',')
                            .Select(s => s.Trim().ToLowerInvariant())
                            .ToArray();
                    }
                    var actual = cleanSplitToLower((string) a);
                    var requested = cleanSplitToLower((string) b);
                    return actual.Intersect(requested).Any();
                }
            );

            private static readonly Operator notIn = new Operator(
                "Not In", (a, b) => Not(() => In.Doop(a, b))
            );

            public static bool Not(Func<bool> op)
            {
                return !op();
            }

            public static bool OrEqual(IComparable a, IComparable b, Operator op)
            {
                return Chains.Or.Doop(() => op.Doop(a, b), () => Equal.Doop(a, b));
            }

            public static Operator Noop { get; } = noop;
            public static Operator Equal { get; } = equal;
            public static Operator NotEqual { get; } = notEqual;
            public static Operator LessThan { get; } = lessThan;
            public static Operator LessThanOrEqual { get; } = lessThanOrEqual;
            public static Operator GreaterThan { get; } = greaterThan;
            public static Operator GreaterThanOrEqual { get; } = greaterThanOrEqual;
            public static Operator Contains { get; } = contains;
            public static Operator DoesNotContain { get; } = doesNotContain;
            public static Operator In { get; } = @in;
            public static Operator NotIn { get; } = notIn;
        }
    }

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

    public class Criteria
    {
        public Criteria(List<Criterion> criteria)
        {
            All = criteria;
        }
        public List<Criterion> All { get; set; }
        public bool SatisfiesAll(ISelectable source)
        {
            var func = SatisfiesAll(source, All, 0);
            return func();
        }

        public Func<bool> SatisfiesAll(ISelectable source, List<Criterion> criteria, int index)
        {
            var current = criteria[index];
            var field = current.Field;
            var value = field.Selector(source);
            bool func() => current.Satisfies(value);
            if (index == criteria.Count - 1)
            {
                return func;
            }
            var next = criteria[index + 1];
            return () => next.Chain.Doop(func, SatisfiesAll(source, criteria, index + 1));
        }
    }

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

    public interface ISelector {
        IComparable Select(ISelectable selectable);
    }

    public interface ISelectable { }

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

    public class Value
    {
        public Value() : this("") { }
        public Value(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
    }
}