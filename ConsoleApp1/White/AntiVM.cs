using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace White
{
	// Token: 0x0200001E RID: 30
	internal class AntiVM
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000AA14 File Offset: 0x00008C14
		private static bool GetDetectVirtualMachine()
		{
			using (ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem").Get())
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					try
					{
						string text = managementBaseObject["Manufacturer"].ToString().ToLower();
						bool flag = managementBaseObject["Model"].ToString().ToLower().Contains("virtual");
						bool flag2 = (text.Equals("microsoft corporation") && flag) || text.Contains("vmware") || managementBaseObject["Model"].ToString().Equals("VirtualBox");
						if (flag2)
						{
							return true;
						}
					}
					catch (Exception)
					{
						return false;
					}
				}
			}
			return false;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000AB28 File Offset: 0x00008D28
		private static bool IsDebuggerAttached(Process process)
		{
			bool result;
			try
			{
				bool flag = false;
				NativeMethods.CheckRemoteDebuggerPresent(process.Handle, ref flag);
				result = flag;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000AB64 File Offset: 0x00008D64
		private static bool IsRdpAvailable
		{
			get
			{
				return SystemInformation.TerminalServerSession;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000AB74 File Offset: 0x00008D74
		private static bool SBieDLL()
		{
			return Process.GetProcessesByName("wsnm").Length != 0 || NativeMethods.GetModuleHandle("SbieDll.dll").ToInt32() != 0;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000ABA6 File Offset: 0x00008DA6
		public static bool GetCheckVMBot()
		{
			return AntiVM.IsDebuggerAttached(Process.GetCurrentProcess()) || AntiVM.SBieDLL() || AntiVM.IsRdpAvailable || AntiVM.GetDetectVirtualMachine();
		}
	}
}
