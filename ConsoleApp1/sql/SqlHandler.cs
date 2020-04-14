using System;
using System.IO;
using System.Text;

namespace sql
{
	// Token: 0x02000010 RID: 16
	internal class SqlHandler
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00003FD0 File Offset: 0x000021D0
		public SqlHandler(string fileName)
		{
			this._fileBytes = File.ReadAllBytes(fileName);
			this._pageSize = this.ConvertToULong(16, 2);
			this._dbEncoding = this.ConvertToULong(56, 4);
			this.ReadMasterTable(100L);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004034 File Offset: 0x00002234
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

		// Token: 0x06000024 RID: 36 RVA: 0x000040A0 File Offset: 0x000022A0
		public int GetRowCount()
		{
			int result;
			try
			{
				result = this._tableEntries.Length;
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000040D4 File Offset: 0x000022D4
		private bool ReadTableFromOffset(ulong offset)
		{
			bool result;
			try
			{
				bool flag = this._fileBytes[(int)(checked((IntPtr)offset))] == 13;
				if (flag)
				{
					ushort num = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
					int num2 = 0;
					bool flag2 = this._tableEntries != null;
					if (flag2)
					{
						num2 = this._tableEntries.Length;
						Array.Resize<SqlHandler.TableEntry>(ref this._tableEntries, this._tableEntries.Length + (int)num + 1);
					}
					else
					{
						this._tableEntries = new SqlHandler.TableEntry[(int)(num + 1)];
					}
					for (ushort num3 = 0; num3 <= num; num3 += 1)
					{
						ulong num4 = this.ConvertToULong((int)offset + 8 + (int)(num3 * 2), 2);
						bool flag3 = offset != 100UL;
						if (flag3)
						{
							num4 += offset;
						}
						int num5 = this.Gvl((int)num4);
						this.Cvl((int)num4, num5);
						int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL));
						this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL), num6);
						ulong num7 = num4 + (ulong)((long)num6 - (long)num4 + 1L);
						int num8 = this.Gvl((int)num7);
						int num9 = num8;
						long num10 = this.Cvl((int)num7, num8);
						SqlHandler.RecordHeaderField[] array = null;
						long num11 = (long)(num7 - (ulong)((long)num8) + 1UL);
						int num12 = 0;
						while (num11 < num10)
						{
							Array.Resize<SqlHandler.RecordHeaderField>(ref array, num12 + 1);
							int num13 = num9 + 1;
							num9 = this.Gvl(num13);
							array[num12].Type = this.Cvl(num13, num9);
							array[num12].Size = (long)((array[num12].Type <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[num12].Type))]) : ((ulong)((!SqlHandler.IsOdd(array[num12].Type)) ? ((array[num12].Type - 12L) / 2L) : ((array[num12].Type - 13L) / 2L))));
							num11 = num11 + (long)(num9 - num13) + 1L;
							num12++;
						}
						bool flag4 = array != null;
						if (flag4)
						{
							this._tableEntries[num2 + (int)num3].Content = new string[array.Length];
							int num14 = 0;
							for (int i = 0; i <= array.Length - 1; i++)
							{
								bool flag5 = array[i].Type > 9L;
								if (flag5)
								{
									bool flag6 = !SqlHandler.IsOdd(array[i].Type);
									if (flag6)
									{
										bool flag7 = this._dbEncoding == 1UL;
										if (flag7)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
										}
										else
										{
											bool flag8 = this._dbEncoding == 2UL;
											if (flag8)
											{
												this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
											}
											else
											{
												bool flag9 = this._dbEncoding == 3UL;
												if (flag9)
												{
													this._tableEntries[num2 + (int)num3].Content[i] = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
												}
											}
										}
									}
									else
									{
										this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
									}
								}
								else
								{
									this._tableEntries[num2 + (int)num3].Content[i] = Convert.ToString(this.ConvertToULong((int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size));
								}
								num14 += (int)array[i].Size;
							}
						}
					}
				}
				else
				{
					bool flag10 = this._fileBytes[(int)(checked((IntPtr)offset))] == 5;
					if (flag10)
					{
						ushort num15 = (ushort)(this.ConvertToULong((int)(offset + 3UL), 2) - 1UL);
						for (ushort num16 = 0; num16 <= num15; num16 += 1)
						{
							ushort num17 = (ushort)this.ConvertToULong((int)offset + 12 + (int)(num16 * 2), 2);
							this.ReadTableFromOffset((this.ConvertToULong((int)(offset + (ulong)num17), 4) - 1UL) * this._pageSize);
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

		// Token: 0x06000026 RID: 38 RVA: 0x000045D0 File Offset: 0x000027D0
		private void ReadMasterTable(long offset)
		{
			try
			{
				byte b = this._fileBytes[(int)(checked((IntPtr)offset))];
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
							Array.Resize<SqlHandler.SqliteMasterEntry>(ref this._masterTableEntries, this._masterTableEntries.Length + (int)num + 1);
						}
						else
						{
							this._masterTableEntries = new SqlHandler.SqliteMasterEntry[num + 1UL];
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
							int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL));
							this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL), num6);
							ulong num7 = num4 + (ulong)((long)num6 - (long)num4 + 1L);
							int num8 = this.Gvl((int)num7);
							int num9 = num8;
							long num10 = this.Cvl((int)num7, num8);
							long[] array = new long[5];
							for (int i = 0; i <= 4; i++)
							{
								int startIdx = num9 + 1;
								num9 = this.Gvl(startIdx);
								array[i] = this.Cvl(startIdx, num9);
								array[i] = (long)((array[i] <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[i]))]) : ((ulong)((!SqlHandler.IsOdd(array[i])) ? ((array[i] - 12L) / 2L) : ((array[i] - 13L) / 2L))));
							}
							bool flag3 = this._dbEncoding == 1UL || this._dbEncoding == 2UL;
							if (flag3)
							{
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

		// Token: 0x06000027 RID: 39 RVA: 0x00004A90 File Offset: 0x00002C90
		public bool ReadTable(string tableName)
		{
			bool result;
			try
			{
				int num = -1;
				for (int i = 0; i <= this._masterTableEntries.Length; i++)
				{
					bool flag = string.Compare(this._masterTableEntries[i].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0;
					if (flag)
					{
						num = i;
						break;
					}
				}
				bool flag2 = num == -1;
				if (flag2)
				{
					result = false;
				}
				else
				{
					string[] array = this._masterTableEntries[num].SqlStatement.Substring(this._masterTableEntries[num].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(new char[]
					{
						','
					});
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
					result = this.ReadTableFromOffset((ulong)((this._masterTableEntries[num].RootNum - 1L) * (long)this._pageSize));
				}
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004C24 File Offset: 0x00002E24
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

		// Token: 0x06000029 RID: 41 RVA: 0x00004C8C File Offset: 0x00002E8C
		private int Gvl(int startIdx)
		{
			int result;
			try
			{
				bool flag = startIdx > this._fileBytes.Length;
				if (flag)
				{
					result = 0;
				}
				else
				{
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
							return i;
						}
					}
					result = startIdx + 8;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004D1C File Offset: 0x00002F1C
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

		// Token: 0x0600002B RID: 43 RVA: 0x00004E88 File Offset: 0x00003088
		private static bool IsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x04000001 RID: 1
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

		// Token: 0x04000002 RID: 2
		private readonly ulong _dbEncoding;

		// Token: 0x04000003 RID: 3
		private readonly byte[] _fileBytes;

		// Token: 0x04000004 RID: 4
		private readonly ulong _pageSize;

		// Token: 0x04000005 RID: 5
		private string[] _fieldNames;

		// Token: 0x04000006 RID: 6
		private SqlHandler.SqliteMasterEntry[] _masterTableEntries;

		// Token: 0x04000007 RID: 7
		private SqlHandler.TableEntry[] _tableEntries;

		// Token: 0x02000023 RID: 35
		private struct RecordHeaderField
		{
			// Token: 0x0400002E RID: 46
			public long Size;

			// Token: 0x0400002F RID: 47
			public long Type;
		}

		// Token: 0x02000024 RID: 36
		private struct TableEntry
		{
			// Token: 0x04000030 RID: 48
			public string[] Content;
		}

		// Token: 0x02000025 RID: 37
		private struct SqliteMasterEntry
		{
			// Token: 0x04000031 RID: 49
			public string ItemName;

			// Token: 0x04000032 RID: 50
			public long RootNum;

			// Token: 0x04000033 RID: 51
			public string SqlStatement;
		}
	}
}
