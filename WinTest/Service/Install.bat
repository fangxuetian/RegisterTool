%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil.exe WinServiceKey.exe
Net Start ServiceKey
sc config ServiceKey start= auto
::pause