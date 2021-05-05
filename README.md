### Windows 10 audio device, Philips Hue and TV contoller bodge script (.NET C#)

Listens for hotkeys (Ctrl + Shift + F1-F12) and triggers the following events:
- Ctrl + Shift + 1: audio output to headphones (NirCmd)
- Ctrl + Shift + 2: audio output to desktop speakers (NirCmd)
- Ctrl + Shift + 3: audio output to TV speakers (NirCmd)
- Ctrl + Shift + 4: turn TV on (HDMI LibCEC)
- Ctrl + Shift + 5: turn TV off (HDMI LibCEC)
- Ctrl + Shift + 8: lights on (Philips Hue API, curl)
- Ctrl + Shift + 9: light scene dim (Philips Hue API, curl)
- Ctrl + Shift + 0: lights off (Philips Hue API, curl)

#### Depends on
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [NirCmd](https://www.nirsoft.net/utils/nircmd.html)
- [LibCEC](http://libcec.pulse-eight.com/Downloads)
- [Curl](https://curl.se/windows/)

For the TV shortcuts to work, you'll need to add the Pulse-Eight LibCEC directory to your Windows PATH variable.
Use the "Edit environment variable" tool and add `C:\Program Files (x86)\Pulse-Eight\USB-CEC Adapter` to `Path`.
