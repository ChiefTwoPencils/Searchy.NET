using System;
using System.Collections.Generic;
using Xunit;

using static SearchyNET.Searchy;

namespace SearchyNET.Tests
{
    public class DataTypeTests
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

        [Fact]
        void StringTest()
        {
            var stringType = DataType.String;
            Assert.IsType<FieldType>(stringType);
            Assert.Equal("String", stringType.DataType);
            Assert.IsType<string>(stringType.Convert("This is just a string"));           
            Assert.Equal(stringTypes, stringType.Ops);
        }

        [Fact]
        void DateTimeTest()
        {
            var dateType = DataType.DateTime;
            Assert.IsType<FieldType>(dateType);
            Assert.Equal("DateTime", dateType.DataType);
            Assert.IsType<DateTime>(dateType.Convert("01/01/2018"));
            Assert.Equal(baseTypes, dateType.Ops);
        }
    }
}
