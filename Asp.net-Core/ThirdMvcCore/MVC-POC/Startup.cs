using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiddlewareDemo.Middleware;

namespace MVC_POC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            //app.UseMiddleware<SecurityMiddleware>();
            app.UseSecurity();

            app.Use(async (context, next)=>
            {
                //context.Response.Headers.Add("Content-Type","text/html");
                await context.Response.WriteAsync("<br/>Middleware request 1 ");
                await next.Invoke();
                await context.Response.WriteAsync("<br/>Middleware response 1");
            });
           
            app.Use(async(context,next)=>
            {
                await context.Response.WriteAsync("<br/>Middelware 2 request");
                await next.Invoke();
                await context.Response.WriteAsync("<br/>Midderware 2 response");
            });
            
            // Only execute if URL matches
           app.Map("/about", async(builder)=>{
               builder.Use(async(context, next)=>{
                await context.Response.WriteAsync("<br/>Middelware for about request");
                await next.Invoke();
                await context.Response.WriteAsync("<br/>Midderware for about response");
               });
                builder.Run(async(context)=>{
                    await context.Response.WriteAsync("<br/>About page");
                });
           });

            // Only execute if URL matches
           app.Map("/Contact", async(builder)=>{               
                builder.Run(async(context)=>{
                    await context.Response.WriteAsync("<br/>Contact page");
                });
           });

            app.MapWhen(ctx=>ctx.Request.Query["version"]=="v1", (builder)=>{
                builder.Run(async(context)=>{
                    await context.Response.WriteAsync("<br/>version 1 executed!");
                });
            });

            app.MapWhen(ctx=>ctx.Request.Query["version"]=="v2", (builder)=>{
                builder.Run(async(context)=>{
                    await context.Response.WriteAsync("<br/>version 2 executed!");
                });
            });

            app.Run(async (context)=>
            {
                await context.Response.WriteAsync("<br/>Hellow World!");
            });

            // app.UseHttpsRedirection();
            // app.UseStaticFiles();
            // app.UseCookiePolicy();

            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });
        }
    }
}
