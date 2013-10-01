﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZAOSCore.Logging
{

    #region ENUMS

    /// <summary>
    /// These correspond to the known logging repositories that should exist for diagnostic logging
    /// </summary>
    public enum LogTarget
    {
        /// <summary>
        /// This message should go to the default root logger only
        /// </summary>
        Root,

        /// <summary>
        /// This message was generated by a page or user control
        /// </summary>
        WebUI,

        /// <summary>
        /// This message was generated by a business object 
        /// </summary>
        Object,

        /// <summary>
        /// This is for HttpModules
        /// </summary>
        HttpModule,

        /// <summary>
        /// This is for Security logging - new for Jewell Dec 2012
        /// </summary>
        Security

    }

    /// <summary>
    /// Target error levels for diagnostic logging
    /// </summary>
    public enum ErrorLevel
    {
        Error,
        Fatal
    }

    /// <summary>
    /// Target diagnostic message importance when logging non-errors
    /// </summary>
    public enum MessageLevel
    {
        Debug,
        Info,
        Warning
    }

    
    #endregion

    public interface ILoggingProvider
    {
        /// <summary>
        /// Initialize any resources needed to perform logging
        /// </summary>
        bool Initialize();

        /// <summary>
        /// Log an error with default message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="ex"></param>
        /// <param name="rethrow"></param>
        void LogError(ErrorLevel level, Exception ex, bool rethrow);

        /// <summary>
        /// Logs an error with custom message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="rethrow"></param>
        void LogError(ErrorLevel level, string message, Exception ex, bool rethrow);

        /// <summary>
        /// Logs an informational diagnostic message
        /// </summary>
        /// <param name="target"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        void Log(LogTarget target, MessageLevel level, string message);

        /// <summary>
        /// Logs an informational diagnostic message with optional variables that need to be converted as strings.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="level"></param>
        /// <param name="Message"></param>
        /// <param name="detailObjects"></param>
        void Log(LogTarget target, MessageLevel level, string Message, params object[] detailObjects);
    }
}
