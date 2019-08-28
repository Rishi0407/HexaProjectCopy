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
using TagHelperDemo.Services;

namespace TagHelperDemo
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

            //services.AddSingleton<StateDataService>();
            services.AddTransient<StateDataService>();

            services.AddMemoryCache();

            // To enable the distributed memory cacheing
            //services.AddDistributedMemoryCache();

            //dotnet sql-cache create "connectionstring" "schemaname" "tablename"
            //dotnet sql-cache create "Data Source=PUNB-TR05-012;Initial Catalog=HexaDB;Integrated Security=True" "dbo" "cachetable"
            services.AddDistributedSqlServerCache(config =>
            {
                config.ConnectionString = "Data Source=PUNB-TR05-012;Initial Catalog=HexaDB;Integrated Security=True"; //Configuration.GetConnectionString("SqlConnection");
                config.SchemaName = "dbo";
                config.TableName = "cachetable";
            });
            services.AddSession(config => {
                config.Cookie.MaxAge = TimeSpan.FromSeconds(300);
                config.Cookie.Name = "MysessionCookie";
                config.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            // To configure redis for the cache
            //services.AddStackExchangeRedisCache(config =>
            //{
            //    config.InstanceName = "redis";
            //    config.Configuration = "localhost:6379";
            //});



            services.AddMvc()
                //.AddSessionStateTempDataProvider()
                //.AddCookieTempDataProvider()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("x-data"))
                {
                    context.Items["KeyExists"] = true;
                    context.Items["DatabaseName"] = "Abc";
                }
                else
                {
                    context.Items["KeyExists"] = false;
                    context.Items["DatabaseName"] = "XYZ";
                }
                await next.Invoke();
            });
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
