using AnimieTechTv.Communication.Reponse;
using AnimieTechTv.Exceptions;
using AnimieTechTv.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AnimieTechTv.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AnimieTechTVException)
            HandleApplicationException(context);
        else
            ThrowUnknownException(context);
    }

    private static void HandleApplicationException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is ErrorOnValidation)
            ThrowValidationException(context);
    }

    private static void ThrowValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ErrorOnValidation;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessage));
    }

    private static void ThrowUnknownException(ExceptionContext context)
    {
        var exception = context.Exception as AnimieTechTVException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessageExceptions.UNKNOW_ERROR));
    }
}
