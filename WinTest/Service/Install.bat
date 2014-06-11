%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil.exe WinServiceKey.exe
Net Start ServiceTest
sc config ServiceTest start= auto
::pause