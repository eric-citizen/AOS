using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZAOSCore.Logging
{
    /// <summary>
    /// Provides singleton access to the logging facility for easy usage.  NOTE: If the logger is NOT configured properly, it should not
    /// explode and throw exceptions but rather not log at all.  This way if you have code that provides logging but you don't want to
    /// capture it the logging will just get ignored.
    /// 
    /// This manager is purposely implemented without the base manager class to avoid infinite recursion trying to initialize itself.
    /// </summary>
    public static class Logger
    {
        private static ILoggingProvider _logProvider = null;

        /// <summary>
        /// Static initializer to get instance of logger
        /// </summary>
        static Logger()
        {
            try
            {
                //TODO
                _logProvider = Activator.CreateInstance(Type.GetType("CZAOSCore.Logging.Log4NetProvider, CZAOSCore")) as ILoggingProvider;
            }
            catch
            {
                _logProvider = null;
            }
        }

        /// <summary>
        /// Initialize any resources needed to perform logging
        /// </summary>
        public static void Initialize()
        {
            if (_logProvider != null)
                _logProvider.Initialize();
        }

        /// <summary>
        /// Log an error with default message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="ex"></param>
        /// <param name="rethrow"></param>
        public static void LogError(ErrorLevel level, Exception ex, bool rethrow)
        {
            if (_logProvider != null)
            {
                _logProvider.Initialize();
                _logProvider.LogError(level, ex, rethrow);
            }
        }

        /// <summary>
        /// Logs an error with custom message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="rethrow"></param>
        public static void LogError(ErrorLevel level, string message, Exception ex, bool rethrow)
        {
            if (_logProvider != null)
            {
                _logProvider.Initialize();
                _logProvider.LogError(level, message, ex, rethrow);
            }
        }

        /// <summary>
        /// Logs an informational diagnostic message
        /// </summary>
        /// <param name="target"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public static void Log(LogTarget target, MessageLevel level, string message)
        {
            if (_logProvider != null)
            {
                _logProvider.Initialize();
                _logProvider.Log(target, level, message);
            }
        }

        /// <summary>
        /// Logs an informational diagnostic message with optional variables that need to be converted as strings.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="level"></param>
        /// <param name="Message"></param>
        /// <param name="detailObjects"></param>
        public static void Log(LogTarget target, MessageLevel level, string Message, params object[] detailObjects)
        {
            if (_logProvider != null)
            {
                _logProvider.Initialize();
                _logProvider.Log(target, level, Message, detailObjects);
            }
        }

    }
}
