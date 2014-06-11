using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;
using System.Threading;

namespace WinTest
{
	public partial class frMainKey : Form
	{
		public frMainKey()
		{
			InitializeComponent();
			lblLog.Text = "";
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string CurrentDirectory = System.Environment.CurrentDirectory;

			//if (!File.Exists("C:\\WINDOWS\\Winlog.txt"))
			//{
			//    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\WINDOWS\\Winlog.txt", true))
			//    {
			//        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
			//    }
			//}

			string targetPath = "C:\\WINDOWS\\Key\\";
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);//路径不存在则创建路径(首次安装)
			}
			//拷贝注册程序文件
			string[] files = System.IO.Directory.GetFiles(CurrentDirectory + "\\Key");
			string fileName = string.Empty;
			string destFile = string.Empty;
			foreach (string s in files)
			{
				fileName = System.IO.Path.GetFileName(s);
				destFile = System.IO.Path.Combine(targetPath, fileName);
				if (!File.Exists(destFile))//不存在则拷贝过去
				{
					System.IO.File.Copy(s, destFile, true);
				}
			}

			targetPath = "C:\\WINDOWS\\Service\\";
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);//路径不存在则创建路径(首次安装)
			}

			//拷贝服务程序文件
			files = System.IO.Directory.GetFiles(CurrentDirectory + "\\Service");
			fileName = string.Empty;
			destFile = string.Empty;
			foreach (string s in files)
			{
				fileName = System.IO.Path.GetFileName(s);
				destFile = System.IO.Path.Combine(targetPath, fileName);
				if (!File.Exists(destFile))//不存在则拷贝过去
				{
					System.IO.File.Copy(s, destFile, true);
				}
			}

			System.Environment.CurrentDirectory = "C:\\WINDOWS\\Service";
			Process process = new Process();
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.FileName = "Install.bat";
			process.StartInfo.CreateNoWindow = true;
			process.Start();
			lblLog.Text = "安装成功!";
			System.Environment.CurrentDirectory = CurrentDirectory;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			//ServiceController serviceController = new ServiceController("ServiceTest");
			//serviceController.Start();
			if (IsServiceExisted("ServiceKey") == true)
			{
				using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController("ServiceKey"))
				{
					if (control.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
					{
						control.Start();
					}
				}

				lblLog.Text = "服务已启动！";
			}
			else
			{
				lblLog.Text = "请先点击“安装服务”然后再启动服务！";
			}
		}

		private bool IsServiceExisted(string serviceName)
		{
			ServiceController[] services = ServiceController.GetServices();
			foreach (ServiceController s in services)
			{
				if (s.ServiceName == serviceName)
				{
					return true;
				}
			}
			return false;
		}


		private void btnInstall_Click(object sender, EventArgs e)
		{
			if (IsServiceExisted("ServiceKey") == true)
			{
				using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController("ServiceKey"))
				{
					if (control.Status == System.ServiceProcess.ServiceControllerStatus.Running)
					{
						control.Stop();
					}
				}

				lblLog.Text = "服务已启动！";
			}

			string CurrentDirectory = System.Environment.CurrentDirectory;
			System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
			Process process = new Process();
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.FileName = "Uninstall.bat";
			process.StartInfo.CreateNoWindow = true;
			process.Start();
			lblLog.Text = "卸载完成!";
			System.Environment.CurrentDirectory = CurrentDirectory;

			CloseProcess();
			Thread.Sleep(3000);
			string path = @"C:\Windows\Key";
			try
			{
				if (Directory.Exists(path))
				{
					string[] files = System.IO.Directory.GetFiles(path);
					foreach (string s in files)
					{
						if (File.Exists(s))
						{
							File.Delete(s);
						}
					}
				}
				path = @"C:\Windows\Service";
				if (Directory.Exists(path))
				{
					//拷贝服务程序文件
					string[] files = System.IO.Directory.GetFiles(path);
					foreach (string s in files)
					{
						if (File.Exists(s))
						{
							File.Delete(s);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void CloseProcess()
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName.Equals("RegisterTool"))
				{
					try
					{
						process.Kill();
					}
					catch { }
				}
			}
		}
	}
}
