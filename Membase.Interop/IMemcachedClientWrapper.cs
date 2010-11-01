using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Membase;
using Membase.Configuration;
using System.Runtime.InteropServices;
using System.Collections;

namespace Membase.Interop
{
	[Guid("2a839618-797d-4715-9337-6c349706034a"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IMemcachedClientWrapper
	{
		object Get(string key);
		Hashtable Gets(object[] keys);

		bool Set(string key, object value);
		bool Add(string key, object value);
		bool Replace(string key, object value);

		bool AddWithExpiration(string key, object value, DateTime expiresAt);
		bool SetWithExpiration(string key, object value, DateTime expiresAt);
		bool ReplaceWithExpiration(string key, object value, DateTime expiresAt);

		bool Remove(string key);

		ulong Increment(string key, ulong defaultValue, ulong delta);
		ulong IncrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt);
		ulong Decrement(string key, ulong defaultValue, ulong delta);
		ulong DecrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt);

		bool Append(string key, object[] data);
		bool Prepend(string key, object[] data);

		void FlushAll();
	}
}
