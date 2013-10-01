using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;
using KT.Extensions;

namespace CZAOSCore.Logging
{
    public class Log4NetProvider : InitializedInstanceBase, ILoggingProvider
    {

        // Repository references
        private ThreadSafeStaticDictionary<LogTarget, ILog> _loggers = new ThreadSafeStaticDictionary<LogTarget, ILog>();
        private ILog _errorLog;

        protected override bool OnInitialize()
        {
            // Initialize log4net logging here
            if (log4net.LogManager.GetRepository().Configured == false)
            {
                log4net.Config.XmlConfigurator.Configure();
            }
            
            // Get repositories -- named ones should exist by default in log4net.config
            _loggers.Add(LogTarget.Root, LogManager.GetLogger("root"));
            _loggers.Add(LogTarget.HttpModule, LogManager.GetLogger("HttpModule"));
            _loggers.Add(LogTarget.WebUI, LogManager.GetLogger("WebUI"));
            _loggers.Add(LogTarget.Object, LogManager.GetLogger("Object"));
            _loggers.Add(LogTarget.Security, LogManager.GetLogger("Security"));

            _errorLog = LogManager.GetLogger("Errors");

            Log(LogTarget.Root, MessageLevel.Info, "Initialized log4net Logging");
            Log(LogTarget.HttpModule, MessageLevel.Info, "Initialized logger");
            _errorLog.Info("Started error logger");

            return true;

        }

        private string AppendUsername(string baseMessage)
        {
            if (HttpContext.Current != null)
            {
                try
                {
                    string user = HttpContext.Current.User.Identity.Name;
                    string sessionmessage = string.Format(" \r\n(Username = {0})", user);

                    if (user.IsNullOrEmpty())
                        sessionmessage = string.Empty;

                    if (!string.IsNullOrEmpty(baseMessage))
                        return string.Concat(baseMessage, sessionmessage);
                    else
                        return sessionmessage;
                }
                catch { }
            }

            return baseMessage;
        }

        #region Errors

        public void LogError(ErrorLevel level, Exception ex, bool rethrow)
        {
            LogError(level, null, ex, rethrow);
        }

        public void LogError(ErrorLevel level, string message, Exception ex, bool rethrow)
        {
            if (Initialize())
            {

                if (_errorLog == null) return; // If logger was misconfigured, nothing we can do here
                message = AppendUsername((string.IsNullOrEmpty(message)) ? "Exception occurred" : message);

                // Ignore thread abort exceptions.  Response Redirect throws these all the time for no good reason
                //  when ending a response.
                if (ex != null && ex is ThreadAbortException)
                    return;

                switch (level)
                {
                    case ErrorLevel.Error:
                        if (ex == null)
                            _errorLog.Error(message);
                        else
                            _errorLog.Error(message, ex);
                        break;

                    case ErrorLevel.Fatal:
                        if (ex == null)
                            _errorLog.Fatal(message);
                        else
                            _errorLog.Fatal(message, ex);
                        break;
                }


            }

            if (rethrow)
                throw ex;
        }

        #endregion

        #region Non-fatal Message Logging

        /// <summary>
        /// Logs a detailed non-error message.  The detailObjects will be enumerated and dumped to the output message.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="level"></param>
        /// <param name="detailObjects"></param>
        public void Log(LogTarget target, MessageLevel level, string message, params object[] detailObjects)
        {
            if (Initialize())
            {
                ILog logger;

                if (!_loggers.ContainsKey(target)) return;

                if (!_loggers.TryRead(target, out logger) || logger == null)
                    return; // If logger was misconfigured, nothing we can do here

                message = AppendUsername(message);

                switch (level)
                {
                    case MessageLevel.Debug:
                        if (logger.IsDebugEnabled) logger.Debug(message);
                        break;
                    case MessageLevel.Warning:
                        if (logger.IsWarnEnabled) logger.Warn(message);
                        break;
                    case MessageLevel.Info:
                        if (logger.IsInfoEnabled) logger.Info(message);
                        break;
                }
            }

        }

        public void Log(LogTarget target, MessageLevel level, string message)
        {
            Log(target, level, message, null);
        }

        #endregion
    }
}
