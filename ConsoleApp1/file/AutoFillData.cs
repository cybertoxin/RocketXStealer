using System;

namespace file
{
	// Token: 0x0200001C RID: 28
	internal class AutoFillData
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public override string ToString()
		{
			return string.Format("――――――――――――――――――――――――――――――――――――――――――――\r\nName  | {0}\r\nValue | {1}\r\n", this.Name, this.Value);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000A618 File Offset: 0x00008818
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000A620 File Offset: 0x00008820
		public string Name { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000A629 File Offset: 0x00008829
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000A631 File Offset: 0x00008831
		public string Value { get; set; }
	}
}
