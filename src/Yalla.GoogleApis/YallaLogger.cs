using System;

namespace Yalla.GoogleApis
{
    /// <summary>
    /// A logger implementation which makes use of the YALLA.NET library.
    /// </summary>
    public class YallaLogger : Singleton<YallaLogger>, Google.Apis.Logging.ILogger
    {
        private static readonly ILoggerCache<Type, YallaLogger> _cache =
            new LoggerCache<Type, YallaLogger>();

        private readonly ILog _log;

        private YallaLogger()
            : base(null)
        {
        }

        private YallaLogger(ILog log)
            : base(null)
        {
            this._log = log;
        }

        /// <summary>
        /// Returns a logger which will be associated with the specified type.
        /// </summary>
        /// <param name="type">Type to which this logger belongs.</param>
        /// <returns>A type-associated logger.</returns>
        public Google.Apis.Logging.ILogger ForType(Type type)
        {
            return _cache.GetOrAdd(type, CreateLogger);
        }

        /// <summary>
        /// Returns a logger which will be associated with the specified type.
        /// </summary>
        /// <typeparam name="T">Type to which this logger belongs.</typeparam>
        /// <returns>A type-associated logger.</returns>
        public Google.Apis.Logging.ILogger ForType<T>()
        {
            return ForType(typeof(T));
        }

        /// <summary>
        /// Gets a value indicating whether debug output is enabled.
        /// </summary>
        /// <value><c>true</c> if debug output is enabled.</value>
        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args"><see cref="String.Format(String,Object[])"/> arguments (if applicable).</param>
        public void Debug(string message, params object[] args)
        {
            _log.DebugFormat(message, args);
        }

        /// <summary>
        /// Logs an info message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args"><see cref="String.Format(String,Object[])"/> arguments (if applicable).</param>
        public void Info(string message, params object[] args)
        {
            _log.InfoFormat(message, args);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args"><see cref="String.Format(String,Object[])"/> arguments (if applicable).</param>
        public void Warning(string message, params object[] args)
        {
            _log.WarnFormat(message, args);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args"><see cref="String.Format(String,Object[])"/> arguments (if applicable).</param>
        public void Error(string message, params object[] args)
        {
            _log.ErrorFormat(message, args);
        }

        /// <summary>
        /// Logs an error message resulting from an exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args"><see cref="String.Format(String,Object[])"/> arguments (if applicable).</param>
        public void Error(Exception exception, string message, params object[] args)
        {
            _log.ErrorFormat(exception, message, args);
        }

        private static YallaLogger CreateLogger(Type type)
        {
            var log = LogManager.GetLogger(type);
            return new YallaLogger(log);
        }
    }
}
