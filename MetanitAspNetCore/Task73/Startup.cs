using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task73
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
            RouteBuilder routeBuilder = new RouteBuilder(app);
            routeBuilder.MapRoute("{controller}/{action}", async (context) => 
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("Controller/action");
            });

            routeBuilder.MapRoute("{controller}/{action}/{id}", async (context)=> 
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("Controller/action/id");
            });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("default");
            });

            //static
            //routeBuilder.MapRoute("default", "store/{action}");
            //nullable
            //routeBuilder.MapRoute("default", "api/{controller}/{action}/{id?}");
            //routeBuilder.MapRoute("default", "{controller}/{action?}/{id?}");
            //default params
            //routeBuilder.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "home", action = "index" });
            //routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //take all params in the end
            //routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}/{*catchall}");
            //prefix
            //routeBuilder.MapRoute("default", "Ru{controller=Home}/{action=Index}-en/{id?}");
            //some params in the segment
            //routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{name}-{year}");
        }
    }
}
