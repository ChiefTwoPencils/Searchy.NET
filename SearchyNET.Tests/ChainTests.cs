using Xunit;

using static SearchyNET.Searchy;

namespace SearchyNET.Tests
{
    public class ChainTests
    {
        [Fact]
        private void RootTest()
        {
            var root = Chains.Root.Doop;
            Assert.True(root(() => true, () => true));
            Assert.True(root(() => false, () => true));
            Assert.False(root(() => false, () => false));
            Assert.False(root(() => true, () => false));
        }

        [Fact]
        private void AndTest()
        {
            var and = Chains.And.Doop;
            Assert.True(and(() => true, () => true));
            Assert.False(and(() => false, () => true));
            Assert.False(and(() => true, () => false));
            Assert.False(and(() => false, () => false));
        }

        [Fact]
        private void OrTest()
        {
            var or = Chains.Or.Doop;
            Assert.True(or(() => true, () => true));
            Assert.True(or(() => true, () => false));
            Assert.True(or(() => false, () => true));
            Assert.False(or(()=> false, () => false));
        }
    }
}
