using System;
using System.Windows.Forms;
using Hotkeys;

/* 
 * Some hotkey bodge for Win10
 * 
 *    Depends on
 *       - LibCEC
 *       - NirCmd
 *       - Curl
 */

namespace manageri
{
    static class Program
    {
        // To access my lights in my local network. If you visit my place, don't use these for evil things :)
        const string HUEAPIURI = "http://lights.lan/api/";
        const string HUEAPIKEY = "mVre-YKTOr9jk0MzXDJ03Qp9B-ylKSiwkPShlqRa";
        const string HUEAPIHANDLE= "/groups/1/action";

        static void exec(string cmd, string args)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = cmd;
            startInfo.Arguments = args;
            process.StartInfo = startInfo;
            process.Start();
        }

        static void SetAudioDevice(object sender, KeyPressedEventArgs e, string deviceID)
        {
            // Change Windows audio output with NirCmd
            exec("nircmd.exe", "setdefaultsounddevice " + deviceID + " 1");
        }

        static void SetCecStatus(object sender, KeyPressedEventArgs e, bool status)
        {
            if (status)
            {
                // CEC command as turns TV on
                exec("cmd.exe", "/C echo on 0 | cec-client -s");
            } else
            {
                // CEC command standby 0 turns tv off
                exec("cmd.exe", "/C echo standby 0 | cec-client -s");
            }
        }

        static void SetHueScene(object sender, KeyPressedEventArgs e, string status, string sceneHash = "")
        {
            // Send HTTP request to Philips Hue bridge
            exec("curl.exe", "-X PUT -d \"{\\\"on\\\": " + status + ", \\\"scene\\\":\\\"" + sceneHash + "\\\"}\" " + HUEAPIURI + HUEAPIKEY + HUEAPIHANDLE);
        }

        [STAThread]
        static void Main()
        {
            // Init keyboard hooks
            KeyboardHook hookVolHeadphones = new KeyboardHook();
            KeyboardHook hookVolDesktop = new KeyboardHook();
            KeyboardHook hookVolTelevision = new KeyboardHook();

            KeyboardHook hookCecOn = new KeyboardHook();
            KeyboardHook hookCecOff = new KeyboardHook();

            KeyboardHook hookHueDefault = new KeyboardHook();
            KeyboardHook hookHueDimmed = new KeyboardHook();
            KeyboardHook hookHueNight = new KeyboardHook();
            KeyboardHook hookHueOff = new KeyboardHook();


            // Assign callback functions to keyboard hooks
            hookVolHeadphones.KeyPressed += (sender, e) => SetAudioDevice(sender, e, "Headphones");
            hookVolDesktop.KeyPressed += (sender, e) => SetAudioDevice(sender, e, "Desktop");
            hookVolTelevision.KeyPressed += (sender, e) => SetAudioDevice(sender, e, "TV");

            hookCecOn.KeyPressed += (sender, e) => SetCecStatus(sender, e, true);
            hookCecOff.KeyPressed += (sender, e) => SetCecStatus(sender, e, false);

            hookHueDefault.KeyPressed += (sender, e) => SetHueScene(sender, e, "true", "qnuiGiKrE7MojNv");
            hookHueDimmed.KeyPressed += (sender, e) => SetHueScene(sender, e, "true", "lwXIyLbE5VFMI-6");
            hookHueNight.KeyPressed += (sender, e) => SetHueScene(sender, e, "true", "fHZAm2sBa9qRqtJ");
            hookHueOff.KeyPressed += (sender, e) => SetHueScene(sender, e, "false");


            // Set keyboard shortcuts for keyboard hooks
            hookVolHeadphones.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D1);
            hookVolDesktop.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D2);
            hookVolTelevision.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D3);

            hookCecOn.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D5);
            hookCecOff.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D6);

            hookHueDefault.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D8);
            hookHueDimmed.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D9);
            hookHueOff.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, Keys.D0);


            // Run program and hide to systray
            using (NotifyIcon icon = new NotifyIcon())
            {
                icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                icon.Visible = true;
                Application.Run();
                icon.Visible = false;
            }
        }
    }
}
