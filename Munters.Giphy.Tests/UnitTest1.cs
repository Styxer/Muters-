using System;
using Xunit;
using System.Linq;
using Munters.Giphy.Manager;
using Munters.Giphy.Model.Parameters;
using Munters.Giphy.Interfaces;
using Munters.Core.Caching;

namespace Munters.Giphy.Tests
{
    public class UnitTest1
    {
        string api_token;
        IGiphyManager giphy;

        public UnitTest1()
        {
            api_token = "PB8lcz4dhkIEGa5d0rzWctpV05RZ7V5y";
            giphy = new GiphyManager(api_token, new HttpManager(), new MemoryCacheManager());
        }

        [Fact]
        public async void GifSearch()
        {
            var searchParameter = new SearchParameter()
            {
                Query = "ofir"
            };

           
            var gifResult = await giphy.GifSearch(searchParameter);
            Assert.True(gifResult != null);
            Assert.True(gifResult.Data.Any());
        }

        [Fact]
        public async void TrendingGifs()
        {
            var gifResult = await giphy.TrendingGifs(new TrendingParameter());
            Assert.True(gifResult != null);
        }
    }
}
