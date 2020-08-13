using System;
using System.IO;
using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace Reload.Core.Utilities
{
    /// <summary>
    /// Singleton wrapper around Serilog used to log messages to the console and/or file.
    /// </summary>
    public sealed class Logger
    {
        private static Logger _instance;

        /// <summary>
        /// Gets the logger instance, or creates new if instance is null. 
        /// </summary>
        public static Logger Log() => _instance ?? new Logger(); 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// and configures serilog.
        /// </summary>
        private Logger()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    Path.Combine(Assembly.GetEntryAssembly().Location, $"Logs/{DateTime.UtcNow}-Log.json"))
                .CreateLogger();
        }

        /// <summary>
        /// Logs information message to the console output and/or file.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Information(string message) => Serilog.Log.Information(message);

        /// <summary>
        /// Logs information message to the console output and/or file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Information(string message, params object[] propertyValues) => Serilog.Log.Information(message, propertyValues);

        /// <summary>
        /// Logs information message to the console output and/or file.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Information(Exception exception, string message, params object[] propertyValues) => Serilog.Log.Information(exception, message, propertyValues);

        /// <summary>
        /// Logs debug information message to the console output and/or file.
        /// </summary>
        public void Debug(string message) => Serilog.Log.Debug(message);

        /// <summary>
        /// Logs debug information message to the console output and/or file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Debug(string message, params object[] propertyValues) => Serilog.Log.Debug(message, propertyValues);

        /// <summary>
        /// Logs debug information message to the console output and/or file.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Debug(Exception exception, string message, params object[] propertyValues) => Serilog.Log.Debug(exception, message, propertyValues);

        /// <summary>
        /// Logs warning message to the console output and/or file.
        /// </summary>
        public void Warning(string message) => Serilog.Log.Warning(message);

        /// <summary>
        /// Logs warning message to the console output and/or file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Warning(string message, params object[] propertyValues) => Serilog.Log.Warning(message, propertyValues);

        /// <summary>
        /// Logs warning message to the console output and/or file.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Warning(Exception exception, string message, params object[] propertyValues) => Serilog.Log.Warning(exception, message, propertyValues);

        /// <summary>
        /// Logs error message to the console output and/or file.
        /// </summary>
        public void Error(string message) => Serilog.Log.Error(message);

        /// <summary>
        /// Logs error message to the console output and/or file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Error(string message, params object[] propertyValues) => Serilog.Log.Error(message, propertyValues);

        /// <summary>
        /// Logs error message to the console output and/or file.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Error(Exception exception, string message, params object[] propertyValues) => Serilog.Log.Error(exception, message, propertyValues);

        /// <summary>
        /// Logs fatal error message to the console output and/or file.
        /// </summary>
        public void Fatal(string message) => Serilog.Log.Fatal(message);

        /// <summary>
        /// Logs fatal error message to the console output and/or file.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Fatal(string message, params object[] propertyValues) => Serilog.Log.Fatal(message, propertyValues);

        /// <summary>
        /// Logs fatal error message to the console output and/or file.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertyValues">The property values.</param>
        public void Fatal(Exception exception, string message, params object[] propertyValues) => Serilog.Log.Fatal(exception, message, propertyValues);
    }
}
