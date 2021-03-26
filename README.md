### Windows 10 audio device, Philips Hue and TV contoller bodge script (.NET C#)

Listens for hotkeys (Ctrl + Shift + F1-F12) and triggers the following events:
- Ctrl + Shift + F1: audio output to headphones (NirCmd)
- Ctrl + Shift + F2: audio output to desktop speakers (NirCmd)
- Ctrl + Shift + F3: audio output to TV speakers (NirCmd)
- Ctrl + Shift + F5: turn TV on (HDMI LibCEC)
- Ctrl + Shift + F6: turn TV off (HDMI LibCEC)
- Ctrl + Shift + F9: lights on (Philips Hue API, curl)
- Ctrl + Shift + F10: light scene dim (Philips Hue API, curl)
- Ctrl + Shift + F11: light scene night (Philips Hue API, curl)
- Ctrl + Shift + F12: lights off (Philips Hue API, curl)

#### Depends on
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [NirCmd](https://www.nirsoft.net/utils/nircmd.html)
- [LibCEC](http://libcec.pulse-eight.com/Downloads)
- [Curl](https://curl.se/windows/)
