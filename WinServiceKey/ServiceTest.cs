using System;
using System.Diagnostics;
using System.ServiceProcess;
using Cjwdev.WindowsApi;
using System.Runtime.InteropServices;

namespace WinServiceKey
{
	partial class ServiceKey : ServiceBase
	{
		#region 变量
		System.Timers.Timer chkTime = null;
		string appStartPath = "C://WINDOWS//Key//RegisterTool.exe";
		bool isProtect = false;
		#endregion

		#region 构造函数
		public ServiceKey()
		{
			InitializeComponent();
			chkTime = new System.Timers.Timer();
			chkTime.Interval = 5000;
			chkTime.Elapsed += new System.Timers.ElapsedEventHandler(chkTime_Elapsed);
		}
		#endregion

		#region 业务
		void chkTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				Process[] localByName = Process.GetProcessesByName("RegisterTool");
				if (localByName.Length < 1)
				{
					StartF();
				}
				//if (!isProtect)
				//{
				//    Process[] serviceNames = Process.GetProcessesByName("WinServiceKey");
				//    if (localByName.Length > 0)
				//    {
				//        KProcess.ProtectProcessID = (uint)serviceNames[0].Id;
				//        KProcess.ProtectProcess();
				//        isProtect = true;
				//        using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\ErrorLog.txt", true))
				//        {
				//            sw.WriteLine("protect successful" + (uint)serviceNames[0].Id);
				//        }
				//    }
				//}
			}
			catch (Exception ex)
			{
				using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\ErrorLog.txt", true))
				{
					sw.WriteLine(ex.Message.ToString() + "\r\n");
				}
			}
		}

		protected override void OnStart(string[] args)
		{
			chkTime.Start();
		}

		protected override void OnStop()
		{
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\Syslog.txt", true))
			{
				sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Stop.");
			}
		}

		private void StartF()
		{
			try
			{
				IntPtr userTokenHandle = IntPtr.Zero;
				ApiDefinitions.WTSQueryUserToken(ApiDefinitions.WTSGetActiveConsoleSessionId(), ref userTokenHandle);

				ApiDefinitions.PROCESS_INFORMATION procInfo = new ApiDefinitions.PROCESS_INFORMATION();
				ApiDefinitions.STARTUPINFO startInfo = new ApiDefinitions.STARTUPINFO();
				startInfo.cb = (uint)Marshal.SizeOf(startInfo);

				ApiDefinitions.CreateProcessAsUser(
					userTokenHandle,
					appStartPath,
				  "",
					IntPtr.Zero,
					IntPtr.Zero,
					false,
					0,
					IntPtr.Zero,
					null,
					ref startInfo,
					out procInfo);

				if (userTokenHandle != IntPtr.Zero)
					ApiDefinitions.CloseHandle(userTokenHandle);

				//_currentAquariusProcessId = (int)procInfo.dwProcessId;
			}
			catch (Exception ex)
			{
				//MessageBox.Show(string.Format("Start Application failed, its path is {0} ,exception: {1}", appStartPath, ex.Message));
			}
		}
		#endregion
	}
}
