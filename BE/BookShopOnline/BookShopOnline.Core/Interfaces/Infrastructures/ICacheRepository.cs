using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.Infrastructures
{
    public interface ICacheRepository
    {
        void AddToCache<T>(string key, T value, TimeSpan expirationTime);
        void RemoveFromCache(string key);
        T GetFromCache<T>(string key);
        bool IsCacheExist(string key);
    }
}
