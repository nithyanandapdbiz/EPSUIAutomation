using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EPS.Automation
{
    /// <summary>
    /// This class handles the processes.
    /// </summary>
    internal class BrowserProcessFactory
    {
        /// <summary>
        ///  Provides all the processes currently running.
        /// </summary>
        /// <returns></returns>
        public static Process[] GetProcessInstance()
        {
            Process[] processes = GetCommonProcess();
            processes.Union(Process.GetProcessesByName("IEDriverServer"));
            processes.Union(Process.GetProcessesByName("QTAgent32"));
            return processes;
        }

        /// <summary>
        /// Gets the the processes.
        /// </summary>
        /// <returns>Processes to be killed.</returns>
        public static Process[] GetCommonProcess()
        {
            string browserName = ConfigurationManager.AppSettings["Browser"];
            Process[] processes = null;
            switch (browserName)
            {
                case BaseTestFixture.InternetExplorer: processes = Process.GetProcessesByName("iexplore"); break;
                case BaseTestFixture.FireFox: processes = Process.GetProcessesByName("firefox"); break;
                case BaseTestFixture.Safari: processes = Process.GetProcessesByName("safari"); break;
                case BaseTestFixture.Chrome: processes = Process.GetProcessesByName("chrome"); break;
            }

            if (processes != null)
            {
                processes.Union(Process.GetProcessesByName("IEDriverServer.exe"));
                processes.Union(Process.GetProcessesByName("chromedriver.exe"));
            }
            return processes;
        }
    }
}