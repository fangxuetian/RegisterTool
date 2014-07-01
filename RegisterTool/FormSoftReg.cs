using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace RegisterTool
{
	public partial class FormSoftReg : Form
	{
		#region 变量
		SoftReg softReg = new SoftReg();
		System.Timers.Timer timerCheck = null;
		DateTime lastRegDate;
		string serialNum;
		string registerNumConfig;
		IList<RegisterTool.WindowsInfo> listSysWindows;
		private IntPtr activeWinHandle;
		private RECT winRect;
		public string registerNum;
		#endregion

		#region 属性
		/// <summary>
		/// 从配置文件获取上次注册时间或更新配置文件上次注册时间
		/// </summary>
		public DateTime LastRegDate
		{
			get
			{
				string dt = FileConfig.GetConfigValue("LastDateTime");
				if (dt != "")
				{
					return Convert.ToDateTime(dt);
				}
				else
				{
					LastRegDate = DateTime.Now;
					return lastRegDate;
				}
			}
			set
			{
				lastRegDate = value;
				FileConfig.SetValue("LastDateTime", lastRegDate.ToString("yyyy-MM-dd HH:mm:ss"));
			}
		}

		/// <summary>
		/// 唯一序列号，用于生成注册码
		/// </summary>
		public string SerialNum
		{
			get
			{
				return softReg.GetMNum("128", LastRegDate);//获取机器序列号
			}
			set
			{
				serialNum = value;
			}
		}

		/// <summary>
		/// 根据序列号生成注册码
		/// </summary>
		public string RegisterNum
		{
			get
			{
				return softReg.GetRNum(SerialNum);
			}
			set
			{
				registerNum = value;
			}
		}

		/// <summary>
		/// 从配置文件获取或设置注册码
		/// </summary>
		public string RegisterNumConfig
		{
			get
			{
				return FileConfig.GetConfigValue("128");
			}
			set
			{
				registerNumConfig = value;
				FileConfig.SetValue("128", registerNumConfig);
			}
		}

		/// <summary>
		/// 从配置文件获取注册期限天数
		/// </summary>
		public int CheckDays
		{
			get
			{
				return int.Parse(FileConfig.GetConfigValue("CheckDays"));
			}
		}

		/// <summary>
		/// 从配置文件获取注册期限天数
		/// </summary>
		public int PreAlertDays
		{
			get
			{
				return int.Parse(FileConfig.GetConfigValue("PreAlertDays"));
			}
		}

		/// <summary>
		/// 当前获得焦点的窗口
		/// </summary>
		public IntPtr ActiveWinHandle
		{
			get
			{
				return API.GetForegroundWindow();
			}
			set
			{
				activeWinHandle = value;
			}
		}

		/// <summary>
		/// 获取当前获得焦点的窗口的rect
		/// </summary>
		public RECT WinRect
		{
			get
			{
				API.GetWindowRect(ActiveWinHandle, ref winRect);
				return winRect;
			}
		}

		/// <summary>
		/// 获取当前获得焦点的窗口的宽
		/// </summary>
		public int WinWidth
		{
			get
			{
				return WinRect.Right - WinRect.Left;
			}
		}

		/// <summary>
		/// 获取当前获得焦点的窗口的高
		/// </summary>
		public int WinHeight
		{
			get
			{
				return WinRect.Bottom - WinRect.Top;
			}
		}

		/// <summary>
		/// 根据最后一次注册日期和注册限制天数判断当前应该采取的操作
		/// </summary>
		public OperateType _OperateType
		{
			get
			{
				if (DateTime.Now.AddDays(PreAlertDays - CheckDays) >= lastRegDate && DateTime.Now.AddDays(-CheckDays) <= lastRegDate)//警告
				{
					return OperateType.Alert;
				}
				else if (DateTime.Now.AddDays(-CheckDays) > lastRegDate)
				{
					return OperateType.ShutDown;
				}
				else
				{
					return OperateType.Normal;
				}
			}
		}
		#endregion

		#region 构造函数
		public FormSoftReg()
		{
			InitializeComponent();
			timerCheck = new System.Timers.Timer();
			timerCheck.Interval = 5000;
			timerCheck.Elapsed += new System.Timers.ElapsedEventHandler(timerCheck_Elapsed);
			timerCheck.Start();
			registerNum = RegisterNum;
			lastRegDate = LastRegDate;
		}
		#endregion

		#region 业务
		/// <summary>
		/// 定时检查是否已注册
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timerCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			DealRegister();
			GetLeftDays();
		}

		/// <summary>
		/// 获取剩余天数
		/// </summary>
		private void GetLeftDays()
		{
			TimeSpan ts = DateTime.Now - lastRegDate;
			int leftDays = CheckDays - ts.Days;
			this.Invoke(new MethodInvoker(() =>
			{
				lblLeftDay.Text = (leftDays > 0 ? leftDays : 0).ToString();
			}));
		}

		private void FormSoftReg_Load(object sender, EventArgs e)
		{
			tbDiskNumber.Text = SerialNum;
			DealRegister();
			GetLeftDays();
		}

		/// <summary>
		/// 处理是否注册逻辑
		/// </summary>
		public void DealRegister()
		{
			if (CheckRegister())
			{
				this.Hide();
			}
			else
			{
				//System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("KJ128NMainRun");
				Process process128 = Get128Process("KJ128NMainRun");
				if (process128 != null)
				{
					if (_OperateType == OperateType.ShutDown)
					{
						process128.Kill();
					}

					this.Invoke(new MethodInvoker(() =>
					{
						this.WindowState = FormWindowState.Normal;
						this.Show();
					}));
				}
			}
		}

		/// <summary>
		/// 检查128主窗体是否获得焦点
		/// </summary>
		/// <returns></returns>
		private bool CheckMainRunWindowIsActive()
		{
			bool isActive = false;
			listSysWindows = GetWindows.Load();
			foreach (WindowsInfo item in listSysWindows)
			{
				if (item.Title == "KJ128A矿用人员管理系统" || item.Title == "KJ128A型矿用人员管理系统--[主机]" || item.Title == "KJ128A型矿用人员管理系统--[备机]" || item.Title == "KJ128A型矿用人员管理系统--[客户端]")
				{
					if (!item.IsMinimzed && item.Handle == ActiveWinHandle)//非最小化并且获得了焦点
					{
						isActive = true;
						break;
					}
				}
			}

			return isActive;
		}

		/// <summary>
		/// 获取128进程
		/// </summary>
		/// <returns></returns>
		private Process Get128Process(string procName)
		{
			Process processTarget = null;
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName.Equals(procName))
				{
					processTarget = process;
					break;
				}
			}

			return processTarget;
		}

		/// <summary>
		/// 检测是否已经注册
		/// </summary>
		/// <returns></returns>
		private bool CheckRegister()
		{
			try
			{
				if (DateTime.Now.AddDays(-(CheckDays - PreAlertDays)) <= lastRegDate && registerNum.Equals(registerNumConfig))//已注册
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				System.IO.File.AppendAllText("D:\\Error.txt", "授权程序启动失败！\n\r" + ex.Message.ToString() + "\n\r", Encoding.Default);
				return false;
			}
		}

		/// <summary>
		/// 注册逻辑
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReg_Click(object sender, EventArgs e)
		{
			string buildNum = tbRegNum1.Text.Trim()
				+ tbRegNum2.Text.Trim()
				+ tbRegNum3.Text.Trim()
				+ tbRegNum4.Text.Trim()
				+ tbRegNum5.Text.Trim()
				+ tbRegNum6.Text.Trim();

			if (buildNum.Length < 24)
			{
				MessageBox.Show("注册码输入有误，请核实！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (RegisterNum.Equals(buildNum))//注册成功后，更新配置文件的时间为当前时间，根据新的时间生成新的序列号和新的注册码，保存新的注册码
			{
				LastRegDate = DateTime.Now;
				RegisterNumConfig = RegisterNum;
				registerNum = RegisterNumConfig;
				MessageBox.Show("注册成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Hide();
				tbDiskNumber.Text = SerialNum;
				tbRegNum1.Text = "";
				tbRegNum2.Text = "";
				tbRegNum3.Text = "";
				tbRegNum4.Text = "";
				tbRegNum5.Text = "";
				tbRegNum6.Text = "";
			}
			else
			{
				MessageBox.Show("注册失败！请重新输入注册码。");
			}
		}

		/// <summary>
		/// 取消注册处理
		/// </summary>
		private void DealCancelReg()
		{
			this.Hide();
			if (_OperateType == OperateType.ShutDown)
			{
				Process process128 = Get128Process("KJ128NMainRun");
				if (process128 != null)
				{
					process128.Kill();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DealCancelReg();
		}

		private void FormSoftReg_DoubleClick(object sender, EventArgs e)
		{
			DealCancelReg();
		}

		#endregion
	}

	public enum OperateType
	{
		Normal,	//正常
		Alert,	//警告
		ShutDown//强制关闭
	}
}
