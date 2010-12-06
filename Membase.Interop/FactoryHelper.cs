using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.IO;

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

		internal static void ConfigureLogger(string logPath)
		{
			if (String.IsNullOrEmpty(logPath))
				Enyim.Caching.LogManager.AssignFactory(new Enyim.Caching.NullLoggerFactory());
			else
			{
				if (!Directory.Exists(Path.GetDirectoryName(logPath)))
					throw new DirectoryNotFoundException("Cannot find the directory " + Path.GetDirectoryName(logPath));

				Enyim.Caching.LogManager.AssignFactory(new BasicLoggerFactory(logPath));
			}
		}
	}
}
