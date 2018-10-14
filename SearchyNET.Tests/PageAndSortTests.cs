using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SearchyNET.Tests
{
    public class PageAndSortTests
    {
        [Fact]
        private void DefaultSkipTest()
        {
            var page = new Page();
            const int expected = (1 - 1) * 25;
            Assert.Equal(expected, page.Skip);
        }

        [Fact]
        private void SkipTest()
        {
            const int number = 4;
            const int take = 15;
            var page = new Page(number, take);
            const int expected = (number - 1) * take;
            Assert.Equal(expected, page.Skip);
        }

        [Fact]
        private void SortAscTest()
        {
            var data = GetTestSelectables(500).ToList();
            var selector = new PropertySelector();
            var sort = new Sort(Sort.Direction.Asc, selector);
            var actual = sort.Apply(data.ToList());
            var expected = data.ToList().OrderBy(selector.Select);
            Assert.Equal(expected, actual);
        }

        [Fact]
        private void SortDescTest()
        {
            var data = GetTestSelectables(500).ToList();
            var selector = new PropertySelector();
            var sort = new Sort(Sort.Direction.Desc, selector);
            var actual = sort.Apply(data.ToList());
            var expected = data.ToList().OrderByDescending(selector.Select);
            Assert.Equal(expected, actual);
        }

        private class TestSelectable : ISelectable
        {
            public TestSelectable(string property)
            {
                Property = property;
            }
            public string Property { get; }
        }

        private class PropertySelector : ISelector
        {
            public IComparable Select(ISelectable selectable)
            {
                return ((TestSelectable) selectable).Property;
            }
        }

        private IEnumerable<TestSelectable> GetTestSelectables(int many)
        {
            return Enumerable.Range(0, many)
                .Select(i => new TestSelectable(GetRandomString()));
        }

        private string GetRandomString()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var alphLength = alphabet.Length;
            var random = new Random((int) DateTime.Now.Ticks);
            return Enumerable.Range(0, 10)
                .Aggregate(
                    new StringBuilder(),
                    (b, i) => b.Append(alphabet[random.Next(alphLength)]),
                    b => b.ToString());
        }
    }
}