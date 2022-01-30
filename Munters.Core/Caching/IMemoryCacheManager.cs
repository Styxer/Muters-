using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Core.Caching
{
    public interface IMemoryCacheManager
    {
        Task<T> GetOrAddAsync<T>(
                     string key, Func<Task<T>> factory, Func<T, TimeSpan> expirationCalculator);
        bool RemoveKey(string key);

     
    }
}
