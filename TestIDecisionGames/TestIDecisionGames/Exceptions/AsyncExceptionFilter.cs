using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestIDecisionGames.Exceptions
{
    public class AsyncExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var error = new ErrorDetails()
            {
                StatusCode = 500,
                Message = "Oops... Something went wrong! Internal Server Error."
            };
            context.Result = new JsonResult(error);
            return Task.CompletedTask;
        }
    }
}
