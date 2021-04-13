using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Task28_1
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var language = context.Request.Query["lang"].ToString();
            if (!string.IsNullOrEmpty(language))
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(language);
                    CultureInfo.CurrentUICulture = new CultureInfo(language);
                }
                catch (CultureNotFoundException)
                {

                }
            }
            await _next.Invoke(context);
        }
    }
    public static class CultureExtensions
    {
        public static IApplicationBuilder UseCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }
}
