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
	public class MemcachedClientWrapper : MemcachedClientWrapperBase
	{
		public MemcachedClientWrapper(IMemcachedClientConfiguration config)
			: base(new MemcachedClient(config)) { }
	}
}
