using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Task29
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
                app.UseBrowserLink();
            }

            app.UseFileServer(enableDirectoryBrowsing: true);

            //app.UseFileServer(new FileServerOptions
            //{
            //    EnableDirectoryBrowsing = true,
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"xxxroot\html")),
            //    RequestPath = new PathString("/pages"),
            //    EnableDefaultFiles = true
            //});

            ////app.UseDirectoryBrowser();
            ////app.UseDefaultFiles();
            ////app.UseStaticFiles();

            //////DefaultFilesOptions options = new DefaultFilesOptions();
            //////options.DefaultFileNames.Clear();
            //////options.DefaultFileNames.Add("hello.html");
            //////app.UseDefaultFiles(options);

            app.Run(async(context)=>
            {
                await context.Response.WriteAsync("Hello");
            });
        }
    }
}
