using System;


namespace EPS.Automation.Exceptions
{

    public static class ExceptionHandler
    {
        /// <summary>
        /// Static logger instance of the class.
        /// </summary>
        private static readonly Logger ExceptionLogger =
            Logger.GetInstance(typeof(ExceptionHandler));

        /// <summary>
        /// This method handels and throws exceptions.and contnues on failure without closing browser
        /// </summary>
        /// <param name="ex">This is the exception.</param>
        public static void HandleException(Exception ex, bool ContinueOnFailure = true)
        {
            string customExceptionMessage = "";
            var genericPageException = new GenericException(ex.ToString(), ex);
            if (!string.IsNullOrWhiteSpace(customExceptionMessage))
            {
                ExceptionLogger.LogException("ExceptionHandler", "HandleException", ex, customExceptionMessage);
                new CustomReport().AddFailStatusToReport(ex, customExceptionMessage);
            }
            else
            {
                ExceptionLogger.LogException("ExceptionHandler", "HandleException", ex, ex.Message);
                new CustomReport().AddFailStatusToReport(ex, customExceptionMessage);
            }
            // close webdriver and browser instances
            if (!ContinueOnFailure)
            {
            }
            throw genericPageException;
        }
    }
}