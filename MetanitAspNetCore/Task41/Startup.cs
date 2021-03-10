using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task41
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"firstname","Tom" },
                    {"age", "31" }
                })
                .AddJsonFile("config.json")
                .AddXmlFile("config.xml")
                .AddConfiguration(config);
            
            AppConfiguration = builder.Build();
        }
        //public Startup(IConfiguration config)
        //{
        //    AppConfiguration = config;
        //}

        public IConfiguration AppConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async (context)=>
            {
                //await context.Response.WriteAsync(AppConfiguration["firstname"]);
                await context.Response.WriteAsync(AppConfiguration["name"] + AppConfiguration["color"]);
            });
        }
    }
}
