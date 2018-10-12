using System;
using Xunit;

using static SearchyNET.Searchy;

namespace SearchyNET.Tests
{
    public class OperatorTests
    {
        private const string TestString = "SomeTestString";
        private readonly DateTime _testDate = new DateTime(2018, 5, 29);

        [Fact]
        private void NoopTest()
        {
            var noop = Operators.Noop.Doop;
            Assert.Equal(default(bool), noop("any", "thing"));
        }

        [Fact]
        private void EqualsTest()
        {
            var equals = Operators.Equal.Doop;
            Assert.True(equals(TestString, TestString));
            Assert.True(equals(_testDate, _testDate));
            Assert.False(equals(TestString, ""));
            Assert.False(equals(_testDate, _testDate.AddDays(1)));
        }

        [Fact]
        private void NotEqualTest()
        {
            var notEqual = Operators.NotEqual.Doop;
            Assert.True(notEqual(TestString, ""));
            Assert.True(notEqual(_testDate, _testDate.AddDays(1)));
            Assert.False(notEqual(TestString, TestString));
            Assert.False(notEqual(_testDate, _testDate));
        }

        [Fact]
        private void LessThanTest()
        {
            var lessThan = Operators.LessThan.Doop;
            Assert.True(lessThan(1, 5));
            Assert.False(lessThan(5, 1));
        }

        [Fact]
        private void LessThanOrEqual()
        {
            var lessThanOrEqual = Operators.LessThanOrEqual.Doop;
            Assert.True(lessThanOrEqual(1, 5));
            Assert.True(lessThanOrEqual(1, 1));
            Assert.False(lessThanOrEqual(5, 1));
        }

        [Fact]
        private void GreaterThanTest()
        {
            var greaterThan = Operators.GreaterThan.Doop;
            Assert.True(greaterThan(4, 3));
            Assert.False(greaterThan(3, 4));
        }

        [Fact]
        private void GreaterThanOrEqualTest()
        {
            var greaterThanOrEqual = Operators.GreaterThanOrEqual.Doop;
            Assert.True(greaterThanOrEqual(4, 3));
            Assert.True(greaterThanOrEqual(4, 4));
            Assert.False(greaterThanOrEqual(4, 5));
        }

        [Fact]
        private void InTest()
        {
            var inop = Operators.In.Doop;
            var group = "hello, world, C#, XUnit";
            Assert.True(inop("world", group));
            Assert.False(inop("missing", group));
        }

        [Fact]
        private void NotInTest()
        {
            var notIn = Operators.NotIn.Doop;
            var group = "hello, world, C#, XUnit";
            Assert.True(notIn("missing", group));
            Assert.False(notIn("C#", group));
        }

        [Fact]
        private void ContainsTest()
        {
            var contains = Operators.Contains.Doop;
            Assert.True(contains(TestString, "Test"));
            Assert.False(contains("Test", TestString));
        }

        [Fact]
        private void DoesNotContainTest()
        {
            var notContains = Operators.DoesNotContain.Doop;
            Assert.True(notContains("Test", TestString));
            Assert.False(notContains(TestString, "Test"));
        }

        [Fact]
        private void NotTest()
        {
            Func<Func<bool>, bool> not = Operators.Not;
            Assert.True(not(() => false));
            Assert.False(not(() => true));
        }
    }
}
