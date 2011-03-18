## COM Interop Wrapper Client for Membase

### Installation

You need to use `regasm` to register the assemblies before you can use them. See the included r.cmd, but essentially this is what you have to do:

	gacutil /i Enyim.Caching.dll
	gacutil /i Membase.dll
	gacutil /i Membase.Interop.dll

	regasm /verbose Membase.Interop.dll

If you are upgrading from a previous version you need to unregister the assemblies first:

	gacutil /u Enyim.Caching
	gacutil /u Membase
	gacutil /u Membase.Interop

	regasm /unregister Membase.Interop.dll

(Usually the `regasm /codebase` works too, and this way we can avoid the whole GAC un/registering hassle, but you need to test it in your environment first, because it does COM registration a bit differently.)

### Usage

Clients are created by either the [MemcachedClientWrapperFactory](/enyim/membase-interop/blob/master/Membase.Interop/MemcachedClientWrapperFactory.cs) or [MembaseClientWrapperFactory](/enyim/membase-interop/blob/master/Membase.Interop/MembaseClientWrapperFactory.cs).

	' memcached client factory
	dim mcf
	set mcf = CreateObject("Enyim.Caching.Interop.MemcachedClientFactory")

	' membase client factory
	dim mbf
	set mbf = CreateObject("Membase.Interop.MembaseClientFactory")

The factories only support config file based client creation. The config files are the same as the app/web.config used by the client. See a sample in the Sample folder; documentation is available at [the client's wiki](https://github.com/enyim/EnyimMemcached/wiki).

	dim client
	set client = mcf.Create("C:\asp\configs\memcached.config")

The Membase client factory has a second constructor which can be used to create clients for different buckets, while still using the same configuration file.

	' connect to the default bucket
	set client = mbf.Create("C:\asp\configs\membase.config")

	' connect to the userdata bucket
	set client = mbf.Create("C:\asp\configs\membase.config", "userdata", "userdatapassword")

All factories return an object implementing the [IMemcachedClientWrapper](/enyim/membase-interop/blob/master/Membase.Interop/IMemcachedClientWrapper.cs) interface.

You can enable debug logging by calling the `SetLogPath` method of the factories.

	' enable logging. make sure the directory exists.
	mbf.SetLogPath("C:\temp\debug.log")
	
	' disable logging by passing Null or an empty string
	mbf.SetLogPath("")

Note: logging settings are global, so all clients you created will log (or not) no matter if they were instantiated before or after you specified the path.
