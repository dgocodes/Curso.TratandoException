using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using School.API.Models;
using System.Net;

namespace School.API.Exceptions.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] //Permite apenas o uso em classe ou métodos
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;

            //Customizando status code de acordo com a exception
            if(context.Exception is EmailAlreadyRegistredException ||
               context.Exception is MinimumAgeException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            var exceptionString = new ErrorReponseData()
            {
                StatusCode = (int)statusCode,
                Message = context.Exception.Message,
                Path = context.Exception.StackTrace ?? string.Empty
            }.ToString();

            context.Result = new JsonResult(exceptionString);
        }
    }
}
