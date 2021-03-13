using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task51.Models;
using Task51.Extensions;

namespace Task51
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.Run(async (context)=> 
            {
                if (context.Session.Keys.Contains("person"))
                {
                    Person person = context.Session.Get<Person>("person");
                    await context.Response.WriteAsync($"{person.Name}-{person.Age}");
                }
                else
                {
                    Person person = new Person() { Name = "Nikita", Age = 0 };
                    context.Session.Set<Person>("person", person);
                    await context.Response.WriteAsync("hello");
                }
            });
            //app.UseSession();
            //app.Run(async (context)=> 
            //{
            //    if (context.Session.Keys.Contains("name"))
            //    {
            //        await context.Response.WriteAsync($"Hello {context.Session.GetString("name")}");
            //    }
            //    else
            //    {
            //        context.Session.SetString("name", "Mikita");
            //        await context.Response.WriteAsync("Hello");
            //    }
            //});
            ////app.Run(async (context)=> 
            ////{
            ////    if (context.Request.Cookies.ContainsKey("name"))
            ////    {
            ////        string name = context.Request.Cookies["name"];
            ////        await context.Response.WriteAsync(name);
            ////    }
            ////    else
            ////    {
            ////        context.Response.Cookies.Append("name", "Mikita");
            ////        await context.Response.WriteAsync("Hello");
            ////    }
            ////});
            //////app.Use(async (context, next)=> 
            //////{
            //////    context.Items["text"] = "Text from Items";
            //////    await next.Invoke();
            //////});
            //////app.Run(async (context) => 
            //////{
            //////    context.Response.ContentType = "text/html; charset=utf-8";
            //////    await context.Response.WriteAsync($"{context.Items["text"]}");
            //////});
        }
    }
}
