call u.bat

gacutil /i Enyim.Caching.dll
gacutil /i Membase.dll
gacutil /i Membase.Interop.dll

regasm /unregister Membase.Interop.dll
regasm /verbose Membase.Interop.dll