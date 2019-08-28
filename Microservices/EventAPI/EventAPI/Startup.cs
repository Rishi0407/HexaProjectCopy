using EventAPI.CustomFormatter;
using EventAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace EventAPI
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
            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Event API",
                    Description = "Contains the API operations for adding, deleting and querying events",
                    Version = "1.0",
                    Contact = new Contact { Name = "Rabindra Singh", Email = "rabindras@hexaware.com" }
                });
            });

            services.AddCors(c =>
            {
                c.AddPolicy("HexClients", config =>
                {
                    config.WithOrigins("https://microsoft.com", "https://synergetics.com")
                    .WithHeaders("Content-Type", "Accept", "Authorization")
                    .WithMethods("GET", "POST", "PUT", "DELETE");

                    config.WithOrigins("https://infosys.com")
                    .WithMethods("GET");
                });

                c.AddPolicy("AllowAll", config =>
                {
                    config.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

                c.AddDefaultPolicy(config =>
                {
                    config.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = Configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret")))
                };
            });


            services.AddMvc(c=> {
                c.OutputFormatters.Add(new CsvFormatter());
            })
                .AddXmlDataContractSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDatabase(app);

            app.UseSwagger();

            //app.UseCors("AllowAll");
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event API");
                    c.RoutePrefix = "";
                });
            }

            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<EventDbContext>())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
