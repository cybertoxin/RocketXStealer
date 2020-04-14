using System;
using System.IO;

namespace c0unt
{
	// Token: 0x02000014 RID: 20
	internal class count
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000052E4 File Offset: 0x000034E4
		public static int GetCrypto()
		{
			int result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Crypto");
				if (flag)
				{
					string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Crypto");
					int num = directories.Length;
					result = num;
				}
				else
				{
					int num2 = 0;
					result = num2;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00005350 File Offset: 0x00003550
		public static int GetJabber()
		{
			int result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Jabber");
				if (flag)
				{
					string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Jabber");
					int num = directories.Length;
					result = num;
				}
				else
				{
					int num2 = 0;
					result = num2;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000053BC File Offset: 0x000035BC
		public static int GetDesktop()
		{
			int result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Desktop");
				if (flag)
				{
					string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Desktop");
					int num = files.Length;
					result = num;
				}
				else
				{
					int num2 = 0;
					result = num2;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00005428 File Offset: 0x00003628
		public static int GetDiscord()
		{
			int result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Discord");
				if (flag)
				{
					string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Discord");
					int num = files.Length;
					result = num;
				}
				else
				{
					int num2 = 0;
					result = num2;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00005494 File Offset: 0x00003694
		public static int GetSteam()
		{
			int result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/steam");
				if (flag)
				{
					string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/steam");
					int num = directories.Length;
					result = num;
				}
				else
				{
					int num2 = 0;
					result = num2;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}
	}
}
