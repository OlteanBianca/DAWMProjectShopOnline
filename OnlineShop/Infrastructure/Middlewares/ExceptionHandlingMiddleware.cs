using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using OnlineShop.Infrastructure.Exceptions;
using System.Net;

namespace OnlineShop.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        #region Private Fields 
        private readonly RequestDelegate next;
        #endregion

        #region Constructors
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        #endregion

        #region Private Methods
        private static Task RespondToExceptionAsync(HttpContext context, HttpStatusCode failureStatusCode, string message, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)failureStatusCode;

            var response = new
            {
                Message = message,
                Error = exception.GetType().Name,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        #endregion

        #region Public Methods
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("request1");

            try
            {
                await next(context);
            }
            catch (ForbiddenException ex)
            {
                await RespondToExceptionAsync(context, HttpStatusCode.Forbidden, ex.Message, ex);
            }
            catch (ResourceMissingException ex)
            {
                await RespondToExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message, ex);
            }
            catch (Exception ex)
            {
                await RespondToExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex);
            }

            Console.WriteLine("response1");
        }

        #endregion
    }
}
