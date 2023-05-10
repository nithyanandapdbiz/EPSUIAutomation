using OpenQA.Selenium;
using System;
using System.Diagnostics;

namespace EPS.Automation
{
    /// <summary>
    /// This is the base test script.
    /// </summary>
    public class WebDriverSingleton : IDisposable
    {
        private readonly IWebDriver _webDriver = null;

        //private readonly ISelenium _selenium = null;
        private readonly string _browser;

        /// <summary>
        /// Get browser instance.
        /// </summary>
        public string Browser
        {
            get { return _browser; }
        }

        private static WebDriverSingleton _webDriverSingleton = null;

        /// <summary>
        /// This method returns an instance of web driver singleton.
        /// </summary>
        /// <returns></returns>
        public static WebDriverSingleton GetInstance()
        {
            if (_webDriverSingleton == null)
            {
                _webDriverSingleton = new WebDriverSingleton();
            }

            return _webDriverSingleton;
        }

        /// <summary>
        /// Returns an instance of web driver
        /// </summary>
        public IWebDriver WebDriver
        {
            get
            {
                if (_webDriverSingleton == null)
                {
                    _webDriverSingleton = new WebDriverSingleton();
                }
                return _webDriver;
            }
        }

        /// <summary>
        /// Resets the singleton.
        /// </summary>
        public void ResetSingleton()
        {
            _webDriverSingleton = new WebDriverSingleton();
        }

        public void ResetNewBrowserSingleton(string browser)
        {
            _webDriverSingleton = new WebDriverSingleton(browser);
        }

        /// <summary>
        /// Creates an instance of the web driver
        /// </summary>
        protected WebDriverSingleton()
        {
            Cleanup();
            WebDriverFactory.Init();
            _browser = WebDriverFactory.GetBrowser();
            _webDriver = WebDriverFactory.GetInstance();
            if (_webDriver != null)
            {
                _webDriver.Manage().Cookies.DeleteAllCookies();
                _webDriver.Manage().Window.Maximize();
            }
        }

        protected WebDriverSingleton(string browser)
        {
            WebDriverFactory.Init();
            _webDriver = WebDriverFactory.GetBrowserInstance(browser);
            _webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Cleans up the web driver and related processess.
        /// </summary>
        public void Cleanup()
        {
            try
            {
                if (_webDriver != null)
                    _webDriver.Quit();
                _webDriver.Close();
                Process[] processesToKill = BrowserProcessFactory.GetProcessInstance();
                foreach (Process processToKill in processesToKill)
                {
                    try
                    {
                        // kill and clean all processess
                        processToKill.Kill();
                    }
                    catch
                    {
                        //This exception is being consumed so that if a process is not killed the testing will not stop
                    }
                }
            }
            catch
            {
                //This exception is being consumed so that if a process is not killed the testing will not stop
            }
        }

        /// <summary>
        /// Disposes the drivers
        /// </summary>
        public void Dispose()
        {
            Cleanup();
        }
    }
}