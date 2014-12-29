using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace MyBatis.DataMapper.Model.Cache.Implementation
{
    public sealed class MemCached : BaseCache
    {
        private string _keyPrefix = "MyBatis:";
        private TimeSpan _timeout = TimeSpan.FromHours(24);
        private MemcachedClient _memcachedClient;

        public MemCached() 
		{
            _memcachedClient = new MemcachedClient();
		}

        public override object Remove(object key)
        {
            var result = _memcachedClient.Get(this.GetKey(key));
            if (result != null)
            {
                _memcachedClient.Remove(this.GetKey(key));
            }

            return result;
        }

        public override void Clear()
        {
            //分布式缓存 需要全部清空？
            _memcachedClient.FlushAll();
        }

        public override object this[object key]
        {
            get
            {
                return _memcachedClient.Get(this.GetKey(key));
            }
            set
            {
                _memcachedClient.Store(StoreMode.Add, this.GetKey(key), value, TimeSpan.Parse("10000"));
               // _memcachedClient.Store(StoreMode.Set, this.GetKey(key), value, _timeout);
            }
        }

        public override bool ContainsKey(object key)
        {
            object outResult;
            var result = _memcachedClient.TryGet(this.GetKey(key),out outResult);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetKey(object key)
        {
            return string.Format("{0}{1}", _keyPrefix, key);
        }
    }
}
