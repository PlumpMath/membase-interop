using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Membase;
using Membase.Configuration;
using Enyim.Caching.Memcached;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using Enyim.Caching;
using Enyim.Caching.Configuration;

namespace Membase.Interop
{
	public abstract class MemcachedClientWrapperBase : IMemcachedClientWrapper
	{
		private IMemcachedClient client;

		public MemcachedClientWrapperBase(IMemcachedClient client)
		{
			this.client = client;
		}

		public void Dispose()
		{
			this.client.Dispose();
			this.client = null;
		}

		object IMemcachedClientWrapper.Get(string key)
		{
			return this.client.Get(key);
		}

		bool IMemcachedClientWrapper.Add(string key, object value)
		{
			return this.client.Store(StoreMode.Add, key, value);
		}

		bool IMemcachedClientWrapper.Set(string key, object value)
		{
			return this.client.Store(StoreMode.Set, key, value);
		}

		System.Collections.Hashtable IMemcachedClientWrapper.GetAsDictionary(object[] keys)
		{
			var realKeys = new string[keys.Length];

			Array.Copy(keys, realKeys, realKeys.Length);

			var tmp = this.client.Get(realKeys);
			var retval = new System.Collections.Hashtable(tmp.Count);

			foreach (var kvp in tmp)
				retval.Add(kvp.Key, kvp.Value);

			return retval;
		}

		object[] IMemcachedClientWrapper.GetAsList(object[] keys)
		{
			var realKeys = new string[keys.Length];

			Array.Copy(keys, realKeys, realKeys.Length);

			var tmp = this.client.Get(realKeys);
			var retval = new object[tmp.Count];
			int i = 0;

			foreach (var kvp in tmp)
				retval[i++] = new CacheItem { Key = kvp.Key, Value = kvp.Value };

			return retval;
		}

		bool IMemcachedClientWrapper.Replace(string key, object value)
		{
			return this.client.Store(StoreMode.Replace, key, value);
		}

		bool IMemcachedClientWrapper.AddWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.client.Store(StoreMode.Add, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.SetWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.client.Store(StoreMode.Set, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.ReplaceWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.client.Store(StoreMode.Replace, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.Remove(string key)
		{
			return this.client.Remove(key);
		}

		ulong IMemcachedClientWrapper.Increment(string key, ulong defaultValue, ulong delta)
		{
			return this.client.Increment(key, defaultValue, delta);
		}

		ulong IMemcachedClientWrapper.IncrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.client.Increment(key, defaultValue, delta, expiresAt);
		}

		ulong IMemcachedClientWrapper.Decrement(string key, ulong defaultValue, ulong delta)
		{
			return this.client.Decrement(key, defaultValue, delta);
		}

		ulong IMemcachedClientWrapper.DecrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.client.Decrement(key, defaultValue, delta, expiresAt);
		}

		bool IMemcachedClientWrapper.Append(string key, object[] data)
		{
			return this.client.Append(key, new ArraySegment<byte>(ToBytes(data)));
		}

		bool IMemcachedClientWrapper.Prepend(string key, object[] data)
		{
			return this.client.Prepend(key, new ArraySegment<byte>(ToBytes(data)));
		}

		internal static byte[] ToBytes(object[] v)
		{
			if (v == null) return null;

			var retval = new byte[v.Length];

			for (var i = 0; i < retval.Length; i++)
			{
				retval[i] = Convert.ToByte(v[i]);
			}

			return retval;
		}

		void IMemcachedClientWrapper.FlushAll()
		{
			this.client.FlushAll();
		}
	}
}
