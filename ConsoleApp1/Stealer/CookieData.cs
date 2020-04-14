using System;

namespace Stealer
{
	// Token: 0x02000017 RID: 23
	internal class CookieData
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00007D50 File Offset: 0x00005F50
		public override string ToString()
		{
			return string.Format("{0}\tFALSE\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", new object[]
			{
				this.host_key,
				this.path,
				this.secure.ToUpper(),
				this.expires_utc,
				this.name,
				this.value
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00007DAD File Offset: 0x00005FAD
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00007DB5 File Offset: 0x00005FB5
		public string expires_utc { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00007DBE File Offset: 0x00005FBE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00007DC6 File Offset: 0x00005FC6
		public string host_key { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00007DCF File Offset: 0x00005FCF
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00007DD7 File Offset: 0x00005FD7
		public string name { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00007DE0 File Offset: 0x00005FE0
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public string path { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00007DF1 File Offset: 0x00005FF1
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00007DF9 File Offset: 0x00005FF9
		public string secure { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00007E02 File Offset: 0x00006002
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00007E0A File Offset: 0x0000600A
		public string value { get; set; }
	}
}
