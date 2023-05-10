using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Automation
{
    public class AutomationConfigurationManager
    {
        /// <summary>
        /// Property Comet url root.
        /// </summary>
        public static string ApplicationUrlRoot
        {
            get { return GetApplicationUrltRoot(); }
        }

        /// <summary>
        /// Find course space url root based on application environment.
        /// </summary>
        /// <returns>Application cs url.</returns>
        public static string GetApplicationUrltRoot()
        {
            string applicationUrl;
            string productName = ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.Product_Key].ToUpper();
            string appServerName = ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.AppServerName_Key].ToLower();
            string environment = ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.Environment_Key].ToLower();
            string productURL;
            switch (productName)
            {
                case "MOL":
                    productURL = AutomationConfigurationManagerResource.MOL_URL;
                    applicationUrl = string.Format(productURL, appServerName, environment);
                    break;

                default: throw new ArgumentException("The suggested application environment was not found");
            }
            return applicationUrl;
        }

        /// <summary>
        /// Property application environment.
        /// </summary>
        public static string ApplicationTestEnvironment
        {
            get { return GetApplicationTestEnvironment(); }
        }

        /// <summary>
        /// Find application environment.
        /// </summary>
        /// <returns>Application environment.</returns>
        public static string GetApplicationTestEnvironment()
        {
            return Environment.GetEnvironmentVariable(
                AutomationConfigurationManagerResource.AUTOMATION_TEST_ENVIRONMENT_KEY.ToUpper())
                   ?? ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.Environment_Key].ToUpper();
        }

        /// <summary>
        /// Property application Test Data path.
        /// </summary>
        public static string TestDataPath
        {
            get
            {
                return Environment.GetEnvironmentVariable(AutomationConfigurationManagerResource.MMSG_TESTDATA_PATH_Key)
                    ?? Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) +
                    AutomationConfigurationManagerResource.AutomationConfig_Testdata_Path;
            }
        }

        /// <summary>
        /// Property browser instance.
        /// </summary>
        public static string BrowserExecutionInstance
        {
            get { return GetExecutionBrowser(); }
        }

        /// <summary>
        /// Property application download file path.
        /// </summary>
        public static string DownloadFilePath
        {
            get
            {
                return Environment.GetEnvironmentVariable(AutomationConfigurationManagerResource.DOWNLOAD_PATH_Key)
                       ?? Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName
                           (System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))))
                       + AutomationConfigurationManagerResource.AutomationConfig_DownloadFile_Path;
            }
        }

        /// <summary>
        /// Find execution browser.
        /// </summary>
        /// <returns>Browser instance.</returns>
        private static string GetExecutionBrowser()
        {
            return Environment.GetEnvironmentVariable(
                AutomationConfigurationManagerResource.AUTOMATION_BROWSER_KEY)
                   ?? ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.BrowserName_Key];
        }
    }
}