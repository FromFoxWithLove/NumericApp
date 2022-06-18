using System.Net;

namespace Numeric.API.Middlewares
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleResponse(context);
            }
        }

        private async Task HandleResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }));
        }
    }
}
