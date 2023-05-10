using System;


namespace EPS.Automation.Exceptions
{
    public class LoginFailedException : BaseAutomationException
    {
        /// <summary>
        /// The logger  instance for the class.
        /// </summary>
        public override Logger Logger
        {
            get
            {
                return exceptionLogger;
            }
        }

        /// <summary>
        /// Static logger instance of the class.
        /// </summary>
        private static Logger exceptionLogger = Logger.GetInstance(typeof(LoginFailedException));

        /// <summary>
        /// This is the LoginFailedException exception
        /// </summary>
        public LoginFailedException()
            : base()
        {
        }

        /// <summary>
        /// This is the LoginFailedException excpeiton
        /// </summary>
        /// <param name="message">A custom message for the exception</param>
        public LoginFailedException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// This is the LoginFailedException excpeiton
        /// </summary>
        /// <param name="message">This is the message</param>
        /// <param name="innerException">This is the inner exception</param>
        public LoginFailedException(string message
            , Exception innerException)
            : base(message, innerException)
        {
        }
    }
}