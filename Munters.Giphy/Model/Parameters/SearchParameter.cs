using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Model.Parameters
{
    public class SearchParameter : TrendingParameter
    {
        public string Query { get; set; }      
        public int Offset { get; set; } = 0;
        
      
    }
}
