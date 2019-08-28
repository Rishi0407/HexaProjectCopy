using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementApplication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagementApplication
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


            services.AddDbContext<EventDbContext>(options =>
            {
                //options.UseInMemoryDatabase(databaseName: "HexaDB");
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
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

            //InitializeDatabase(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                using (var db = new EventDbContext(serviceProvider.GetRequiredService<DbContextOptions<EventDbContext>>()))
                {
                    if (db.Events.Any())
                    {
                        return;
                    }
                    else
                    {
                        db.Events.Add(new Models.EventInfo
                        {
                            Title ="Modern App",
                            StartDate = DateTime.Now.AddDays(5),
                            EndDate = DateTime.Now.AddDays(7),
                            Location="Pune",
                            Organizer = "Microsoft",
                            RegistrationUrl = "https://events.microsoft.com/MDAZ001"
                        });

                        db.Events.Add(new Models.EventInfo
                        {
                            Title = "Modern App 1",
                            StartDate = DateTime.Now.AddDays(7),
                            EndDate = DateTime.Now.AddDays(9),
                            Location = "Pune 3",
                            Organizer = "Microsoft 3",
                            RegistrationUrl = "https://events.microsoft.com/MDAZ002"
                        });

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
