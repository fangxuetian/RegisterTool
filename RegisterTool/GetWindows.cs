using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace RegisterTool
{
	/// <summary>
	/// Author：xxh 
	/// CreateTime：2014-06-11 11:02:29 
	/// 获取全部窗体
	/// </summary>
	public class GetWindows
	{
		private static IList<WindowsInfo> _WindowsList = new List<WindowsInfo>();
		private static IntPtr _Statusbar;
		public delegate bool EnumWindowsProc(IntPtr p_Handle, int p_Param);

		private static bool NetEnumWindows(IntPtr p_Handle, int p_Param)
		{
			if (!API.IsWindowVisible(p_Handle))
				return true;
			StringBuilder _TitleString = new StringBuilder(256);
			API.GetWindowText(p_Handle, _TitleString, 256);
			if (string.IsNullOrEmpty(_TitleString.ToString()))
			{
				return true;
			}
			if (_TitleString.Length != 0 || (_TitleString.Length == 0) || p_Handle != _Statusbar)
			{
				_WindowsList.Add(new WindowsInfo(p_Handle, _TitleString.ToString(), API.IsIconic(p_Handle), API.IsZoomed(p_Handle)));
			}
			return true;
		}
		public static IList<WindowsInfo> Load()
		{
			_Statusbar = API.FindWindow("Shell_TrayWnd", "");
			EnumWindowsProc _EunmWindows = new EnumWindowsProc(NetEnumWindows);
			API.EnumWindows(_EunmWindows, 0);
			return _WindowsList;
		}
	}

	public class WindowsInfo
	{
		private IntPtr m_Handle;
		/// <summary>
		/// 句柄
		/// </summary>
		public IntPtr Handle { get { return m_Handle; } set { m_Handle = value; } }
		private string m_Title;
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get { return m_Title; } set { m_Title = value; } }
		private bool m_IsMinimzed;
		/// <summary>
		/// 是否最小
		/// </summary>
		public bool IsMinimzed { get { return m_IsMinimzed; } set { m_IsMinimzed = value; } }
		private bool m_IsMaximized;
		/// <summary>
		/// 是否最大     
		/// </summary>
		public bool IsMaximized { get { return m_IsMaximized; } set { m_IsMaximized = value; } }
		public WindowsInfo()
		{
			m_Handle = IntPtr.Zero;
			m_Title = "";
			m_IsMinimzed = false;
			m_IsMaximized = false;
		}
		public WindowsInfo(IntPtr p_Handle, string p_Title, bool p_IsMinimized, bool p_IsMaximized)
		{
			this.m_Handle = p_Handle;
			this.m_Title = p_Title;
			this.m_IsMinimzed = p_IsMinimized;
			this.m_IsMaximized = p_IsMaximized;
		}
	}

	public class API
	{
		[DllImport("user32.dll")]
		public static extern int EnumWindows(GetWindows.EnumWindowsProc ewp, int lParam);
		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsIconic(IntPtr hWnd);
		[DllImport("user32.dll")]
		public static extern bool IsZoomed(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr CloseWindow(IntPtr hWnd);

	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int Left;                             //最左坐标
		public int Top;                             //最上坐标
		public int Right;                           //最右坐标
		public int Bottom;                        //最下坐标
	}
}
