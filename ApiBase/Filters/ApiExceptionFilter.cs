using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiBase.Filters
{
    public class ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IHostEnvironment hostEnvironment) : IExceptionFilter
    {
        private readonly ILogger _logger = logger;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            if (!_hostEnvironment.IsDevelopment())
            {
                // Don't display exception details unless running in Development.
                _logger.LogError(context.Exception, "Ocorreu um exceção não tratada: Status Code 500");

                context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação: Status Code 500")
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }

    }
}
