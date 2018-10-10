using System;
using Xunit;

using static SearchyNET.Searchy;

namespace SearchyNET.Tests
{
    public class OperatorTests
    {
        string testString = "SomeTestString";
        DateTime testDate = new DateTime(2018, 5, 29);

        [Fact]
        void NoopTest()
        {
            var noop = Operators.Noop.Doop;
            Assert.Equal(default(bool), noop("any", "thing"));
        }

        [Fact]
        void EqualsTest()
        {
            var equals = Operators.Equal.Doop;
            Assert.True(equals(testString, testString));
            Assert.True(equals(testDate, testDate));
            Assert.False(equals(testString, ""));
            Assert.False(equals(testDate, testDate.AddDays(1)));
        }

        [Fact]
        void NotEqualTest()
        {
            var notEqual = Operators.NotEqual.Doop;
            Assert.True(notEqual(testString, ""));
            Assert.True(notEqual(testDate, testDate.AddDays(1)));
            Assert.False(notEqual(testString, testString));
            Assert.False(notEqual(testDate, testDate));
        }

        [Fact]
        void LessThanTest()
        {
            var lessThan = Operators.LessThan.Doop;
            Assert.True(lessThan(1, 5));
            Assert.False(lessThan(5, 1));
        }

        [Fact]
        void LessThanOrEqual()
        {
            var lessThanOrEqual = Operators.LessThanOrEqual.Doop;
            Assert.True(lessThanOrEqual(1, 5));
            Assert.True(lessThanOrEqual(1, 1));
            Assert.False(lessThanOrEqual(5, 1));
        }

        [Fact]
        void GreaterThanTest()
        {
            var greaterThan = Operators.GreaterThan.Doop;
            Assert.True(greaterThan(4, 3));
            Assert.False(greaterThan(3, 4));
        }

        [Fact]
        void GreaterThanOrEqualTest()
        {
            var greaterThanOrEqual = Operators.GreaterThanOrEqual.Doop;
            Assert.True(greaterThanOrEqual(4, 3));
            Assert.True(greaterThanOrEqual(4, 4));
            Assert.False(greaterThanOrEqual(4, 5));
        }

        [Fact]
        void InTest()
        {
            var inop = Operators.In.Doop;
            var group = "hello, world, C#, XUnit";
            Assert.True(inop("world", group));
            Assert.False(inop("missing", group));
        }

        [Fact]
        void NotInTest()
        {
            var notIn = Operators.NotIn.Doop;
            var group = "hello, world, C#, XUnit";
            Assert.True(notIn("missing", group));
            Assert.False(notIn("C#", group));
        }

        [Fact]
        void ContainsTest()
        {
            var contains = Operators.Contains.Doop;
            Assert.True(contains(testString, "Test"));
            Assert.False(contains("Test", testString));
        }

        [Fact]
        void DoesNotContainTest()
        {
            var notContains = Operators.DoesNotContain.Doop;
            Assert.True(notContains("Test", testString));
            Assert.False(notContains(testString, "Test"));
        }

        [Fact]
        void NotTest()
        {
            Func<Func<bool>, bool> not = Operators.Not;
            Assert.True(not(() => false));
            Assert.False(not(() => true));
        }
    }
}
