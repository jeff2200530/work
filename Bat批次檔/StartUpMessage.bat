start C:\Users\Jeff\Desktop\work\StartUpMessage\StartUpMessage\bin\Debug\StartUpMessage.exe
@ping 127.0.0.1 -n 3 -w 1000 > nul

taskkill /F /IM StartUpMessage.exe

exit