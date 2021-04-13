using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Task28_1.Models;

namespace Task28_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = @"Server=(localdb)\MSSQLLocalDB;Database=localizationdb;Trusted_Connection=True;";
            services.AddDbContext<LocalizationContext>(options => 
            {
                options.UseSqlServer(connection);
            });
            services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(connection));

            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options => 
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(null);
                        //return factory.Create(typeof(SharedResource));
                    };
                }) // annotation localization 
                .AddViewLocalization(); //view localization
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("de"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //// //app.UseCulture();

            //// var supportedCultures = new[]
            ////{
            ////     new CultureInfo("en-US"),
            ////     new CultureInfo("en-GB"),
            ////     new CultureInfo("en"),
            ////     new CultureInfo("ru-Ru"),
            ////     new CultureInfo("ru"),
            ////     new CultureInfo("de-DE"),
            ////     new CultureInfo("de")
            //// };
            //// app.UseRequestLocalization(new RequestLocalizationOptions
            //// {
            ////     DefaultRequestCulture = new RequestCulture("ru-RU"),
            ////     SupportedCultures = supportedCultures,
            ////     SupportedUICultures = supportedCultures
            //// });

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("ru"),
            //    new CultureInfo("de")
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("ru"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});
            app.UseRequestLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
