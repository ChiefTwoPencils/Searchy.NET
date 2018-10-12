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
}