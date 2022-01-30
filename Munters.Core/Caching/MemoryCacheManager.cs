using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Munters.Core.Caching
{
    public class MemoryCacheManager : IMemoryCacheManager
    {      
        private readonly IMemoryCache _cache;

        private static double _slidingExpiration;
        private static double _absoluteExpiration;

        public MemoryCacheManager(double  slidingExpiration = 24, double  absoluteExpiration = 24)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _slidingExpiration = slidingExpiration;
            _absoluteExpiration = absoluteExpiration;
        }
        public Task<T> GetOrAddAsync<T>(
        string key, Func<Task<T>> factory, Func<T, TimeSpan> expirationCalculator)
        {
            return _cache.GetOrCreateAsync(key, async cacheEntry =>
            {
                var cts = new CancellationTokenSource();
                cacheEntry.AddExpirationToken(new CancellationChangeToken(cts.Token));
                cacheEntry.SetSlidingExpiration(TimeSpan.FromHours(_slidingExpiration))
                          .SetAbsoluteExpiration(TimeSpan.FromHours(_absoluteExpiration));              
                var value = await factory().ConfigureAwait(false);
                cts.CancelAfter(expirationCalculator(value));
                return value;
            });
        }
        public bool  RemoveKey(string key)
        {
            var success = false;
            try
            {
                if (Exists(key))
                {
                    _cache.Remove(key);
                    success = true;
                }
                  
            }
            catch (Exception ex)
            {
                    
            }

            return success;
        }

        private  bool Exists(string key) => _cache.TryGetValue(key, out var _);



    }
}
