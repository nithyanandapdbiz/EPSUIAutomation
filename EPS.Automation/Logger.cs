using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace EPS.Automation
{
    public class Logger
    {
        private readonly ILog _log4NetLogger;
        public static String screenShotFilePath;
        private static ICollection _configLogs = log4net.Config.XmlConfigurator.Configure();

        //Holds the date and time when the application launches first time.
        private static string _applicationStartDateTime = "";

        /// <summary>
        /// Returns an instance of logger.
        /// </summary>
        /// <returns>An instance of logger.</returns>
        public static Logger GetInstance(Type T)
        {
            if (string.IsNullOrWhiteSpace(_applicationStartDateTime))
            {
                _applicationStartDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                CreateTestResultFolders();
            }

            var logger = new Logger(T);
            return logger;
        }

        /// <summary>
        /// Logger.
        /// This method configures the path for log file to be saved
        /// </summary>
        private Logger(Type T)
        {
            _log4NetLogger = LogManager.GetLogger("MMSG.Automation");
            var LogFilePath = Path.Combine(new string[] {
                                                   Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).
                                                   GetDirectories("TestResults")[0].FullName,
                                                   ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.Product_Key].ToUpper()+
                                                   AutomationConfigurationManagerResource.TextSeparator+
                                                   ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.AppServerName_Key].ToUpper()+
                                                   AutomationConfigurationManagerResource.TextSeparator
                                                   + _applicationStartDateTime,
                                                   AutomationConfigurationManagerResource.Log_Path,
                                                   AutomationConfigurationManagerResource.LogFile_Name });
            GlobalContext.Properties["LogFileName"] = LogFilePath;
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method creates a folder inside TestResults folder of the project
        /// with project name and datetime stamp to hold reports, log and screenshots
        /// in respective subfolders
        /// </summary>
        public static void CreateTestResultFolders()
        {
            try
            {
                var testResultsDirectoryPath = Path.Combine(new string[] {
                                                   Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).
                                                   GetDirectories("TestResults")[0].FullName,
                                                   ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.Product_Key].ToUpper()+
                                                   AutomationConfigurationManagerResource.TextSeparator+
                                                   ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.AppServerName_Key].ToUpper()+
                                                   AutomationConfigurationManagerResource.TextSeparator
                                                   + _applicationStartDateTime });
                var reportsPath = Path.Combine(new string[] { testResultsDirectoryPath, AutomationConfigurationManagerResource.Reports_Path });
                var logPath = Path.Combine(new string[] { testResultsDirectoryPath, AutomationConfigurationManagerResource.Log_Path });
                var screenShotsPath = Path.Combine(new string[] { testResultsDirectoryPath, AutomationConfigurationManagerResource.ScreenShots_Path });

                Directory.CreateDirectory(testResultsDirectoryPath);
                if (Directory.Exists(testResultsDirectoryPath))
                {
                    Directory.CreateDirectory(reportsPath);
                    Directory.CreateDirectory(logPath);
                    Directory.CreateDirectory(screenShotsPath);
                }
            }
            catch (Exception exp)
            {
                Logger.GetInstance(typeof(Logger)).LogException("Logger", "CreateTestResultFolders", exp, false);
            }
        }

        /// <summary>
        /// This method returns the date time stamp of application launch
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationStartDateTime()
        {
            return _applicationStartDateTime;
        }

        /// <summary>
        /// This method takes a screenshot and puts it in the directory.
        /// </summary>
        /// <param name="fileName">This is the name of the file.</param>
        private void TakeScreenShot(string fileName)
        {
            try
            {
                Screenshot screenShot = ((ITakesScreenshot)WebDriverSingleton.GetInstance().WebDriver).GetScreenshot();
                string executingPath = Path.Combine(new string[] {
                                                   Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).
                                                   GetDirectories("TestResults")[0].FullName,
                                                   ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.Product_Key].ToUpper()+
                                                   AutomationConfigurationManagerResource.TextSeparator+
                                                   ConfigurationManager.AppSettings[AutomationConfigurationManagerResource.AppServerName_Key].ToUpper()+
                                                   AutomationConfigurationManagerResource.TextSeparator
                                                   + _applicationStartDateTime, AutomationConfigurationManagerResource.ScreenShots_Path });
                // Get Screenshot Path
                screenShotFilePath = Path.Combine(new string[] { executingPath, fileName + ".png" });
                screenShot.SaveAsFile(Path.Combine(screenShotFilePath));
            }
            catch (Exception e)
            {
                _log4NetLogger.Fatal("Unable to take screenshot " + e);
            }
        }

        /// <summary>
        /// This method logs a message.
        /// </summary>
        /// <param name="message">This is the message.</param>
        private void Log(string message)
        {
            string messageToLog = string.Format(" UserName = {0} ~UserType=  {1}  ~ TransactionTimings = {2} ~ BrowserName = {3}"
                , BaseTestScript.UserName
                , BaseTestScript.UserType
                , BaseTestScript.TransactionTimings
                , BaseTestScript.CurrentBrowserName
                );
            //Log at info level the message
            _log4NetLogger.Debug(messageToLog + " ~ " + message);
        }

        /// <summary>
        /// This method logs a message.
        /// </summary>
        /// <param name="className">The name of the class.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="message">The message.</param>
        /// <param name="isTakeScreenShot">If a screen shot should be taken.</param>
        public void LogMessage(string message, bool isTakeScreenShot = false)
        {
            const string logMessageTemplate = "~Message = {0} ~ ScreenShot File = {1}";
            // Date time stamp to generate the screen shot file name
            String screenShotFileName = DateTime.Now.Year
                + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture)
                + DateTime.Now.Day
                + DateTime.Now.Hour
                + DateTime.Now.Minute
                + DateTime.Now.Second;

            String logMessage = String.Format(logMessageTemplate, message, isTakeScreenShot ? screenShotFileName : "Not Applicable");

            Log(logMessage);

            if (isTakeScreenShot) TakeScreenShot(screenShotFileName);
        }

        /// <summary>
        /// This logs a metho entry.
        ///
        /// </summary>
        /// <param name="className">This is the name of the class.</param>
        /// <param name="methodName">This is the name of the method.</param>
        /// <param name="isTakeScreenShot">This tells if a screen shot should be taken default to false.</param>
        public void LogMethodEntry(bool isTakeScreenShot = false)
        {
            LogMessage("Entry", isTakeScreenShot);
        }

        /// <summary>
        /// This logs a metho exit
        /// </summary>
        /// <param name="className">This is the name of the class</param>
        /// <param name="methodName">This is the name of the method</param>
        /// <param name="isTakeScreenShot">This tells if a screen shot should be taken default to false</param>
        public void LogMethodExit(bool isTakeScreenShot = false)
        {
            LogMessage("Exit", isTakeScreenShot);
        }

        /// <summary>
        /// This logs an exception.
        /// </summary>
        /// <param name="className">This is the name of the class.</param>
        /// <param name="methodName">This is the name of the method.</param>
        /// <param name="exception">This is the exception to be logged.</param>
        /// <param name="isTakeScreenShot">This tells if a screen shot should be taken default to false.</param>
        public void LogException(string className, string methodName, Exception exception, bool isTakeScreenShot = false)
        {
            LogMessage("~ Exception Message = " + exception.Message + " ~ Inner Exception = " + exception.InnerException + " ~ Stack Trace =" + exception.StackTrace, isTakeScreenShot);
        }

        /// <summary>
        /// This logs an exception
        /// </summary>
        /// <param name="className">This is the name of the class</param>
        /// <param name="methodName">This is the name of the method</param>
        /// <param name="exception">This is the exception to be logged</param>
        /// <param name="message"> This is the user Message</param>
        /// <param name="isTakeScreenShot">This tells if a screen shot should be taken default to false</param>
        public void LogException(string className, string methodName, Exception exception, string message, bool isTakeScreenShot = false)
        {
            LogMessage("~ Message = " + message + "~ Exception Message = " + exception.Message + " ~ Inner Exception = " + exception.InnerException + " ~ Stack Trace =" + exception.StackTrace, isTakeScreenShot);
        }

        /// <summary>
        /// This method asserts an expression and logs result.
        /// </summary>
        /// <param name="testCaseName">The name of test case.</param>
        /// <param name="scenarioName">The name of the scenario.</param>
        /// <param name="message">Any user message</param>
        /// <param name="assertExpression">This is the assert expression
        /// This expression is expected to be of format ()=> Assert.Fail(),Assert.AreEqual()...
        /// The method is executed and based on result the logging is done.
        /// </param>
        /// <param name="isTakeScreenShotOnPass">This tells if a screen shot should be taken
        /// if execution passes.</param>
        private void LogAssertion(string testCaseName, string scenarioName, string message, Action assertExpression, bool isTakeScreenShotOnPass = false,
             bool ContinueOnFailure = true)
        {
            const string logMessageTemplate = "~TestCaseName  = {0} ~ ScenarioName ={1} ~ Result = {2} ~ Message= {3} ~ ScreenShot File = {4}";
            //  string logMessage = null;

            // Date time stamp to convert the Screen shot file name
            String screenShotFileName = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture)
                + DateTime.Now.Month
                + DateTime.Now.Day
                + DateTime.Now.Hour
                + DateTime.Now.Minute
                + DateTime.Now.Second;
            try
            {
                assertExpression.Invoke();
                message = String.Format(logMessageTemplate, testCaseName, scenarioName, "Assert Passed",
                    message, isTakeScreenShotOnPass ? screenShotFileName : "Not Applicable");
                Log(message);
                if (isTakeScreenShotOnPass)
                {
                    TakeScreenShot(screenShotFileName);
                    new CustomReport().AddStatusToReport(CustomReport.ReportStatus.AttachScreenshot, screenShotFilePath);
                }
            }
            catch (AssertFailedException afe)
            {
                message = String.Format(logMessageTemplate, testCaseName, scenarioName, "~Assert Failed~", " | UserMessage = " + message + "~ Reason = " + afe, screenShotFileName);
                TakeScreenShot(screenShotFileName);
                Log(message);

                //write Fail status to report
                var messageToLog = afe.Message.ToString();
                //dispaly '>' in html file
                if (messageToLog.Contains(">"))
                {
                    messageToLog = messageToLog.Replace(">", "&gt;");
                }
                if (messageToLog.Contains("<"))
                {
                    messageToLog = messageToLog.Replace("<", "&lt;");
                }
                new CustomReport().AddStatusToReport(CustomReport.ReportStatus.Fail, messageToLog);
                new CustomReport().AddStatusToReport(CustomReport.ReportStatus.AttachScreenshot, screenShotFilePath);
                // Close webdriver and browser instances if Assert Fails
                // Close webdriver and browser instances if Assert Fails
                if (!ContinueOnFailure)
                {
                    WebDriverSingleton.GetInstance().Dispose();
                    throw;
                }
                throw;
            }
        }

        /// <summary>
        /// This method asserts an expression and logs result.
        /// </summary>
        /// <param name="testCaseName">The name of test case.</param>
        /// <param name="scenarioName">The name of the scenario.</param>
        /// <param name="assertExpression">This is the assert expression
        /// This expression is expected to be of format ()=> Assert.Fail(),Assert.AreEqual()...
        /// The method is executed and based on result the logging is done.
        /// </param>
        /// <param name="isTakeScreenShotOnPass">This tells if a screen shot should be taken
        /// if execution passe.s</param>
        public void LogAssertion(string testCaseName, string scenarioName, Action assertExpression, bool isTakeScreenShotOnPass = false)
        {
            LogAssertion(testCaseName, scenarioName, "", assertExpression, isTakeScreenShotOnPass);
        }

        /// <summary>
        /// This method asserts an expression and logs result.
        /// </summary>
        /// <param name="testCaseName">The name of test case.</param>
        /// <param name="scenarioName">The name of the scenario.</param>
        /// <param name="message">Any user message.</param>
        /// <param name="exception">This is th exception being used.</param>
        /// <param name="assertExpression">This is the assert expression
        /// This expression is expected to be of format ()=> Assert.Fail(),Assert.AreEqual()...
        /// The method is executed and based on result the logging is done.
        /// </param>
        public void LogAssertion(string testCaseName, string scenarioName, string message, Exception exception, Action assertExpression)
        {
            LogAssertion(testCaseName, scenarioName, "~Message = " + message + "~ Exception = " + exception + " ~ StackTrace = " + exception.StackTrace
                , assertExpression, true);
        }
    }
}