using Munters.Giphy.Model.Parameters;
using Munters.Giphy.Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Interfaces
{
    public interface IGiphyManager
    {
        Task<GiphySearchResult> GifSearch(SearchParameter searchParameter);    
        Task<GiphySearchResult> TrendingGifs(TrendingParameter trendingParameter);
    }
}
