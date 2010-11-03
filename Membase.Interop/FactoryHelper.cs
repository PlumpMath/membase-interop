using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace Membase.Interop
{
	internal static class FactoryHelper
	{
		internal static void ClearCache(string keyPrefix)
		{
			var cache = HttpRuntime.Cache;

			foreach (System.Collections.DictionaryEntry entry in cache)
			{
				var k = entry.Key as string;

				if (k != null && k.StartsWith(keyPrefix))
					cache.Remove(k);
			}
		}

		internal static string GetAssemblyVersion()
		{
			var a = typeof(FactoryHelper).Assembly;
			var name = a.GetName();

			return name.Version.ToString();
		}
	}
}
