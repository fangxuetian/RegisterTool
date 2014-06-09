using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RegisterTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                FormSoftReg fm = new FormSoftReg();


                //判断程序是否已被打开
                System.Diagnostics.Process currentprocess = System.Diagnostics.Process.GetCurrentProcess();
                System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName(currentprocess.ProcessName);
                if (myProcesses.Length > 1)
                {
                    Application.Exit();
                }
                else
                {

                    System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("KJ128NMainRun");
                    if (localByName.Length >= 1)
                    {
                        Application.Run(fm);

                    }
                    else
                    {
                        Application.Exit();
                    }

                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("TS"+e.ToString());
            }


         
        }
    }
}
