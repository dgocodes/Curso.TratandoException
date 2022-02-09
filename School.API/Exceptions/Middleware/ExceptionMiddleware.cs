using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using School.API.Models;
using System.Net;

namespace School.API.Exceptions.Middleware
{
    public static class ExceptionMiddleware
    {
        //Build-int exception handler
        public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();
                    
                    context.Response.ContentType = "application/json";

                    if(contextFeature != null)
                    {
                        var errorString = new ErrorReponseData()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest?.Path ?? string.Empty,
                        }.ToString();

                        await context.Response.WriteAsync(errorString);
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
