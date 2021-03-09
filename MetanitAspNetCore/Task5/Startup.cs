using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task31.Services;

namespace Task31
{
    public class Startup
    {

        private IServiceCollection _services;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<TimeService>();
            //services.AddTimeService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender messageSender, TimeService timeService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                //await context.Response.WriteAsync(messageSender.Send());
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Time {timeService?.GetTime()}");
            });
            //app.Run(async (context) =>
            //{
            //    var sb = new StringBuilder();
            //    sb.Append("<h1>All of the services</h1>");
            //    sb.Append("<table>");
            //    sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Realization</th></tr>");
            //    foreach (var svc in _services)
            //    {
            //        sb.Append("<tr>");
            //        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
            //        sb.Append($"<td>{svc.Lifetime}</td>");
            //        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
            //        sb.Append("</tr>");
            //    }
            //    sb.Append("</table>");
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync(sb.ToString());
            //});
        }
    }
}
