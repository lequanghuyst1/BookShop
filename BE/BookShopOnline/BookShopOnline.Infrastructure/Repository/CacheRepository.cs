using BookShopOnline.Core.Interfaces.Infrastructures;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class CacheRepository : ICacheRepository
    {
        readonly IMemoryCache _memoryCache;

        public CacheRepository(IMemoryCache memorycache)
        {
            _memoryCache = memorycache;
        }

        public void AddToCache<T>(string key, T value, TimeSpan expirationTime)
        {
            // Thêm dữ liệu vào Memory Cache với khóa key và thời gian tồn tại là expirationTime
            _memoryCache.Set<T>(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            });

            // Đăng ký hàm callback để xử lý khi phần tử bị xóa khỏi cache
        }

        public T GetFromCache<T>(string key)
        {
            // Lấy dữ liệu từ Memory Cache bằng khóa key
            if (_memoryCache.TryGetValue(key, out T value))
            {
                return value;
            }
            else
            {
                // Trường hợp dữ liệu không tồn tại trong Cache
                return default(T);
            }
        }

        public bool IsCacheExist(string key)
        {
            return _memoryCache.Get(key) != null;
        }

        public void RemoveFromCache(string key)
        {
            // Xóa dữ liệu khỏi Memory Cache bằng khóa key
            _memoryCache.Remove(key);
        }
    }
}
