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

namespace Membase.Interop
{
	[Guid("64a3a309-94d4-4d88-a587-1756fb66527c"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Enyim.Caching.Interop.MemcachedClientFactory")]
	public class MemcachedClientWrapperFactory : IMemcachedClientWrapperFactory
	{
		private static Dictionary<string, IMemcachedClientWrapper> cache = new Dictionary<string, IMemcachedClientWrapper>(StringComparer.OrdinalIgnoreCase);

		IMemcachedClientWrapper IMemcachedClientWrapperFactory.Create(string configPath)
		{
			var key = configPath;
			IMemcachedClientWrapper retval;

			if (!cache.TryGetValue(key, out retval))
				lock (cache)
					if (!cache.TryGetValue(key, out retval))
					{
						var config = this.Load(configPath, null);

						cache[key] = retval = new MemcachedClientWrapper(config);
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
				sectionName = "membase";

			var section = cfg.GetSection(sectionName) as IMemcachedClientConfiguration;
			if (section == null) if (!File.Exists(path)) throw new InvalidOperationException("The config section '" + sectionName + "' cannot be found.");

			return section;
		}
	}
}
