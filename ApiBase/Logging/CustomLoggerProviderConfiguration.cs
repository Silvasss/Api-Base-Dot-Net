namespace ApiBase.Logging
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
        public int EventId { get; set; } = 0;
    }
}
