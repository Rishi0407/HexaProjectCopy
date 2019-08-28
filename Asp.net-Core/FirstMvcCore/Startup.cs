using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace FirstMvcCore
{
    public class Startup
    {
        static Dictionary<string, string> appsettings = new Dictionary<string, string>()
        {
            {"AuthorName","Rabindra"},
            {"AuthorEmail","rabindras@hexaware.com"},
            {"Message","I am from InMemory"}
        };
        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(appsettings)
                        .AddXmlFile("mysettings.xml", optional: true)
                        .AddIniFile("mysttings.ini", optional: true)
                        .AddJsonFile("mysettings.json", optional: true)
                        .AddKeyPerFile("FileDir", optional: true)
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables();
                
            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(Configuration.GetValue<string>("Company"));
            Console.WriteLine(Configuration.GetValue<string>("Address:City"));
            Console.WriteLine(Configuration.GetValue<int>("Duration"));
            Console.WriteLine(Configuration.GetValue<string>("ProjectName"));
            Console.WriteLine(Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT"));
            Console.WriteLine(Configuration.GetValue<string>("AuthorName"));
            Console.WriteLine(Configuration.GetValue<string>("AuthorEmail"));
            Console.WriteLine(Configuration.GetValue<string>("Message"));
            Console.WriteLine(Configuration.GetSection("Courses")["Title"]);

            services.Configure<AppConfiguration>(Configuration);



            Console.WriteLine("_________________________________________");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //env.EnvironmentName = "Production"; //ASPNETCORE_ENVIRONMENT ="Production";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler((builder) =>
                {
                    builder.Run(async (context) =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.Headers["Content-Type"] = "text/html";
                        await context.Response.WriteAsync("My custom error data.");
                        var exceptionPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        await context.Response.WriteAsync($"<p>{exceptionPathFeature.Error.Message}</p>");
                    });
                });
                app.UseHsts();
            }

            //app.UseStatusCodePages("text/html", "Client side error occurred with status code {0}");

            //app.UseStatusCodePagesWithRedirects("/index.html");
            //app.UseStatusCodePagesWithRedirects("/index.html?code={0}");
            app.UseStatusCodePagesWithReExecute("/index.html");

            app.UseHttpsRedirection();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    RequestPath = "/doc",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "doc"))
            //});

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    RequestPath = "/doc",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "doc"))
            //});

            app.UseStaticFiles();
            
            var fileOptions = new FileServerOptions
            {
                RequestPath = "/doc",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "doc")),
                EnableDirectoryBrowsing = true
            };
            fileOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileOptions.DefaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseFileServer(fileOptions);

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
