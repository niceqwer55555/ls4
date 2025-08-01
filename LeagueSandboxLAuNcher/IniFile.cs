using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LeagueSandbox_LAN_Server_Launcher
{
    public class IniFile
    {
        private string Path;

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = "ls4lan.ini")
        {
            Path = new FileInfo(IniPath).FullName;
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
                // Initialize default values in the ini file
                Write("Settings", "GameServerPath", "GameServerConsole.exe");
                Write("Settings", "GameInfoFilePath", "Settings\\GameInfo.json");
                Write("Settings", "ContentPath", "../../../../Content");
            }
        }

        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        public string Read(string Section, string Key, string Default = "")
        {
            StringBuilder RetVal = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, Default, RetVal, 255, Path);
            return RetVal.ToString();
        }
    }
}
