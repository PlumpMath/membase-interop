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
using Enyim;
using System.Web;

namespace Membase.Interop
{
	[Guid("c6c892e3-76ae-4684-a449-9d90f1b3bf7a"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Membase.Interop.MembaseClientFactory")]
	public class MembaseClientWrapperFactory : IMembaseClientWrapperFactory
	{
		private static readonly object SyncObj = new Object();

		IMemcachedClientWrapper IMembaseClientWrapperFactory.Create(string configPath)
		{
			//System.Diagnostics.Debugger.Break();

			return ((IMembaseClientWrapperFactory)this).CreateWithBucket(configPath, null);
		}

		IMemcachedClientWrapper IMembaseClientWrapperFactory.CreateWithBucket(string configPath, string bucketName)
		{
			if (String.IsNullOrEmpty(configPath)) throw new ArgumentNullException("configPath");
			if (!File.Exists(configPath)) throw new FileNotFoundException("File not found: " + configPath);

			var key = ("MB@" + configPath + "@" + bucketName).ToUpperInvariant();
			IMemcachedClientWrapper retval;

			var cache = HttpRuntime.Cache;

			if ((retval = cache[key] as IMemcachedClientWrapper) == null)
				lock (cache)
					if ((retval = cache[key] as IMemcachedClientWrapper) == null)
					{
						var config = this.Load(configPath, null);
						retval = new MembaseClientWrapper(config, bucketName);

						cache.Insert(key, retval, new System.Web.Caching.CacheDependency(configPath));
					}

			return retval;
		}

		private IMembaseClientConfiguration Load(string path, string sectionName)
		{
			var cfm = new ConfigurationFileMap();
			cfm.MachineConfigFilename = path;

			var cfg = ConfigurationManager.OpenMappedMachineConfiguration(cfm);
			if (cfg == null) throw new InvalidOperationException("The config file '" + path + "' cannot be loaded.");

			if (String.IsNullOrEmpty(sectionName))
				sectionName = "membase";

			var section = cfg.GetSection(sectionName) as IMembaseClientConfiguration;
			if (section == null) throw new InvalidOperationException("The config section '" + sectionName + "' cannot be found in the config file: '" + path + "'.");

			return section;
		}

		void IMembaseClientWrapperFactory.ClearCachedClients()
		{
			lock (SyncObj) FactoryHelper.ClearCache("MB@");
		}

		string IMembaseClientWrapperFactory.GetLibraryVersion()
		{
			return FactoryHelper.GetAssemblyVersion();
		}

		void IMembaseClientWrapperFactory.SetLogPath(string path)
		{
			FactoryHelper.ConfigureLogger(path);
		}
	}
}
