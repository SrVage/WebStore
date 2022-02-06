using System.Xml;
using Microsoft.Extensions.Logging;
using System.Reflection;
using log4net;

namespace WebStore.Logging
{
    public class Log4NetLogger:ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string Category, XmlElement Configuration)
        {
            var logger_repository = LogManager
               .CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(logger_repository.Name, Category);

            log4net.Config.XmlConfigurator.Configure(Configuration);
        }

        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel Level) => Level switch
        {
            LogLevel.Trace => _log.IsDebugEnabled,
            LogLevel.Debug => _log.IsDebugEnabled,
            LogLevel.Information => _log.IsInfoEnabled,
            LogLevel.Warning => _log.IsWarnEnabled,
            LogLevel.Error => _log.IsErrorEnabled,
            LogLevel.Critical => _log.IsFatalEnabled,
            LogLevel.None => false,
            _ => throw new ArgumentOutOfRangeException(nameof(Level), Level, null)
        };

        public void Log<TState>(
            LogLevel Level,
            EventId Id,
            TState State,
            Exception? Error,
            Func<TState, Exception?, string> Formatter)
        {
            if (Formatter is null) throw new ArgumentNullException(nameof(Formatter));

            if (!IsEnabled(Level))
                return;

            var logString = Formatter(State, Error);
            if (string.IsNullOrWhiteSpace(logString) && Error is null)
                return;

            switch (Level)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(Level), Level, null);

                case LogLevel.None:
                    break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(logString);
                    break;

                case LogLevel.Information:
                    _log.Info(logString);
                    break;

                case LogLevel.Warning:
                    _log.Warn(logString);
                    break;

                case LogLevel.Error:
                    _log.Error(logString, Error);
                    break;

                case LogLevel.Critical:
                    _log.Fatal(logString, Error);
                    break;
            }
        }
    }
}
