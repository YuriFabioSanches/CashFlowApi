using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowlUnkownError(context);
        }
    }
    
    public void HandleProjectException(ExceptionContext context) 
    {
        var cashFlowException = (CashFlowException)context.Exception;
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        
        var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());
        context.Result = new ObjectResult(errorResponse);
    }

    public void ThrowlUnkownError(ExceptionContext context) 
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
