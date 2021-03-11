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

namespace Task47
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                //.AddJsonFile("person.json")
                .AddJsonFile("person2.json");
            AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var tom = new Person2();
            AppConfiguration.Bind(tom);
            app.Run(async (context) => 
            {
                string name = $"<p>Name: {tom.Name}</p>";
                string age = $"<p>Age: {tom.Age}</p>";
                string company = $"<p>Company: {tom.Company?.Title}</p>";
                string langs = "<p>Languages:</p><ul>";
                foreach (var lang in tom.Languages)
                {
                    langs += $"<li><p>{lang}</p></li>";
                }
                langs += "</ul>";
                await context.Response.WriteAsync($"{name}{age}{company}{langs}");
                // await context.Response.WriteAsync($"<p>Name: {tom.Name}</p><p>Age: {tom.Age}</p>");
            });
        }
    }
}
