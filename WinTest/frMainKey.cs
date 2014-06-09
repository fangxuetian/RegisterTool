using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;

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

            if (File.Exists("C:\\WINDOWS\\log.txt"))
            {
                File.Delete("C:\\WINDOWS\\log.txt");
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\WINDOWS\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
            }

            //将文件夹拷贝过去
            string sourcePath = "C:\\WINDOWS\\Key\\";
            if (!Directory.Exists(sourcePath))
            {
                //文件夹不存在 就考过过去
                Directory.CreateDirectory(sourcePath);

                string[] files = System.IO.Directory.GetFiles(CurrentDirectory + "\\Key");
                string fileName = string.Empty;
                string destFile = string.Empty;
                foreach (string s in files)
                {
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(sourcePath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }


            sourcePath = "C:\\WINDOWS\\Service\\";
            if (!Directory.Exists(sourcePath))
            {
                //文件夹不存在 就考过过去
                Directory.CreateDirectory(sourcePath);

                string[] files = System.IO.Directory.GetFiles(CurrentDirectory + "\\Service");
                string fileName = string.Empty;
                string destFile = string.Empty;
                foreach (string s in files)
                {
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(sourcePath, fileName);
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
            if (IsServiceExisted("ServiceTest") == true)
            {
                using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController("ServiceTest"))
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
            if (IsServiceExisted("ServiceTest") == true)
            {
                using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController("ServiceTest"))
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
            // CloseProcess();



        }



    }
}
