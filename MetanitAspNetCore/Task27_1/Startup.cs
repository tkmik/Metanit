using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task27_1.Services;

namespace Task27_1
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new RewriteOptions()
                .AddRedirect("(.*)/$", "$1")
                .AddRewrite("home/index", "home/about", skipRemainingRules: true);
            var options2 = new RewriteOptions()
                .AddIISUrlRewrite(env.ContentRootFileProvider, "urlrewrite.xml");

            var options3 = new RewriteOptions()
               .AddApacheModRewrite(env.ContentRootFileProvider, "rewrite.txt");

            var options4 = new RewriteOptions().Add(RewritePHPRequests);

            var options5 = new RewriteOptions()
            .Add(new RedirectPHPRequests(extension: "html", newPath: "/html"));

            app.UseRewriter(options5);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/home/about", async context =>
                {
                    await context.Response.WriteAsync($"About: {context.Request.Path}");
                });
                endpoints.MapGet("/home/index", async context =>
                {
                    await context.Response.WriteAsync("Home Index Page!");
                });
            });

            static void RewritePHPRequests(RewriteContext context)
            {
                var path = context.HttpContext.Request.Path;
                var pathValue = path.Value; 
                                            
                if (path.StartsWithSegments(new PathString("/html")))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.Result = RuleResult.EndResponse;
                    return;
                }
                
                if (pathValue.EndsWith(".php", StringComparison.OrdinalIgnoreCase))
                {                   
                    string proccedPath = "/html" + pathValue.Substring(0, pathValue.Length - 3) + "html";
                    context.HttpContext.Request.Path = new PathString(proccedPath);
                }
            }
        }
    }
}
