using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task20_1.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessge = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = $"Method:{actionName}, Exception:{exceptionMessge}-{exceptionStack}"
            };
            context.ExceptionHandled = true;
        }
    }
}
