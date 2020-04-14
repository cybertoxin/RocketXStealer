using System;
using System.IO;
using System.Net;

namespace dLL
{
	// Token: 0x02000013 RID: 19
	internal class dll
	{
		// Token: 0x06000033 RID: 51 RVA: 0x0000528C File Offset: 0x0000348C
		public static void dLL(string url)
		{
			using (WebClient webClient = new WebClient())
			{
				string currentDirectory = Directory.GetCurrentDirectory();
				webClient.DownloadFile(url, currentDirectory + "/ICSharpCode.SharpZipLib.dll");
			}
		}
	}
}
