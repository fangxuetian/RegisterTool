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
using System.Xml;

namespace WinTest
{
	public partial class frMainKey : Form
	{
		public string CheckDays
		{
			get
			{
				string days = tbCheckDays.Text.Trim();
				if (days == "")
				{
					return "15";
				}
				else
				{
					return days;
				}
			}
		}

		public string PreAlertDays
		{
			get
			{
				string days = tbPreAlertDays.Text.Trim();
				if (days == "")
				{
					return "10";
				}
				else
				{
					return days;
				}
			}
		}

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

			SetValue("CheckDays", CheckDays);
			SetValue("PreAlertDays", PreAlertDays);

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

		private void btnSaveConfig_Click(object sender, EventArgs e)
		{
			SetValue("CheckDays", CheckDays);
			SetValue("PreAlertDays", PreAlertDays);
			MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		#region 获得配置文件中的值
		public string GetConfigValue(string appKey)
		{
			try
			{
				XmlDocument xDoc = new XmlDocument();
				xDoc.Load(@"C:\Windows\Key\RegisterTool.exe.config");

				XmlNode xNode;
				XmlElement xElem;
				xNode = xDoc.SelectSingleNode("//connectionStrings");
				xElem = (XmlElement)xNode.SelectSingleNode("//add[@name='" + appKey + "']");
				if (xElem != null)
					return xElem.GetAttribute("connectionString");
				else
					return "";
			}
			catch (Exception)
			{
				return "";
			}
		}
		#endregion

		#region 修改配置文件中的值
		public void SetValue(string AppKey, string AppValue)
		{
			try
			{
				XmlDocument xDoc = new XmlDocument();
				//获取可执行文件的路径和名称
				xDoc.Load(@"C:\Windows\Key\RegisterTool.exe.config");

				XmlNode xNode;
				XmlElement xElem1;
				XmlElement xElem2;
				xNode = xDoc.SelectSingleNode("//connectionStrings");

				xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@name='" + AppKey + "']");
				if (xElem1 != null) xElem1.SetAttribute("connectionString", AppValue);
				else
				{
					xElem2 = xDoc.CreateElement("add");
					xElem2.SetAttribute("name", AppKey);
					xElem2.SetAttribute("connectionString", AppValue);
					xNode.AppendChild(xElem2);
				}
				xDoc.Save(@"C:\Windows\Key\RegisterTool.exe.config");
			}
			catch (Exception e)
			{
				//MessageBox.Show("客户端报错" + e.Message.ToString());
			}
		}
		#endregion

		private void frMainKey_Load(object sender, EventArgs e)
		{
			string days = GetConfigValue("CheckDays");
			if (days == "")
			{
				tbCheckDays.Text = "15";
			}
			else
			{
				tbCheckDays.Text = days;
			}

			days = GetConfigValue("PreAlertDays");
			if (days == "")
			{
				tbPreAlertDays.Text = "10";
			}
			else
			{
				tbPreAlertDays.Text = days;
			}
		}
	}
}
