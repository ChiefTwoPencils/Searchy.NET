using System;
using Xunit;

using static SearchyNET.Searchy;

namespace SearchyNET.Tests
{
    public class ChainTests
    {
        [Fact]
        void RootTest()
        {
            var root = Chains.Root.Doop;
            Assert.True(root(() => false, () => true));
            Assert.False(root(() => false, () => false));
        }

        [Fact]
        void AndTest()
        {
            var and = Chains.And.Doop;
            Assert.True(and(() => true, () => true));
            Assert.False(and(() => false, () => true));
            Assert.False(and(() => true, () => false));
            Assert.False(and(() => false, () => false));
        }

        [Fact]
        void OrTest()
        {
            var or = Chains.Or.Doop;
            Assert.True(or(() => true, () => false));
            Assert.True(or(() => false, () => true));
            Assert.False(or(()=> false, () => false));
        }
    }
}
