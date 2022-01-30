using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy.Tools
{
    public class UriExtensions
    {
        public static string ToQueryString(NameValueCollection nvc, bool encodeValue = true)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select $"{WebUtility.UrlEncode(key)}={WebUtility.UrlEncode(encodeValue ? WebUtility.UrlEncode(value) : value)}")
                           .ToArray();

            return "?" + string.Join("&", array);

        }
    }
}
