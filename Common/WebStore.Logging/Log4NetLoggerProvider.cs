using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace WebStore.Logging
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _configurationFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new();

        public Log4NetLoggerProvider(string ConfigurationFile) => _configurationFile = ConfigurationFile;

        public ILogger CreateLogger(string Category) =>
            _loggers.GetOrAdd(Category, static(category, congifFile) =>
            {
                var xml = new XmlDocument();
                xml.Load(congifFile);
                return new Log4NetLogger(category, xml["log4net"]!);
            }, _configurationFile);

        public void Dispose() => _loggers.Clear();
    }
}
