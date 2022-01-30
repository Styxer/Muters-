using Munters.Core.Caching;
using Munters.Giphy.Interfaces;
using Munters.Giphy.Model.Parameters;
using Munters.Giphy.Model.Results;
using Munters.Giphy.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Manager
{
    public class GiphyManager : IGiphyManager
    {
        private const string BaseUrl = "http://api.giphy.com/";
        private const string BaseGif = "v1/gifs";
        private const string BaseSticker = "v1/stickers";

        private readonly IHttpManager _webManager;
        private readonly IMemoryCacheManager _cacheManager;
        private readonly string _authKey;

        private NameValueCollection _nvc;

        public GiphyManager(string authKey, IHttpManager webManager, IMemoryCacheManager cacheManager)
        {
            _authKey = authKey;

            _nvc = new NameValueCollection();

            _webManager = webManager;
            _cacheManager = cacheManager;
        }

        public async Task<GiphySearchResult> GifSearch(SearchParameter searchParameter)
        {
            if (String.IsNullOrEmpty(searchParameter.Query))
            {
                throw new FormatException("Must set query in order to search.");
            }

            InitBasicData();
            _nvc.Add("q", searchParameter.Query);
            _nvc.Add("limit", searchParameter.Limit.ToString());
            _nvc.Add("offset", searchParameter.Offset.ToString());
            if (searchParameter.Rating != Rating.None)
                _nvc.Add("rating", searchParameter.Rating.ToFriendlyString());
            if (!string.IsNullOrEmpty(searchParameter.Format))
                _nvc.Add("fmt", searchParameter.Format);

    
             return await DoSearch<GiphySearchResult>(SearchType.Search,  searchParameter.Query); 
           

        }
    
        public async Task<GiphySearchResult> TrendingGifs(TrendingParameter trendingParameter)
        {
            InitBasicData();
            _nvc.Add("limit", trendingParameter.Limit.ToString());
            if (trendingParameter.Rating != Rating.None)
                _nvc.Add("rating", trendingParameter.Rating.ToFriendlyString());
            if (!string.IsNullOrEmpty(trendingParameter.Format))
                _nvc.Add("fmt", trendingParameter.Format);            

           return await DoSearch<GiphySearchResult>(SearchType.Trending);
          

        }

        private void InitBasicData()
        {
            _nvc.Clear();
            _nvc.Add("api_key", _authKey);
        }

        private async Task<T> DoSearch<T>(SearchType searchType , string query = null)
        {
            //var sb = new StringBuilder();
            // if (type == SearchType.Trending)
            //     sb.AppendLine("trending");
            // else
            // {
            //     for (int i = 0; i < _nvc.Keys.Count; i++)
            //     {
            //         sb.AppendLine(_nvc.v);
            //     }
            // }    


            // key; // = string.Join(",", _nvc.Keys);


            string key = SearchType.Search == searchType ? query : searchType.ToString();
            string type = $"/{searchType.GetMemberAttr()}";

            var result =  await _cacheManager.GetOrAddAsync(key,
                () => Task.Run(() => _webManager.GetData(new Uri($"{BaseUrl}{BaseGif}{type}{UriExtensions.ToQueryString(_nvc)}")))
                , expirationCalculator: i => TimeSpan.FromMilliseconds(1));
            //expirationCalculator how long the items stays in cache

            if (!result.IsSuccess)
            {
                throw new WebException($"Failed to get GIF: {result.ResultJson}");
            }

            return JsonConvert.DeserializeObject<T>(result.ResultJson);
        }
    }
}
