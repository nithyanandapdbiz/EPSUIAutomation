using EPS.Automation.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace EPS.Automation
{
    /// <summary>
    /// All tests derive from this class.
    /// </summary>
    [Binding]
    public abstract class BaseTestFixture : BaseTestScript 
    {
        /// <summary>
        /// These are the browser constants.
        /// </summary>
        public const string Chrome = "Chrome";

        public const string InternetExplorer = "Internet Explorer";
        public const string Safari = "Safari";
        public const string FireFox = "FireFox";
        public const string Edge = "Edge";
        private Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// This is the Broswer variable called from AppSettings.
        /// </summary>
        private static readonly string BrowserName = ConfigurationManager.AppSettings["Browser"];

        protected bool IsTakeScreenShotDuringEntryExit = Convert.ToBoolean
            (ConfigurationManager.AppSettings["TakeScreenShotDuringEntryExit"]);

        private readonly int _waitTimeOut = Convert.ToInt32(
            ConfigurationManager.AppSettings["ElementFindTimeOutInSeconds"]);

        /// <summary>
        /// Returns a new instance of web driver wait.
        /// </summary>
        /// <param name="timeout">This is the time to wait for the condition fullfill.</param>
        private WebDriverWait CreateWebDriverWait(int timeout = -1)
        {
            if (timeout == -1)
            {
                timeout = this._waitTimeOut;
            }
            return new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits for finding the element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="timeOut">This is the time to wait for HTML element to appear on the page.</param>
        /// <exception cref="ElementNotVisibleException">If the element not able to visible on the page.</exception>
        /// <exception cref="WebDriverTimeoutException">If the element not able to find in the specified time.</exception>
        /// <returns>True if the element is visible otherwise throws the exception.</returns>
        protected void WaitForElement(By by, int timeOut = -1)
        {
            bool flag1 = false;
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<IWebElement>((d) =>
                {
                    IWebElement element = WebDriver.FindElement(by);
                    if (element.Displayed && element.Enabled)
                    {
                        return element;
                    }
                    return null;
                }
                    );
                this.IsElementDisplayedInPage(by, flag1);
            }
            //Exception Handling
            catch (Exception)
            {
                throw new GenericException(String.Format
                    ("Element " + "{0}" + " does not exist on the page ", by));
            }
        }

        /// <summary>
        /// Waits for page switched to another page successfully.
        /// </summary>
        /// <param name="expectedPageTitle"> The expected title, which must be an exact match.</param>
        /// <param name="timeOut">This is the time to wait for HTML element to appear on the page.</param>
        /// <exception cref="WebDriverTimeoutException">If the element not able to find in the specified time.</exception>
        /// <see cref="TitleIs">An expectation for checking the title of a page.</see>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected void WaitUntilPageGetSwitchedSuccessfully
            (String expectedPageTitle, int timeOut = -1)
        {
            //Wait Till Page Switched Successfully
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
                wait.Until(ExpectedConditions.TitleIs(expectedPageTitle));
                WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeOut);
            }
            //Exception Handling
            catch (Exception)
            {
                throw new GenericException("User " + UserName + "action performed to wait until page get switched successfully for window " + expectedPageTitle + " has failed");
            }
        }

        /// <summary>
        /// Is successfull message appears or not on the page.
        /// </summary>
        /// <param name="message">This is the content of the successfull message.</param>
        /// <param name="elementClassName">This is the successfull message class name attribute.</param>
        /// <param name="timeout">This is the time to wait for successfull message appear's on the page..</param>
        /// <returns>True if message is found on the page otherwise false.</returns>
        /// <exception cref="ElementNotVisibleException">If the element not able to visible on the page.</exception>
        /// <exception cref="WebDriverTimeoutException">If the element not able to find in the specified time.</exception>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected bool IsMessageExists(String message,
            String elementClassName, int timeout = -1)
        {
            //Verify Is Message Exists
            WebDriverWait webDriverWait = CreateWebDriverWait(timeout);
            try
            {
                SelectDefaultWindow();
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(elementClassName)));
                string getSuccessMessage = null;
                switch (BrowserName)
                {
                    case BaseTestFixture.FireFox:
                    case BaseTestFixture.Chrome:
                        //Get Sucess Message Text From UI
                        getSuccessMessage = WebDriver.FindElement(By.ClassName(
                            elementClassName)).Text;
                        break;

                    case BaseTestFixture.InternetExplorer:
                        //Get Sucess Message Text From UI
                        getSuccessMessage = WebDriver.FindElement(By.
                            ClassName(elementClassName)).GetAttribute("innerText");
                        break;
                }
                return getSuccessMessage != null && (
                    getSuccessMessage.Contains(message));
            }
            //Exception Handling
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Is successfull message appears or not on the pop up page.
        /// </summary>
        /// <param name="message">This is the content of the successfull message.</param>
        /// <param name="elementClassName">This is the successfull message class name attribute.</param>
        /// <param name="timeout">This is the time to wait for successfull message appear's on the page.</param>
        /// <returns>True if message is found on the page otherwise false.</returns>
        /// <exception cref="ElementNotVisibleException">If the element not able to visible on the page.</exception>
        /// <exception cref="WebDriverTimeoutException">If the element not able to find in the specified time.</exception>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected bool IsMessagePresentOnPopUp(String message,
            String elementClassName, int timeout = -1)
        {
            //Verify Is Message Exists in Pop Up
            WebDriverWait webDriverWait = CreateWebDriverWait(timeout);
            try
            {
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(
                    By.ClassName(elementClassName)));
                string getSuccessMessage = null;
                switch (BrowserName)
                {
                    case BaseTestFixture.FireFox:
                    case BaseTestFixture.Chrome:
                        //Get Sucess Message Text From UI
                        getSuccessMessage = WebDriver.FindElement(By.
                            ClassName(elementClassName)).Text;
                        break;

                    case BaseTestFixture.InternetExplorer:
                        //Get Sucess Message Text From UI
                        getSuccessMessage = WebDriver.FindElement(By.
                            ClassName(elementClassName)).GetAttribute("innerText");
                        break;
                }
                return getSuccessMessage != null && (
                     getSuccessMessage.Contains(message));
            }
            //Exception Handling
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Is element present on the page or not.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="timeout">This is the time to wait for element appear's on the page.</param>
        /// <returns>True if the element is present on the page otherwise false.</returns>
        /// <exception cref="WebDriverTimeoutException">If the element not able to find in the specified time.</exception>
        /// <exception cref="ElementNotVisibleException">If the element not able to visible on the page.</exception>
        /// <exception cref="InvalidOperationException">When a method call is invalid for the object's current state.</exception>
        /// <exception cref="SystemException">Common language runtime when errors occur
        /// that are nonfatal and recoverable by user programs.</exception>
        protected bool IsElementPresent(By by, int timeout = -1)
        {
            //Verify Element Present on Page
            try
            {
                //Check for the element existence. If element is present then return true else wait for the timeout time.
                //When wait time if over then an exception is thrown from the webDriver. Catch only
                //this specific exception and from there return false.
                //Need to catch this exception so that outer code does not worry about this exception
                //and existing logic which relies on TRUE/FALSE can work as it is.
                WebDriverWait webDriverWait = CreateWebDriverWait(timeout);
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(by));
                return true;
            }
            //Exception Handling
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Is pop up closed or not.
        /// </summary>
        /// <param name="numberOfWindows">This is the count of windows opened at that particular time.</param>
        /// <param name="timeOut">This is the time to wait for pop up get close.</param>
        /// <returns>True if popup is closed successfully otherwise false.</returns>
        /// <exception cref="TimeoutException">If the pop up not able to find in the specified time.</exception>
        protected bool IsPopUpClosed(
            int numberOfWindows, int timeOut = -1)
        {
            //Is Pop Up Close
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            int waitTimeConsumed = 0;
            try
            {
                if (timeOut > 0)
                {
                    while (WebDriver.WindowHandles.Count >= numberOfWindows
                        && waitTimeConsumed < timeOut)
                    {
                        Thread.Sleep(5000);
                        waitTimeConsumed += 5;
                    }
                    if (waitTimeConsumed < timeOut)
                    {
                        WebDriver.SwitchTo().Window(WebDriver.WindowHandles.First());
                        return true;
                    }
                }
            }
            //Exception Handling
            catch (Exception)
            {
                throw new GenericException("User " + UserName + " action performed to close the popup has failed, as the popup does not exist.");
            }
            return false;
        }

        /// <summary>
        /// Is window exists on the page or not.
        /// </summary>
        /// <param name="windowName">This is the name of the window.</param>
        /// <param name="timeout">This is the time to wait to find window exists on the page.</param>
        /// <returns>True if window exists otherwise throws exception.</returns>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        /// <exception cref="WebDriverTimeoutException">if the window not find in the specified interval of time.</exception>
        protected bool IsWindowsExists(
            String windowName, int timeout = -1)
        {
            //Verify Window Exists
            try
            {
                WebDriverWait webDriverWait = CreateWebDriverWait(timeout);
                webDriverWait.Until(ExpectedConditions.TitleContains(windowName));
                return true;
            }
            //Exception Handling
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Is pop up present on the page or not.
        /// </summary>
        /// <param name="popupName">This is the name of the pop up.</param>
        /// <param name="timeOut">This is the time to wait for window get open.</param>
        /// <returns>True if pop up present on the page otherwise throws exception.</returns>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected bool IsPopupPresent(
            String popupName, int timeOut = -1)
        {
            //Verify Existance of Popup window
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            try
            {
                IWebDriver popup = null;
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    //Get all opend window
                    ReadOnlyCollection<String> windowIterator = WebDriver.WindowHandles;
                    foreach (var windowHandle in windowIterator)
                    {
                        popup = WebDriver.SwitchTo().Window(windowHandle);
                        if (popup.Title.Equals(popupName))
                        {
                            //Stop stopwatch when popup found true
                            stopWatch.Stop();
                            return true;
                        }
                    }
                }
            }
            //Exception Handling
            catch (Exception)
            {
                return false;
            }
            stopWatch.Stop();
            return false;
        }

        /// <summary>
        /// Is Window Opened In the Specified Interval of Time or not.
        /// </summary>
        /// <param name="windowName">This is the name of the window.</param>
        /// <param name="waitTimeOutDuration">This is the time to wait for window get open.</param>
        /// <exception cref="TimeoutException">If the window not able to find in the specified time.</exception>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected void WaitUntilWindowLoads(
            String windowName, int waitTimeOutDuration = -1)
        {
            //Wait Until Window Loads
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (waitTimeOutDuration == -1)
            {
                waitTimeOutDuration = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < waitTimeOutDuration)
                {
                    if (WebDriver.WindowHandles.Any(item => WebDriver.SwitchTo().
                        Window(item).Title.Equals(windowName)))
                    {
                        break;
                    }
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                throw new GenericException(String.Format
                    ("Window not found = {0}", windowName));
            }
            stopWatch.Stop();
        }

        /// <summary>
        /// Wait for pop up window till it gets load on the page.
        /// </summary>
        /// <param name="popUpWindowName">The name of the pop up.</param>
        /// <param name="timeOut">This is time to wait.</param>
        /// <exception cref="TimeoutException">if the window not loads in the specified interval of time.</exception>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected void WaitUntilPopUpLoads(
            String popUpWindowName, int timeOut = -1)
        {
            //Wait Till Window Loads
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            while (stopWatch.Elapsed.TotalSeconds < timeOut)
            {
                try
                {
                    //Wait for window
                    WaitUntilWindowLoads(popUpWindowName);
                    break;
                }
                //Exception Handling
                catch (Exception)
                {
                    throw new GenericException("User " + UserName + " action to load the popup " + popUpWindowName + "has failed");
                }
            }
        }

        /// <summary>
        /// Select Currently Last Opened Window.
        /// </summary>
        /// <param name="windowName">This is the name of the window.</param>
        /// <exception cref="NoSuchWindowException">If the specified window not exists on the page.</exception>
        protected void SelectWindow(String windowName)
        {
            //Select Already Present Window
            try
            {
                foreach (string item in WebDriver.WindowHandles)
                {
                    if (WebDriver.SwitchTo().Window(item).Title.Equals(windowName))
                    {
                        WebDriver.SwitchTo().Window(item).Title.
                        ToString(CultureInfo.InvariantCulture);
                        break;
                    }
                }
            }
            //Exception Handling
            catch (Exception)
            {
                throw new GenericException("User " + UserName + " action to select window " + windowName + "has failed");
            }
        }

        /// <summary>
        /// Upload any type of file from the local system path.
        /// </summary>
        /// <param name="filePath">This is the full local file path where file is exists.</param>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="SendKeys">This method to simulate typing into an element,
        /// which may set its value.</see>
        /// <exception cref="NoSuchElementException">If element is not visible on current page.</exception>
        protected void UploadFile(String filePath, By by)
        {
            //Upload File
            try
            {
                WebDriver.FindElement(by).SendKeys(filePath);
            }
            //Exception Handling
            catch (Exception)
            {
                throw new GenericException("User " + UserName + " action to upload file from " + filePath + "has failed");
            }
        }

        /// <summary>
        /// Is Window Opened and the specified element present on the window or not.
        /// </summary>
        /// <param name="windowName">This is the name of the window.</param>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="timeOut">TThis is the time to wait for window get open and element found.</param>
        /// <returns>True if the window and element are present otherwise false.</returns>
        /// <exception cref="NoSuchWindowException">If the specified window not exists on the page.</exception>
        /// <exception cref="WebDriverTimeoutException">if the window not find in the specified interval of time.</exception>
        /// <exception cref="ElementNotVisibleException">If the element not able to visible on the window.</exception>
        protected bool IsElementLoadedInWindow(
            String windowName, By by, int timeOut = -1)
        {
            //Verify Element Present In window
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    if (WebDriver.WindowHandles.Any(item => WebDriver.SwitchTo().Window(item).Title == windowName))
                    {
                        SelectWindow(windowName);
                        WebDriverWait setwait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
                        setwait.Until((element) => { return element.FindElement(by).Displayed; });
                        return true;
                    }
                    return false;
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                throw new GenericException("Action performed by user " + UserName + "failed to load element in window " + windowName + "");
            }
            stopWatch.Stop();
            return false;
        }

        /// <summary>
        /// Is this element displayed or not? This method avoids the problem of having to parse an element's "style" attribute.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="holdTime">This is the time to wait for HTML element to display on the page.</param>
        /// <returns>Whether or not the element is displayed.</returns>
        protected void WaitForElementDisplayedInUi(By by, int holdTime = -1)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (holdTime == -1)
            {
                holdTime = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < holdTime)
                {
                    try
                    {
                        bool isObjectDisplayed = WebDriver.FindElement(by).Displayed;
                        if (isObjectDisplayed.Equals(true))
                        {
                            break;
                        }
                    }
                    catch (WebDriverException)
                    {
                        // not thorwing exception due to repeat the loop until element displayed in Ui.
                    }
                }
            }
            catch (Exception)
            {
                throw new GenericException("Action performed by user " + UserName + "to wait for element " + by + " displayed in Ui has failed");
            }
        }

        /// <summary>
        /// Check whether element is displayed in web page or not.
        /// </summary>
        /// <param name="by">Element name.</param>
        /// <param name="timeOut">Time out value.</param>
        /// <returns>Status of the element display.</returns>
        public bool IsElementDisplayedInPage(By by, bool flag, int timeOut = -1)
        {
            bool isElementDisplayedInPage = false;

            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            if (!flag)
            {
                stopWatch.Start();
            }

            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    isElementDisplayedInPage = WebDriver.FindElement(by).Displayed;
                    if (isElementDisplayedInPage)
                    {
                        isElementDisplayedInPage = true;
                        break;
                    }
                }
            }
            //Exception Handling
            catch (NoSuchElementException)
            {
                bool flag2 = true;
                this.IsElementDisplayedInPage(by, flag2);
            }
            stopWatch.Stop();
            return isElementDisplayedInPage;
        }

        /// <summary>
        /// Selects the parent window or the first window opened.
        /// </summary>
        /// <exception cref="KeyNotFoundException">If the configuration key is not defined or incorrectly defined.</exception>
        /// <exception cref="NoSuchWindowException">If the specified window not exists on the page.</exception>
        protected void SelectDefaultWindow()
        {
            //Select Default Window
            try
            {
                Thread.Sleep(5000);
                WebDriver.SwitchTo().Window(WebDriver.WindowHandles.First());
            }
            //Exception Handling
            catch (Exception)
            {
                throw new GenericException("Action performed by user " + UserName + " failed to load default window");
            }
        }

        /// <summary>
        /// Is file uploading in progress the wait to get file upload successfully.
        /// </summary>
        /// <param name="frameName">This is the name of the frame attribute.</param>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="timeOut">This is the time to wait for file get uploaded successfully.</param>
        /// <exception cref="TimeoutException">If the file not uploded under the specified interval of time.</exception>
        /// <exception cref="NoSuchFrameException">If the frame not present on the page.</exception>
        /// <exception cref="NoSuchWindowException">If the file upload window not present on the page.</exception>
        protected void WaitUntilFileUploadInProgress(
            String frameName, By by, int timeOut = -1)
        {
            //Wait Till File Upload In Progress
            bool isFileStillUploading = false;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    //Wait For Frame
                    WaitForElement(By.Id(frameName));
                    WebDriver.SwitchTo().Frame(frameName);
                    if (!IsElementPresent((by), 20))
                    {
                        isFileStillUploading = true;
                        break;
                    }
                    //Switch To Parent Window
                    WebDriver.SwitchTo().DefaultContent();
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                throw new GenericException("File Still Loading After Specified Time.");
            }
            stopWatch.Stop();
            if (!isFileStillUploading)
            {
                throw new Exception("File Still Loading After Specified Time.");
            }
        }

        /// <summary>
        /// Wait until Thinking Indicator is loading on the page.
        /// </summary>
        /// <returns>True if the thinking indicator element is currently present
        /// after the specified time interval, false otherwise.</returns>
        /// <exception cref="TimeoutException">if the window not loads in the specified interval of time.</exception>
        /// <exception cref="NoSuchElementException">If the element not present on the page.</exception>
        protected bool IsThinkingIndicatorLoading(int timeOut = -1)
        {
            //If element not present then webdriver throw the exception,
            //catch this exception so that outer code does not worry
            //about this exception and existing logic which relies on TRUE/FALSE can work as it is except TimeoutException.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            bool isThinkingIndicatorProcessing = false;
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    isThinkingIndicatorProcessing = WebDriver.FindElement(By.XPath
                        ("//img[contains(@src,'/App_Static/IMG/ThinkingIndicator/process.gif')]")).Displayed;
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                return false;
            }
            stopWatch.Stop();
            return isThinkingIndicatorProcessing;
        }

        /// <summary>        /// Wait until Thinking Indicator is loading on the page.
        /// </summary>
        /// <returns>True if the thinking indicator element is currently present
        /// after the specified time interval, false otherwise.</returns>
        /// <exception cref="TimeoutException">if the window not loads in the specified interval of time.</exception>
        /// <exception cref="NoSuchElementException">If the element not present on the page.</exception>
        protected bool IsSSRSThinkingIndicatorLoading(int timeOut = -1)
        {
            //If element not present then webdriver throw the exception,
            //catch this exception so that outer code does not worry
            //about this exception and existing logic which relies on TRUE/FALSE can work as it is except TimeoutException.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            bool isThinkingIndicatorProcessing = false;
            string thinkingIndicator = "//img[contains(@src,'/Reports/Reserved.ReportViewerWebControl.axd?OpType=Resource&Version=10.50.4266.0&Name=Microsoft.Reporting.WebForms.Icons.SpinningWheel.gif";
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    isThinkingIndicatorProcessing = WebDriver.FindElement(By.XPath(thinkingIndicator)).Enabled;
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                return false;
            }
            stopWatch.Stop();
            return isThinkingIndicatorProcessing;
        }

        /// <summary>
        /// Generates the alphanumeric random code.
        /// </summary>
        /// <param name="characterSet">This is the set of the characters from which random code needs to generate.</param>
        /// <param name="length">The length of the string
        ///  needed to generate random code.</param>
        /// <returns>The alphanumeric random code.</returns>
        protected string GetRandomNumber(
            String characterSet, int length)
        {
            Random random = new Random();
            //The below code will select the random characters from the set
            //and then the array of these characters are passed to string
            //constructor to makes an (alpha) numeric string depends on the user need.
            String getRandomCode = new string(Enumerable.Repeat(characterSet, length)
                    .Select(set => set[random.Next(set.Length)]).ToArray());
            return getRandomCode;
        }

        /// <summary>
        /// Compares the sub string present in the whole string.
        /// </summary>
        /// <param name="sourceString">This is whole string to match.</param>
        /// <param name="compareString">This the sub string to compare in whole string.</param>
        /// <returns>True if sub string present in whole string; else false.</returns>
        protected bool IsStringContainsTheSubString(string sourceString, string compareString)
        {
            int results = sourceString.IndexOf(compareString, StringComparison.CurrentCultureIgnoreCase);
            return results == -1 ? false : true;
        }

        /// <summary>
        /// Wait until PCT Thinking Indicator is loading on the page.
        /// </summary>
        /// <returns>True if the thinking indicator element is currently present
        /// after the specified time interval, false otherwise.</returns>
        /// <exception cref="TimeoutException">if the window not loads in the specified interval of time.</exception>
        /// <exception cref="NoSuchElementException">If the element not present on the page.</exception>
        protected bool IsPCTThinkingIndicatorLoading(int timeOut = -1)
        {
            //If element not present then webdriver throw the exception,
            //catch this exception so that outer code does not worry
            //about this exception and existing logic which relies on TRUE/FALSE can work as it is except TimeoutException.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            bool isThinkingIndicatorProcessing = false;
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    isThinkingIndicatorProcessing = WebDriver.FindElement(By.ClassName
                        ("DocInProcess")).Displayed;
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                return false;
            }
            stopWatch.Stop();
            return isThinkingIndicatorProcessing;
        }

        /// <summary>
        /// Wait until PCT Thinking Indicator is loading on the page.
        /// </summary>
        /// <returns>True if the thinking indicator element is currently present
        /// after the specified time interval, false otherwise.</returns>
        /// <exception cref="TimeoutException">if the window not loads in the specified interval of time.</exception>
        /// <exception cref="NoSuchElementException">If the element not present on the page.</exception>
        protected bool IsPCTFileUploadThinkingIndicatorLoading(int timeOut = -1)
        {
            //If element not present then webdriver throw the exception,
            //catch this exception so that outer code does not worry
            //about this exception and existing logic which relies on TRUE/FALSE can work as it is except TimeoutException.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            bool isThinkingIndicatorProcessing = false;
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    isThinkingIndicatorProcessing = WebDriver.FindElement(By.Id
                        ("FileUploadingImage")).Displayed;
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                return false;
            }
            stopWatch.Stop();
            return isThinkingIndicatorProcessing;
        }

        /// <summary>
        /// Is Window Opened In the Specified Interval of Time or not.
        /// </summary>
        /// <param name="windowName">This is the name of the window.</param>
        /// <param name="waitTimeOutDuration">This is the time to wait for window get open.</param>
        /// <exception cref="TimeoutException">If the window not able to find in the specified time.</exception>
        /// <exception cref="NoSuchWindowException">If the window not exists on the page.</exception>
        protected void WaitUntilWindowLoadsPartialTitle(
            String windowName, int waitTimeOutDuration = -1)
        {
            //Wait Until Window Loads
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (waitTimeOutDuration == -1)
            {
                waitTimeOutDuration = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < waitTimeOutDuration)
                {
                    if (WebDriver.WindowHandles.Any(item => WebDriver.SwitchTo().
                        Window(item).Title.Contains(windowName)))
                    {
                        break;
                    }
                }
            }
            //Exception Handling
            catch (Exception)
            {
                stopWatch.Stop();
                throw new GenericException("User " + UserName + "action to wait until Window " + windowName + "Loads with PartialTitle");
            }
            stopWatch.Stop();
        }
    }
}
