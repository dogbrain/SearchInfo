using FluentAssertions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.Tests
{
    public class GoogleSearchTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Return_Result()
        {
            var search = new GoogleSearch();
            var result = search.NumberOfHits("Hello", CancellationToken.None);
            result.Should().NotBeNull();
        }


        [Test]
        public async Task Should_Return_A_Result()
        {
            var search = new MultiSearch(new[] { new GoogleSearch() });
            var result = await search.MultiProviderSearch("Hello", CancellationToken.None);
            result.Should().NotBeEmpty().And.HaveCount(1);
        }


        [Test]
        public async Task Should_Return_Two_Result()
        {
            var search = new MultiSearch(new[] { new GoogleSearch() });
            var result = await search.MultiProviderSearch("Hello World", CancellationToken.None);
            result.Should().NotBeEmpty().And.HaveCount(2);
        }

    }
}