using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.Tests
{
    internal class MultiSearchTests
    {

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task One_Word_Each_Provider_Shoud_Return_A_Result()
        {
            var providers = new ISearchProvider[] { new GoogleSearch(), new BingSearch() };
            var search = new MultiSearch(providers);
            var result = await search.MultiProviderSearch("Hello", CancellationToken.None);
            result.Should().NotBeEmpty().And.HaveCount(providers.Length);
        }


        [Test]
        public async Task Two_Words_Each_Provider_Shoud_Return_Two_Result()
        {
            var providers = new ISearchProvider[] { new GoogleSearch(), new BingSearch() };
            var search = new MultiSearch(providers);
            var result = await search.MultiProviderSearch("Hello World", CancellationToken.None);
            result.Should().NotBeEmpty().And.HaveCount(providers.Length*2);
        }

        [Test]
        public async Task Three_Words_Each_Provider_Shoud_Return_Three_Result()
        {
            var providers = new ISearchProvider[] { new GoogleSearch(), new BingSearch() };
            var search = new MultiSearch(providers);
            var result = await search.MultiProviderSearch("Hello Sunny World", CancellationToken.None);
            result.Should().NotBeEmpty().And.HaveCount(providers.Length*3);
        }
    }
}
