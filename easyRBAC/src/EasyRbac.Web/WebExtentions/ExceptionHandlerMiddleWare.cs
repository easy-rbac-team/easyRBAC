using EasyRbac.Dto.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRbac.Web.WebExtentions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                if (ex is EasyRbacException)
                {
                    context.Response.StatusCode = 400;
                }
                else
                {
                    context.Response.StatusCode = 500;
                }
                context.Response.ContentType = "application/json";
                var errObj = new ErroResponse()
                {
                    Message = ex.Message
                };

                var json = JsonConvert.SerializeObject(errObj, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                //context.Response.ContentLength = json.Length;
                var body = Encoding.UTF8.GetBytes(json);
                context.Response.ContentLength = body.Length;
                await context.Response.WriteAsync(json, Encoding.UTF8);
                

                await ClearCacheHeaders(ex);

            }
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;
            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);
            return Task.CompletedTask;
        }
    }
}
