using System;
using System.Runtime.InteropServices;

namespace White
{
	// Token: 0x0200001F RID: 31
	internal static class NativeMethods
	{
		// Token: 0x06000097 RID: 151
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x06000098 RID: 152
		[DllImport("Kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] ref bool isDebuggerPresent);
	}
}
