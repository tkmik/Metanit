using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task61
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            var loggerFactory = LoggerFactory.Create(builder => 
            {
                builder.AddConsole();
            });
            ILogger ilogger = loggerFactory.CreateLogger<Startup>();
            app.Run(async (context) => 
            {
                ilogger.LogInformation($"requested path {context.Request.Path}");
                await context.Response.WriteAsync("hello");
            });

            //app.Run(async (context)=> 
            //{
            //    logger.LogTrace($"log trace - {context.Request.Path}");
            //    logger.LogDebug($"log debug - {context.Request.Path}");
            //    logger.LogInformation($"log information - {context.Request.Path}");
            //    logger.LogWarning($"log warning - {context.Request.Path}");
            //    logger.LogError($"log error - {context.Request.Path}");
            //    logger.LogCritical($"log ritical - {context.Request.Path}");
            //    await context.Response.WriteAsync("hello");
            //});
        }
    }
}
