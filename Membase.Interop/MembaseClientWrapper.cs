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
	public class MembaseClientWrapper : MemcachedClientWrapperBase
	{
		public MembaseClientWrapper(IMembaseClientConfiguration config) : this(config, null) { }
		public MembaseClientWrapper(IMembaseClientConfiguration config, string bucketName)
			: base(new MembaseClient(config, bucketName)) { }
	}
}
