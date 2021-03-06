using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Task71
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                Endpoint endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;

                    Debug.WriteLine($"{endpoint.DisplayName}");
                    Debug.WriteLine($"{routePattern}");
                    await next.Invoke();
                }
                else
                {
                    Debug.WriteLine("Endpoint: null");
                    await next.Invoke();
                }
            });
            app.UseEndpoints((endpoints)=> 
            {
                endpoints.MapGet("/", async (context) => 
                {
                    await context.Response.WriteAsync("Hello");
                });
                endpoints.MapGet("/index", async (context) => 
                {
                    await context.Response.WriteAsync("Index");
                });
            });
        }
    }
}
