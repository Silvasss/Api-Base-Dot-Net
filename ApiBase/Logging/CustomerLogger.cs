namespace ApiBase.Logging
{
    public class CustomerLogger(string name, CustomLoggerProviderConfiguration configCustom) : ILogger
    {
        readonly string loggerName = name;
        readonly CustomLoggerProviderConfiguration loggerConfig = configCustom;

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            SalvarTextoNoBancoDados(mensagem);
        }

        private async void SalvarTextoNoBancoDados(string mensagem)
        {
            string caminhoArquivoLog = @"LogsLocal\LogLocal.txt";

            using (StreamWriter streamWriter = new(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }      
        }
    }
}
