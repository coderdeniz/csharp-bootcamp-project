using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value,TimeSpan.FromMinutes(duration)); 
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var coherentState = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);

            var coherentStateValue = coherentState.GetValue(_memoryCache);

            var entriesCollection = coherentStateValue.GetType().GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);

            var entriesCollectionValue = entriesCollection.GetValue(coherentStateValue) as ICollection;

            var keys = new List<string>();

            if (entriesCollectionValue != null)
            {
                foreach (var item in entriesCollectionValue)
                {
                    var methodInfo = item.GetType().GetProperty("Key");

                    var val = methodInfo.GetValue(item);

                    keys.Add(val.ToString());
                }
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            var keysToRemove = keys.Where(d => regex.IsMatch(d.ToString())).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
