using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Model.Parameters
{
    public class TrendingParameter
    {
        public int Limit { get; set; } = 25;
        public Rating Rating { get; set; }
        public string Format { get; set; }
    }
}
