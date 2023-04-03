using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PrimatesWallet.Application.Exceptions;
using System.Data.Common;
using System.Net;

namespace PrimatesWallet.Application.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (AppException exception)
            {
                var listErrors = new List<ErrorMessage>
                {
                    new ErrorMessage(exception.Message)
                };
                var EnvelopeError = new Envelope((int)exception.StatusCode, listErrors);

                await SendPayload(context, EnvelopeError, exception.StatusCode);
            }
            catch (DbException exception) 
            {
                var listErrors = new List<ErrorMessage>
                {
                    new ErrorMessage(exception.Message)
                };
                var EnvelopeError = new Envelope(404, listErrors);
                await SendPayload(context, EnvelopeError, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                var listErrors = new List<ErrorMessage>
                {
                    new ErrorMessage(exception.Message)
                };
                var EnvelopeError = new Envelope(500, listErrors);
                await SendPayload(context, EnvelopeError, HttpStatusCode.InternalServerError);
            }
        }

        private async Task SendPayload<TPayload>(HttpContext context, TPayload payload, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = JsonConvert.SerializeObject(payload, settings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}

