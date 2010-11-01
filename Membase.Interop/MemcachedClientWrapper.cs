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
	[Guid("4f860318-e4b1-4e8e-a2ab-d20ca147e2f6"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Enyim.Caching.Interop.MemcachedClient")]
	public class MemcachedClientWrapper : IMemcachedClientWrapper
	{
		private MemcachedClient mc;

		public MemcachedClientWrapper(IMemcachedClientConfiguration config)
		{
			this.mc = new MemcachedClient(config);
		}

		public void Dispose()
		{
			this.mc.Dispose();
			this.mc = null;
		}

		object IMemcachedClientWrapper.Get(string key)
		{
			return this.mc.Get(key);
		}

		bool IMemcachedClientWrapper.Add(string key, object value)
		{
			return this.mc.Store(StoreMode.Add, key, value);
		}

		bool IMemcachedClientWrapper.Set(string key, object value)
		{
			return this.mc.Store(StoreMode.Set, key, value);
		}

		System.Collections.Hashtable IMemcachedClientWrapper.Gets(object[] keys)
		{
			var realKeys = new string[keys.Length];

			Array.Copy(keys, realKeys, realKeys.Length);

			var tmp = this.mc.Get(realKeys);
			var retval = new System.Collections.Hashtable(tmp.Count);

			foreach (var kvp in tmp)
				retval.Add(kvp.Key, kvp.Value);

			return retval;
		}

		bool IMemcachedClientWrapper.Replace(string key, object value)
		{
			return this.mc.Store(StoreMode.Replace, key, value);
		}

		bool IMemcachedClientWrapper.AddWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.mc.Store(StoreMode.Add, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.SetWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.mc.Store(StoreMode.Set, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.ReplaceWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.mc.Store(StoreMode.Replace, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.Remove(string key)
		{
			return this.mc.Remove(key);
		}

		ulong IMemcachedClientWrapper.Increment(string key, ulong defaultValue, ulong delta)
		{
			return this.mc.Increment(key, defaultValue, delta);
		}

		ulong IMemcachedClientWrapper.IncrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.mc.Increment(key, defaultValue, delta, expiresAt);
		}

		ulong IMemcachedClientWrapper.Decrement(string key, ulong defaultValue, ulong delta)
		{
			return this.mc.Decrement(key, defaultValue, delta);
		}

		ulong IMemcachedClientWrapper.DecrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.mc.Decrement(key, defaultValue, delta, expiresAt);
		}

		bool IMemcachedClientWrapper.Append(string key, object[] data)
		{
			return this.mc.Append(key, new ArraySegment<byte>(ToBytes(data)));
		}

		bool IMemcachedClientWrapper.Prepend(string key, object[] data)
		{
			return this.mc.Prepend(key, new ArraySegment<byte>(ToBytes(data)));
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
			this.mc.FlushAll();
		}
	}
}
