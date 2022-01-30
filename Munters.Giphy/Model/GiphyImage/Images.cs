using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Model.GiphyImage
{
    public class Images
    {

        [JsonProperty("fixed_height")]
        public FixedHeight FixedHeight { get; set; }    

        [JsonProperty("original")]
        public Original Original { get; set; }
        [JsonProperty("downsized")]
        public Downsized Downsized { get; set; }
    }
}
