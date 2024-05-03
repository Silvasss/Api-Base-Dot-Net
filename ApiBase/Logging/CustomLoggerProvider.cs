using System.Collections.Concurrent;

namespace ApiBase.Logging
{
    public class CustomLoggerProvider(CustomLoggerProviderConfiguration configCustom) : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration loggerConfig = configCustom;

        readonly ConcurrentDictionary<string, CustomerLogger> loggers = new();

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
