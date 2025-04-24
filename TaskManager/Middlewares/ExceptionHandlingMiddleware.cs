using Newtonsoft.Json;
using System.Net;

namespace TaskManager.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // Chama o próximo middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(httpContext, ex); // Trata a exceção
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Configura o status code e o tipo de resposta
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                message = "An unexpected error occurred. Please try again later.",
                details = exception.Message // Você pode customizar a mensagem conforme necessário
            };

            // Retorna a resposta com o erro
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
