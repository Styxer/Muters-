using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy
{  
    public enum SearchType
    {
        [EnumMember(Value = "trending")]
        Trending,
        [EnumMember(Value = "search")]
        Search
    }


}
