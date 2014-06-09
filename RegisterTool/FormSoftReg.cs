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
		SoftReg sr;
		bool isRegistered = false;
        string strDateTime = string.Empty;
        System.Timers.Timer timerCheck = null;
        string strCpuL = string.Empty;

        bool isGet = false;
		public delegate void RegisterHandle(bool isSuccessed);
		public event RegisterHandle RegisterEvent;
		public void OnRegisterEvent(bool isSuccessed)
		{
			if (RegisterEvent != null)
			{
				RegisterEvent(isSuccessed);
			}
		}

		public FormSoftReg()
		{
			InitializeComponent();

             strDateTime = FileConfig.GetConfigValue("DateTime");
             if (strDateTime.Trim().Equals("FXCHKKJ128"))
             {
                 strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                 FileConfig.SetValue("DateTime", strDateTime);
             }

             timerCheck = new    System.Timers.Timer();
             timerCheck.Interval = 60000;
             timerCheck.Elapsed += new System.Timers.ElapsedEventHandler(timerCheck_Elapsed);

		}

        void timerCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("KJ128NMainRun");
            if (localByName.Length >= 1)
            {              

            }
            else
            {
                timerCheck.Stop();
                isGet = true;
                Application.Exit();
            }

            SetMessage();
        }

        private delegate void SetText(string text);
        private void SetlblCounts(string text)
        {
            label8.Text = text;
        }

        int iDay = 0;
        private void SetMessage()
        {
            

            string correctNum = sr.GetRNum(strCpuL);
            string regNum = "";


            try
            {
                regNum = FileConfig.GetConfigValue("128");
                regNum = regNum.Replace("-", "");


                
                if (!correctNum.Equals(regNum))//注册码错误
                {
                    strDateTime = FileConfig.GetConfigValue("DateTime");
                    if (strDateTime.Trim().Equals(""))
                    {
                        strDateTime = DateTime.Now.AddDays(-11).ToString("yyyy-MM-dd HH:mm:ss");
                        FileConfig.SetValue("DateTime", strDateTime);
                    }
                    DateTime dtCheck = DateTime.Now;
                    try
                    {
                        dtCheck = Convert.ToDateTime(strDateTime);
                    }
                    catch
                    {
                        strDateTime = DateTime.Now.AddDays(-11).ToString("yyyy-MM-dd HH:mm:ss");
                        FileConfig.SetValue("DateTime", strDateTime);
                        dtCheck = Convert.ToDateTime(strDateTime);
 
                    }
                    DateTime dtNow = DateTime.Now;
                    TimeSpan ts = dtNow - dtCheck;
                    if (ts.TotalDays < 0)
                    {
                        strDateTime = dtNow.AddDays(-11).ToString("yyyy-MM-dd HH:mm:ss");
                        FileConfig.SetValue("DateTime", strDateTime);

                    }
                    else if (ts.TotalDays > 10)
                    {
                        label8.Invoke(new SetText(this.SetlblCounts), new object[] { "人员定位软件试用期已过\n  系统将随时关闭！" });
                        //关闭非法程序
                       // CloseProcess();
                    }
                    else
                    {
                        iDay = Convert.ToInt32( 10 - ts.TotalDays);
                        label8.Invoke(new SetText(this.SetlblCounts), new object[] { "人员定位软件试用期已过\n  系统在"+iDay+"天后自动关闭！" });

                    }                    
                }
                else
                {
                    isGet = true;
                    Application.Exit();
                   

                }
            }
            catch
            {
            }
        }
		private void FormSoftReg_Load(object sender, EventArgs e)
		{
            try
            {
                
                sr = new SoftReg();

                //1.获取机器序列号
                strCpuL = sr.GetMNum();
                tbDiskNumber.Text = strCpuL;
                SetMessage();
                if (isGet == false)
                {
                    timerCheck.Start();
                    FormBorderStyle = FormBorderStyle.None;

                    this.TopMost = true;
                }
                else
                {
                    Application.Exit();
                    CloseProcess();
                }
               

               
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("D:\\Error.txt","授权程序启动失败！\n\r"+ex.Message.ToString()+"\n\r",Encoding.Default);
            }
		}

		private void btnReg_Click(object sender, EventArgs e)
		{
			string correctNum = sr.GetRNum(tbDiskNumber.Text);
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

			if (correctNum.Equals(buildNum))
			{
				//存入配置文件
				buildNum = buildNum.Insert(4, "-");
				buildNum = buildNum.Insert(9, "-");
				buildNum = buildNum.Insert(14, "-");
				buildNum = buildNum.Insert(19, "-");
				buildNum = buildNum.Insert(24, "-");
				FileConfig.SetValue("128", buildNum);
				OnRegisterEvent(true);
				isRegistered = true;
				MessageBox.Show("注册成功！");

                Application.Exit();
			}
			else
			{
				OnRegisterEvent(false);
				isRegistered = false;
				MessageBox.Show("注册失败！请重新输入注册码。");
			}
		}

		private void FormSoftReg_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!isRegistered)
			{
                //if (MessageBox.Show("软件尚未注册成功，是否立即退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                //{
                //    e.Cancel = true;
                //}
                //MessageBox.Show("软件尚未注册成功，请联系湖南三恒客服中心！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (isGet == false)
                {
                    e.Cancel = true;
                }
			}
		}

        private void CloseProcess()
        {

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals("FormSoftReg"))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch { }
                    // break;
                }            
           
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

        }
	}
}
