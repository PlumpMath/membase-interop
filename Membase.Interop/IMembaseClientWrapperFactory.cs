using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Membase;
using Membase.Configuration;
using System.Runtime.InteropServices;

namespace Membase.Interop
{
	[Guid("742c7d81-4c83-48b5-a59d-01565aaea33c"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IMemcachedClientWrapperFactory
	{
		[return: MarshalAs(UnmanagedType.IDispatch)]
		IMemcachedClientWrapper Create(string configPath);
	}

	[Guid("03879c02-340d-4aaa-b317-f790ded01084"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IMembaseClientWrapperFactory
	{
		[return: MarshalAs(UnmanagedType.IDispatch)]
		IMemcachedClientWrapper Create(string configPath);

		[return: MarshalAs(UnmanagedType.IDispatch)]
		IMemcachedClientWrapper CreateWithBucket(string configPath, string bucketName);
	}
}
