using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Infrastructure.Cache
{
    /// <summary>
    /// 内存缓存管理
    /// </summary>
    public static class RAMCacheProvider
    {
        /// <summary>
        /// 内部数据
        /// </summary>
        private static readonly Dictionary<string, object> _Data;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static RAMCacheProvider()
        {
            _Data = new Dictionary<string, object>();
        }

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <param name="value"></param>
        public static void Add(string key, string valKey, object value)
        {
            Dictionary<string, object> dict;
            if (_Data.ContainsKey(key))
            {
                dict = (Dictionary<string, object>)_Data[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object> { { valKey, value } };
            }
            _Data.Add(key, dict);
        }

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <param name="value"></param>
        public static void Put(string key, string valKey, object value)
        {
            Add(key, valKey, value);
        }

        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <returns></returns>
        public static object Get(string key, string valKey)
        {
            if (_Data.ContainsKey(key))
            {
                var dict = (Dictionary<string, object>)_Data[key];
                if (dict != null && dict.ContainsKey(valKey))
                    return dict[valKey];
                return null;
            }
            return null;
        }

        /// <summary>
        /// 从缓存中移除对象。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        public static void Remove(string key, string valKey)
        {
            ((Dictionary<string, object>)_Data[key]).Remove(valKey);
        }

        /// <summary>
        /// 获取一个<see cref="bool"/>值，该值表示拥有指定键值是否存在。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return _Data.ContainsKey(key);
        }

        /// <summary>
        ///  获取一个<see cref="bool"/>值，该值表示拥有指定键值和缓存值键的缓存是否存在。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <returns></returns>
        public static bool Exists(string key, string valKey)
        {
            return _Data.ContainsKey(key) &&
                ((Dictionary<string, object>)_Data[key]).ContainsKey(valKey);
        }
    }
}
