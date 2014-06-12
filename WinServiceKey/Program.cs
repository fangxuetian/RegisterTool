using System.ServiceProcess;

namespace WinServiceKey
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new ServiceKey() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
