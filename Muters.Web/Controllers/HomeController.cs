using Microsoft.AspNetCore.Mvc;
using Munters.Core.Caching;
using Munters.Giphy.Interfaces;
using Munters.Giphy.Model.Parameters;
using Munters.Giphy.Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Muters.Web.Controllers
{
    [Route("api/gifs")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IGiphyManager _giphy;

        public HomeController(IGiphyManager cacheManager)
        {
            _giphy = cacheManager;
        }



        [HttpGet("{searchTerm}")]
        public async Task<GiphySearchResult> SearchGif(string searchTerm)
        {
            var searchParameter = new SearchParameter()
            {
                Query = searchTerm
            };
           return await _giphy.GifSearch(searchParameter);
           
            
        }

  
        [HttpGet]
        public async Task<GiphySearchResult> GetTrending(string search)
        {
            return await _giphy.TrendingGifs(new TrendingParameter());


        }

       
    }
}
