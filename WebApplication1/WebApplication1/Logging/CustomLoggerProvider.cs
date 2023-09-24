using System.Collections.Concurrent;

namespace FiapStore.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration logerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration logerConfig)
        {
            this.logerConfig = logerConfig;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomLogger(name, logerConfig));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
