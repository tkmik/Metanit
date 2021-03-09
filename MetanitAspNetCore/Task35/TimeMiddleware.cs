using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task35
{
    public class TimeMiddleware
    {
        private readonly RequestDelegate _next;
        TimeService _timeService;

        public TimeMiddleware(RequestDelegate next, TimeService timeService)
        {
            _next = next;
            _timeService = timeService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower() == "/time")
            {
                context.Response.ContentType = "/text/html; charset=utf-8";
                await context.Response.WriteAsync($"Current time: {_timeService?.Time}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
