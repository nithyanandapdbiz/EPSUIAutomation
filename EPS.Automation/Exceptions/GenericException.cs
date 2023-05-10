using System;


namespace EPS.Automation.Exceptions
{

    public class GenericException : BaseAutomationException
    {
        public override Logger Logger
        {
            get
            {
                return ExceptionLogger;
            }
        }

        /// <summary>
        /// Static logger instance of the class.
        /// </summary>
        private static readonly Logger ExceptionLogger = Logger.GetInstance(typeof(GenericException));

        /// <summary>
        /// GenericPageException exception.
        /// </summary>
        public GenericException()
            : base()
        {
        }

        /// <summary>
        /// GenericPageException exception.
        /// </summary>
        /// <param name="message">Retrieves custom message for the exception.</param>
        public GenericException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// GenericPageException exception.
        /// </summary>
        /// <param name="message">Retrieves the message.</param>
        /// <param name="innerException">Retrieves the inner exception.</param>
        public GenericException(string message
            , Exception innerException)
            : base(message, innerException)
        {
        }
    }
}