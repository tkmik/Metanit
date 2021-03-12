using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task21
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
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<RoutingMiddleware>();

            //app.UseToken("123");
            //app.Run(async(context)=> 
            //{
            //    await context.Response.WriteAsync("Hello");
            //});
            ////app.Map("/home", home =>
            ////{
            ////    app.Map("/test", Test);
            ////});
            ////app.Run(async(context)=>
            ////{
            ////    await context.Response.WriteAsync("home");
            ////});
            //////app.Use(async(context,next)=>
            //////{
            //////    await context.Response.WriteAsync("<p>Hello</p>");
            //////    await next.Invoke();
            //////});

            //////app.Run(async(context)=>
            //////{
            //////    await context.Response.WriteAsync("<p>World</p>");
            //////});
            ////////int x = 10;
            ////////int y = 12;
            ////////int z = 0;

            ////////app.Use(async(context,next)=> 
            ////////{
            ////////    z = x * y;
            ////////    await next.Invoke();
            ////////});


            ////////app.Run(async(context) => 
            ////////{
            ////////    await context.Response.WriteAsync($"x*y={z}");
            ////////});
        }
        ////private static void Test(IApplicationBuilder app)
        ////{
        ////    app.Run(async(context)=>
        ////    {
        ////        await context.Response.WriteAsync("Test");
        ////    });
        ////}
    }
}
