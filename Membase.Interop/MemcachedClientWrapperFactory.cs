using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Enyim.Caching.Memcached;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using Enyim;
using Enyim.Caching.Configuration;
using System.Web;

namespace Membase.Interop
{
	[Guid("64a3a309-94d4-4d88-a587-1756fb66527c"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Enyim.Caching.Interop.MemcachedClientFactory")]
	public class MemcachedClientWrapperFactory : IMemcachedClientWrapperFactory
	{
		private static readonly object SyncObj = new Object();

		IMemcachedClientWrapper IMemcachedClientWrapperFactory.Create(string configPath)
		{
			System.Diagnostics.Debugger.Break();

			if (String.IsNullOrEmpty(configPath)) throw new ArgumentNullException("configPath");
			if (!File.Exists(configPath)) throw new FileNotFoundException("File not found: " + configPath);

			var key = ("MCC@" + configPath).ToUpperInvariant();
			IMemcachedClientWrapper retval;

			var cache = HttpRuntime.Cache;

			if ((retval = cache[key] as IMemcachedClientWrapper) == null)
				lock (cache)
					if ((retval = cache[key] as IMemcachedClientWrapper) == null)
					{
						var config = this.Load(configPath, null);
						retval = retval = new MemcachedClientWrapper(config);

						cache.Insert(key, retval, new System.Web.Caching.CacheDependency(configPath));
					}

			return retval;
		}

		private IMemcachedClientConfiguration Load(string path, string sectionName)
		{
			//System.Diagnostics.Debugger.Break();

			if (!File.Exists(path)) throw new InvalidOperationException("The config file '" + path + "' cannot be found.");
			var cfm = new ConfigurationFileMap();
			cfm.MachineConfigFilename = path;

			var cfg = ConfigurationManager.OpenMappedMachineConfiguration(cfm);
			if (cfg == null) if (!File.Exists(path)) throw new InvalidOperationException("The config file '" + path + "' cannot be found.");

			if (String.IsNullOrEmpty(sectionName))
				sectionName = "enyim.com/memcached";

			var section = cfg.GetSection(sectionName) as IMemcachedClientConfiguration;
			if (section == null) if (!File.Exists(path)) throw new InvalidOperationException("The config section '" + sectionName + "' cannot be found.");

			return section;
		}

		void IMemcachedClientWrapperFactory.ClearCachedClients()
		{
			var cache = HttpRuntime.Cache;

			lock (SyncObj)
				foreach (System.Collections.DictionaryEntry entry in HttpRuntime.Cache)
				{
					var k = entry.Key as string;

					if (k != null && k.StartsWith("MCC@"))
						cache.Remove(k);
				}
		}
	}
}
