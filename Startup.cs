using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace events
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            
            if (env.IsDevelopment())
            {
                // Setup WebpackDevMidleware for "Hot module replacement" while debugging
                app.UseWebpackDevMiddleware(
                    new WebpackDevMiddlewareOptions()
                    {
                        HotModuleReplacement = true
                    }
                );
            }
            else
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "api",
                        template: "{controller=Home}/{action=Index}/{id?}"
                    );
                    // Allow returning of physical files on the file system of the web root
                    routes.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "Home", action = "Index" }
                    );
                });

            }

            app.UseDeveloperExceptionPage();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
