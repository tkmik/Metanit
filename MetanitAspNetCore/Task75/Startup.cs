using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Task75
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
            var routeHandler = new RouteHandler(Handle);
            var routerBuilder = new RouteBuilder(app, routeHandler);
            routerBuilder.MapRoute("default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index" },
                new { myConstraint = new CustomConstraint("/Home/Index/12") });
            //new { controller = new AlphaRouteConstraint()
            //new { id = new BoolRouteConstraint()
            //new { id = new DateTimeRouteConstraint() 
            //new { id = new DecimalRouteConstraint() 
            //new { id = new DoubleRouteConstraint() 
            //new { id = new FloatRouteConstraint() 
            //new { id = new GuidRouteConstraint()
            //new { httpMethod = new HttpMethodRouteConstraint("POST")
            //new { id = new IntRouteConstraint() 
            //new { id = new LongRouteConstraint() 
            //new { id = new RangeRouteConstraint(3, 100)
            //new { id = new MinRouteConstraint(4) }

            app.UseRouter(routerBuilder.Build());
            
        }
        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("hello");
        }
    }
}
