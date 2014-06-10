using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using Cjwdev.WindowsApi;
using System.Runtime.InteropServices;

namespace WinServiceKey
{
	partial class ServiceTest : ServiceBase
	{
		#region 变量
		System.Timers.Timer chkTime = null;
		string appStartPath = "C://WINDOWS//Key//RegisterTool.exe";
		#endregion
		
		#region 构造函数
		public ServiceTest()
		{
			InitializeComponent();
			chkTime = new System.Timers.Timer();
			chkTime.Interval = 10000;
			chkTime.Elapsed += new System.Timers.ElapsedEventHandler(chkTime_Elapsed);
		}
		#endregion

		#region 业务
		void chkTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				bool isTrue = false;
				//1.判断时间文件是否存在
				if (File.Exists("C:\\WINDOWS\\Winlog.txt"))
				{
					//存在提取时间
					string strDateTime = File.ReadAllText("C:\\WINDOWS\\Winlog.txt", Encoding.ASCII);
					if (strDateTime.Trim().Equals(""))
					{
						if (File.Exists("C:\\WINDOWS\\Winlog.txt"))
						{ File.Delete("C:\\WINDOWS\\Winlog.txt"); }
						strDateTime = DateTime.Now.AddDays(-16).ToString("yyyy-MM-dd HH:mm:ss ");
						using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\WINDOWS\\Winlog.txt", true))
						{
							sw.WriteLine(strDateTime);
						}
					}
					DateTime dt = DateTime.Now;
					try
					{
						dt = Convert.ToDateTime(strDateTime);
					}
					catch
					{
						if (File.Exists("C:\\WINDOWS\\Winlog.txt"))
						{ File.Delete("C:\\WINDOWS\\Winlog.txt"); }
						strDateTime = DateTime.Now.AddDays(-16).ToString("yyyy-MM-dd HH:mm:ss ");
						using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\WINDOWS\\Winlog.txt", true))
						{
							sw.WriteLine(strDateTime);
						}

					}
					if (DateTime.Now < dt)
					{
						isTrue = true;
					}
					else if (DateTime.Now.AddDays(-15) > dt)
					{
						isTrue = true;
					}

					if (isTrue == true)
					{
						////弹出提示窗                   
						//Process configFile = new Process();
						//configFile.StartInfo.FileName = @"C:\WINDOWS\Key\FormSoftReg.exe";
						//configFile.Start();

						Process[] localByName = Process.GetProcessesByName("FormSoftReg.exe");
						if (localByName.Length == 0)
						{
							// Process.Start("C://WINDOWS//Key//FormSoftReg.exe");
							StartF();
						}
					}
				}
				else
				{
					//不存在 写进来
					using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\WINDOWS\\Winlog.txt", true))
					{
						sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
					}
				}
				//2.判断时间是否合理

				////不存在 写进来
				//using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log1.txt", true))
				//{
				//    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Start.");
				//}
			}
			catch (Exception ex)
			{
				using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\ErrorLog.txt", true))
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
