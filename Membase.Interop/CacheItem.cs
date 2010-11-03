using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Membase.Interop
{
	[Guid("948f831b-d509-44cb-ba2d-274e4c6dfffd"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Membase.Interop.CacheItem")]
	public class CacheItem : ICacheItem
	{
		public string Key { get; set; }
		public object Value { get; set; }

		string ICacheItem.Key
		{
			get { return this.Key; }
		}

		object ICacheItem.Value
		{
			get { return this.Value; }
		}
	}
}
