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

namespace Membase.Interop
{
	[Guid("e6dfee2d-80f3-4d12-8370-aca16b7f3bdd"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Membase.Interop.MembaseClient")]
	public class MembaseClientWrapper : IMemcachedClientWrapper
	{
		private MembaseClient nsc;

		public MembaseClientWrapper(IMembaseClientConfiguration config) : this(config, null) { }

		public MembaseClientWrapper(IMembaseClientConfiguration config, string bucketName)
		{
			this.nsc = new MembaseClient(config, bucketName);
		}

		public void Dispose()
		{
			this.nsc.Dispose();
			this.nsc = null;
		}

		object IMemcachedClientWrapper.Get(string key)
		{
			return this.nsc.Get(key);
		}

		bool IMemcachedClientWrapper.Add(string key, object value)
		{
			return this.nsc.Store(StoreMode.Add, key, value);
		}

		bool IMemcachedClientWrapper.Set(string key, object value)
		{
			return this.nsc.Store(StoreMode.Set, key, value);
		}

		System.Collections.Hashtable IMemcachedClientWrapper.Gets(object[] keys)
		{
			var realKeys = new string[keys.Length];

			Array.Copy(keys, realKeys, realKeys.Length);

			var tmp = this.nsc.Get(realKeys);
			var retval = new System.Collections.Hashtable(tmp.Count);

			foreach (var kvp in tmp)
				retval.Add(kvp.Key, kvp.Value);

			return retval;
		}

		bool IMemcachedClientWrapper.Replace(string key, object value)
		{
			return this.nsc.Store(StoreMode.Replace, key, value);
		}

		bool IMemcachedClientWrapper.AddWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Add, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.SetWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Set, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.ReplaceWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Replace, key, value, expiresAt);
		}

		bool IMemcachedClientWrapper.Remove(string key)
		{
			return this.nsc.Remove(key);
		}

		ulong IMemcachedClientWrapper.Increment(string key, ulong defaultValue, ulong delta)
		{
			return this.nsc.Increment(key, defaultValue, delta);
		}

		ulong IMemcachedClientWrapper.IncrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.nsc.Increment(key, defaultValue, delta, expiresAt);
		}

		ulong IMemcachedClientWrapper.Decrement(string key, ulong defaultValue, ulong delta)
		{
			return this.nsc.Decrement(key, defaultValue, delta);
		}

		ulong IMemcachedClientWrapper.DecrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.nsc.Decrement(key, defaultValue, delta, expiresAt);
		}

		bool IMemcachedClientWrapper.Append(string key, object[] data)
		{
			return this.nsc.Append(key, new ArraySegment<byte>(MemcachedClientWrapper.ToBytes(data)));
		}

		bool IMemcachedClientWrapper.Prepend(string key, object[] data)
		{
			return this.nsc.Prepend(key, new ArraySegment<byte>(MemcachedClientWrapper.ToBytes(data)));
		}

		void IMemcachedClientWrapper.FlushAll()
		{
			this.nsc.FlushAll();
		}
	}
}
