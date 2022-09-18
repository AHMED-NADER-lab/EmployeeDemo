using Newtonsoft.Json;
using System.Net;

namespace EmployeeDemo.Middelware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                ILogger _logger = logger;
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ExceptionHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.BadRequest; // 400 if unexpected
            string Message = "";
         
      
          
                code = HttpStatusCode.BadRequest;
            Message += ex.Message;
            var result = JsonConvert.SerializeObject(new
            {
                Success = "false",
                Message,
                ExceptionType = "Custom"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            logger.LogError(Message);
            return context.Response.WriteAsync(result);
        }
    }


    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(
       this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
