using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace EPS.Automation
{
    /// <summary>
    /// Factory to get instance of web driver.
    /// </summary>
    internal static class WebDriverFactory
    {
        private const string APP_SETTINGS_BROWSER = "Browser";
        private const string APP_SETTINGS_TEST_ENVIRONMENT = "TestEnvironment";

        //private static bool _isChromeExecuted;
        private const bool IsInternetExplorerExecuted = false;

        private const string AUTOMATION_REMOTE = "AUTOMATION_IS_REMOTE";
        private const string AUTOMATION_REMOTE_HUB_URL = "AUTOMATION_REMOTE_HUB_URL";
        private const string AUTOMATION_REMOTE_HUB_URL1 = "AUTOMATION_REMOTE_HUB_URL1";
        private const string APP_SETTINGS_REMOTE = "isRemote";
        private const string APP_SETTINGS_REMOTE_HUB_URL = "remoteHubUrl";
        private const string APP_SETTINGS_REMOTE_HUB_URL1 = "remoteHubUrl1";

        private static bool _isRemote = false;
        private static string _remoteHubUrl;
        private static string _remoteHubUrl1;
        private static string _browserName;

        /// <summary>
        /// This is defined static variables.
        /// </summary>
        private static readonly int TimeOut = Convert.ToInt16(ConfigurationManager.AppSettings["WebDriverTimeOutInSeconds"]);

        /// <summary>
        /// Returns an instance of the web driver based on test browser.
        /// </summary>
        /// <returns>The browser driver.</returns>
        public static IWebDriver GetInstance()
        {
            IWebDriver webDriver = null;
            DriverOptions remoteCapability = null;
            if (_isRemote)
            {
                switch (_browserName)
                {
                    // get browser driver based on browserName
                    case BaseTestFixture.InternetExplorer: remoteCapability = new InternetExplorerOptions(); ; break;
                    case BaseTestFixture.FireFox: remoteCapability = new FirefoxOptions(); break;
                    case BaseTestFixture.Safari: remoteCapability = new SafariOptions(); break;
                    case BaseTestFixture.Chrome:
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("disable-popup-blocking");
                        remoteCapability = chromeOptions;
                        break;

                    case BaseTestFixture.Edge: remoteCapability = new EdgeOptions(); break;
                    default: throw new ArgumentException("The suggested browser was not found");
                }

                if (_remoteHubUrl == null)
                {
                }
                else
                {
                    // object representing the image of the page on the screen
                    webDriver = new ScreenShotRemoteWebDriver(new Uri(_remoteHubUrl), (DriverOptions)remoteCapability,
                    commandTimeout: TimeSpan.FromSeconds(TimeOut));
                }
                if (_remoteHubUrl1 == null)
                {
                }
                else
                {
                    // object representing the image of the page on the screen
                    webDriver = new ScreenShotRemoteWebDriver(new Uri(_remoteHubUrl1), (DriverOptions)remoteCapability,
                    commandTimeout: TimeSpan.FromSeconds(TimeOut));
                }
            }
            else
            {
                // Get Browser instance
                _browserName = WebDriverFactory.GetBrowser();
                switch (_browserName)
                {
                    // get browser driver based on browserName
                    case BaseTestFixture.InternetExplorer: webDriver = IeWebDriver(); break;
                    case BaseTestFixture.FireFox: webDriver = FireFoxWebDriver(); break;
                    case BaseTestFixture.Safari: webDriver = SafariWebDriver(); break;
                    case BaseTestFixture.Chrome: webDriver = ChromeWebDriver(); break;
                    case BaseTestFixture.Edge: webDriver = EdgeWebDriver(); break;
                    case "": break;
                    default: throw new ArgumentException("The suggested browser was not found");
                }
            }
            return webDriver;
        }

        /// <summary>
        /// Get Environment Variables.
        /// </summary>
        public static void Init()
        {
            Console.Out.WriteLine(string.Format("Attempting to get ENV_PEG_AUTOMATION_REMOTE"));
            bool success = Boolean.TryParse(Environment.GetEnvironmentVariable(AUTOMATION_REMOTE), out _isRemote);
            if (!success)
            {
                Console.Out.WriteLine(string.Format("Attempting to get APP_SETTINGS_REMOTE"));
                Boolean.TryParse(ConfigurationManager.AppSettings[APP_SETTINGS_REMOTE], out _isRemote);
            }
            Console.Out.WriteLine("IS_REMOTE = {0}", _isRemote);

            Console.Out.WriteLine(string.Format("Attempting to get ENV_AUTOMATION_BROWSER"));
            if (string.IsNullOrEmpty(_browserName))
            {
                Console.Out.WriteLine(string.Format("Attempting to get APP_SETTINGS_BROWSER"));
                _browserName = ConfigurationManager.AppSettings[APP_SETTINGS_BROWSER];
            }
            Console.Out.WriteLine("BROWSER = {0}", _browserName);

            if (_isRemote)
            {
                Console.Out.WriteLine(string.Format("Attempting to get ENV_PEG_AUTOMATION_REMOTE_HUB_URL"));
                _remoteHubUrl = Environment.GetEnvironmentVariable(AUTOMATION_REMOTE_HUB_URL);
                _remoteHubUrl1 = Environment.GetEnvironmentVariable(AUTOMATION_REMOTE_HUB_URL1);
                if (string.IsNullOrEmpty(_remoteHubUrl))
                {
                    Console.Out.WriteLine(string.Format("Attempting to get APP_SETTINGS_REMOTE_HUB_URL"));
                    _remoteHubUrl = ConfigurationManager.AppSettings[APP_SETTINGS_REMOTE_HUB_URL];
                    _remoteHubUrl1 = ConfigurationManager.AppSettings[APP_SETTINGS_REMOTE_HUB_URL1];
                }
                Console.Out.WriteLine("REMOTE_HUB_URL = {0}", _remoteHubUrl);
            }
        }

        /// <summary>
        /// Returns an instance of IE based driver.
        /// </summary>
        /// <returns>IE based driver.</returns>
        private static IWebDriver IeWebDriver()
        {
            KillProcess(BaseTestFixture.InternetExplorer);
            // get installed browser's version in machine
            Dictionary<string, string> browserList = GetBrowserDetails();
            // get Ie driver path based on Ie version installed in
            String getIeDriverPath = System.AppDomain.CurrentDomain.BaseDirectory;
            // start internet explorer driver service
            InternetExplorerDriverService ieservice = InternetExplorerDriverService.CreateDefaultService(getIeDriverPath);
            ieservice.LoggingLevel = InternetExplorerDriverLogLevel.Debug;
            // get path for save log execution file
            String getExecutingPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            if (getExecutingPath != null)
                ieservice.LogFile = Path.Combine(getIeDriverPath, "Log", "IEDriver" + DateTime.Now.Ticks + ".log");
            // set internet explorer options
            var options = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true,
                EnableNativeEvents = true,
                EnsureCleanSession = true,
                BrowserAttachTimeout = TimeSpan.FromMinutes(20),
                EnablePersistentHover = false
            };

            // create internet explorer driver object
            IWebDriver webDriver = new InternetExplorerDriver(ieservice, options, TimeSpan.FromMinutes(20));
            // set webDriver page load duration
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(20);
            // set cursor position center of the screen
            Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            return webDriver;
        }

        /// <summary>
        /// Returns an instance of Firefox based driver.
        /// </summary>
        /// <returns>FireFox based driver</returns>
        private static IWebDriver FireFoxWebDriver()
        {
            // Proxy setup starts here
            OpenQA.Selenium.Proxy proxy = new OpenQA.Selenium.Proxy();

            FirefoxProfile profile = new FirefoxProfile();
            profile.AssumeUntrustedCertificateIssuer = false;
            // get Log Execution Path
            String getExecutingPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            // set profile preferences
            profile.SetPreference("FireFox" + DateTime.Now.Ticks + ".log", getExecutingPath);
            profile.SetPreference("browser.helperApps.alwaysAsk.force", false);
            profile.SetPreference("browser.download.folderList", 2);
            profile.SetPreference("browser.download.dir", AutomationConfigurationManager.DownloadFilePath.Replace("file:\\", ""));
            profile.SetPreference("services.sync.prefs.sync.browser.download.manager.showWhenStarting", false);
            profile.SetPreference("browser.download.useDownloadDir", true);
            profile.SetPreference("browser.download.downloadDir", AutomationConfigurationManager.DownloadFilePath.Replace("file:\\", ""));
            profile.SetPreference("browser.download.defaultFolder", AutomationConfigurationManager.DownloadFilePath.Replace("file:\\", ""));
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/zip, application/x-zip, application/x-zip-compressed, application/download, application/octet-stream");
            profile.EnableNativeEvents = true;
            profile.SetPreference("browser.cache.disk.enable", false);
            profile.SetPreference("browser.cache.memory.enable", false);
            profile.SetPreference("browser.cache.offline.enable", false);
            profile.SetPreference("network.http.use-cache", false);
            //set proxyperference
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            service.FirefoxBinaryPath = "C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe";
            FirefoxDriver webDriver = new FirefoxDriver(service);
            // set page load duration
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TimeOut);
            // set cursor position center of the screen
            Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            return webDriver;
        }

        /// <summary>
        /// Returns an instance of Safari based driver.
        /// </summary>
        /// <returns>Safari based driver.</returns>
        private static IWebDriver SafariWebDriver()
        {
            // create safari browser object
            IWebDriver webDriver = new SafariDriver();
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TimeOut);
            return webDriver;
        }

        /// <summary>
        /// Returns an instance of Chrome based driver.
        /// </summary>
        /// <returns>Chrome based driver</returns>
        private static IWebDriver ChromeWebDriver()
        {
            // chrome options
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("disable-application-cache");
            chromeOptions.AddArguments("disk-cache-size=0");
            chromeOptions.AddArgument("disable-popup-blocking");
            chromeOptions.ToCapabilities();
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "en");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", true);
            chromeOptions.AddUserProfilePreference("download.default_directory", AutomationConfigurationManager.DownloadFilePath.Replace("file:\\", ""));
            // chrome driver path
            string chromeDriverPath = System.AppDomain.CurrentDomain.BaseDirectory;
            // create chrome browser instance
            IWebDriver webDriver = new ChromeDriver(chromeDriverPath, chromeOptions);
            // set page load duration
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TimeOut);
            return webDriver;
        }

        private static IWebDriver EdgeWebDriver()
        {
            // chrome options
            var EdgeOptions = new EdgeOptions();
            EdgeOptions.ToCapabilities();
            // edge driver path
            string EdgeDriverPath = System.AppDomain.CurrentDomain.BaseDirectory;
            // create chrome browser instance
            IWebDriver webDriver = new EdgeDriver(EdgeDriverPath, EdgeOptions, TimeSpan.FromMinutes(3));
            return webDriver;
        }

        /// <summary>
        /// Returns list of installed browsers in local machine.
        /// </summary>
        /// <returns>Browser Name and Version List.</returns>
        private static Dictionary<string, string> GetBrowserDetails()
        {
            var browserList = new Dictionary<string, string>();

            // check in current user -- chrome
            string path = Convert.ToString(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", string.Empty, null));
            if (String.IsNullOrWhiteSpace(path))
            {
                // check in local machine -- chrome
                path = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", string.Empty, null));
                if (!String.IsNullOrEmpty(path) && File.Exists(path))
                {
                    browserList.Add("Chrome", FileVersionInfo.GetVersionInfo(path.ToString()).FileVersion);
                }
            }
            else
            {
                browserList.Add("Chrome", FileVersionInfo.GetVersionInfo(path.ToString()).FileVersion);
            }

            // check in local machine -- FF
            path = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\firefox.exe", string.Empty, null));
            if (!String.IsNullOrEmpty(path) && File.Exists(path))
            {
                browserList.Add("FireFox", FileVersionInfo.GetVersionInfo(path.ToString()).FileVersion);
            }

            // check in local machine --IE
            path = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\iexplore.exe", string.Empty, null));
            if (!String.IsNullOrEmpty(path) && File.Exists(path))
            {
                browserList.Add("IE", FileVersionInfo.GetVersionInfo(path.ToString()).FileVersion);
            }

            return browserList;
        }

        /// <summary>
        /// Object for browser instance.
        /// </summary>
        /// <returns>Browser instance.</returns>
        internal static string GetBrowser()
        {
            return _browserName;
        }

        /// <summary>
        /// Returns an instance of the web driver based on test browser.
        /// </summary>
        /// <returns>The browser driver.</returns>
        public static IWebDriver GetBrowserInstance(string browser)
        {
            IWebDriver webDriver = null;
            DriverOptions remoteCapability = null;
            if (_isRemote)
            {
                switch (browser)
                {
                    // get browser driver based on browserName
                    case BaseTestFixture.InternetExplorer: remoteCapability = new InternetExplorerOptions(); ; break;
                    case BaseTestFixture.FireFox: remoteCapability = new FirefoxOptions(); break;
                    case BaseTestFixture.Safari: remoteCapability = new SafariOptions(); break;
                    case BaseTestFixture.Chrome: remoteCapability = new ChromeOptions(); break;
                    case BaseTestFixture.Edge: remoteCapability = new EdgeOptions(); break;
                    default: throw new ArgumentException("The suggested browser was not found");
                }
                // object representing the image of the page on the screen
                webDriver = new ScreenShotRemoteWebDriver(new Uri(_remoteHubUrl), remoteCapability,
                    commandTimeout: TimeSpan.FromSeconds(TimeOut));
                webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TimeOut);
            }
            else
            {
                switch (browser)
                {
                    // get browser driver based on browserName
                    case BaseTestFixture.InternetExplorer: webDriver = IeWebDriver(); break;
                    case BaseTestFixture.FireFox: webDriver = FireFoxWebDriver(); break;
                    case BaseTestFixture.Safari: webDriver = SafariWebDriver(); break;
                    case BaseTestFixture.Chrome: webDriver = ChromeWebDriver(); break;
                    case BaseTestFixture.Edge: webDriver = EdgeWebDriver(); break;
                    default: throw new ArgumentException("The suggested browser was not found");
                }
            }
            return webDriver;
        }

        /// <summary>
        /// Kill the process
        /// </summary>
        /// <param name="processToBeKilled"></param>
        public static void KillProcess(String processToBeKilled)
        {
            String ProcessHastoBeKilled = "";
            if (processToBeKilled == "Chrome")
            {
                ProcessHastoBeKilled = "chromedriver";
            }
            else if (processToBeKilled == "Internet Explorer")
            {
                ProcessHastoBeKilled = "iedriverserver";
            }
            Process[] ps = Process.GetProcessesByName(ProcessHastoBeKilled);
            foreach (Process p in ps)
            {
                p.Kill();
            }
        }
    }
}