using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinServiceKey
{
    public partial class Service1 : ServiceBase
    {
        private Thread MainThread;

        public Service1()
        {
            InitializeComponent();
            MainThread = new Thread(new ThreadStart(ThreadFunc));
            MainThread.Priority = ThreadPriority.Lowest;
        }

        protected override void OnStart(string[] args)
        {
            MainThread.Start();
        }

        protected override void OnStop()
        {
            MainThread.Abort();
        }

        public static void ThreadFunc()
        {
            int LastHour = DateTime.Now.Minute;
            while (true)
            {
                System.Threading.Thread.Sleep(6000);
                if (DateTime.Now.Minute - 1 == LastHour)
                {
                    MessageBox.Show("为了爱护您的眼睛，请您暂时休息5分钟并向远处眺望！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    LastHour = DateTime.Now.Minute;

                }
            }
        } 
         
    }
}
